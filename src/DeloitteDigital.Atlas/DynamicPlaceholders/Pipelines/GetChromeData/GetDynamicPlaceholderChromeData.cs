using System;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Abstractions;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.GetChromeData;

namespace DeloitteDigital.Atlas.DynamicPlaceholders.Pipelines.GetChromeData
{
    /// <summary>
    ///     Replaces the Displayname of the Placeholder rendering with the dynamic "parent"
    /// </summary>
    public class GetDynamicPlaceholderChromeData : GetChromeDataProcessor
    {
        public GetDynamicPlaceholderChromeData() : base(ServiceLocator.ServiceProvider.GetRequiredService<BaseClient>()) { }

        public override void Process(GetChromeDataArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.IsNotNull(args.ChromeData, "Chrome Data");
            if (!"placeholder".Equals(args.ChromeType, StringComparison.OrdinalIgnoreCase)) return;
            var placeholderKey = args.CustomData["placeHolderKey"] as string;

            // Is a Dynamic Placeholder
            if (placeholderKey != null && placeholderKey.Contains("_dph_"))
            {
                placeholderKey = placeholderKey.Substring(0, placeholderKey.LastIndexOf("_dph_", StringComparison.Ordinal));
                if (placeholderKey.LastIndexOf("/", StringComparison.Ordinal) >= 0)
                {
                    placeholderKey = placeholderKey.Substring(placeholderKey.LastIndexOf("/", StringComparison.Ordinal) + 1);
                }
            }

            args.ChromeData.DisplayName = placeholderKey;
            args.ChromeData.ExpandedDisplayName = placeholderKey + " Dynamic Placeholder";
        }
    }
}
