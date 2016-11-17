using System;
using System.Runtime.Serialization;
using DeloitteDigital.Atlas.Refactoring;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Validators;
using Sitecore.Diagnostics;
using Sitecore.Shell.Applications.ContentEditor;

namespace DeloitteDigital.Atlas.Validators
{    
    /// <summary>
    /// Validator that validates than an image conforms with specific width/height requirements
    /// Taken from http://briancaos.wordpress.com/2011/05/16/sitecore-validators-validating-image-width-height-and-aspect-ratio/
    /// as at 21/06/2011
    /// </summary>
    /// <remarks>
    /// Validator accepts the following parameters:
    /// MinWidth = minimum image width
    /// MaxWidth = maximum image width
    /// MinHeight = minimum image height
    /// MaxHeight = maximum image height
    /// AspectRatio = The required aspect ratio (width/height width 2 decimals)
    /// Set minwidth/height and maxwidth/maxheight to the same values to validate an exact image size
    /// </remarks>
    [Serializable]
    [LegacyCode]
    public class ImageWidthHeightValidator : StandardValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageWidthHeightValidator"/> class.
        /// </summary>
        public ImageWidthHeightValidator()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageWidthHeightValidator"/> class.
        /// </summary>
        /// <param name="info">The Serialization info.</param>
        /// <param name="context">The context.</param>
        public ImageWidthHeightValidator(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The validator name.</value>
        public override string Name
        {
            get { return "Image Width/Height"; }
        }

        /// <summary>
        /// Gets the max validator result.
        /// </summary>
        /// <returns>The max validator result.</returns>
        /// <remarks>
        /// This is used when saving and the validator uses a thread. If the Max Validator Result
        /// is Error or below, the validator does not have to be evaluated before saving.
        /// If the Max Validator Result is CriticalError or FatalError, the validator must have
        /// been evaluated before saving.
        /// </remarks>
        protected override ValidatorResult GetMaxValidatorResult()
        {
            return GetFailedResult(ValidatorResult.CriticalError);
        }

        /// <summary>
        /// When overridden in a derived class, this method contains the code to determine whether the value in the input control is valid.
        /// </summary>
        /// <returns>The result of the evaluation.</returns>
        protected override ValidatorResult Evaluate()
        {
            int minWidth = MainUtil.GetInt(Parameters["MinWidth"], 0);
            int minHeight = MainUtil.GetInt(Parameters["MinHeight"], 0);
            int maxWidth = MainUtil.GetInt(Parameters["MaxWidth"], int.MaxValue);
            int maxHeight = MainUtil.GetInt(Parameters["MaxHeight"], int.MaxValue);
            float aspectRatio = MainUtil.GetFloat(Parameters["AspectRatio"], 0.00f);

            if (ItemUri != null)
            {
                if (MediaItem == null)
                    return ValidatorResult.Valid;

                int width = MainUtil.GetInt(MediaItem.InnerItem["Width"], 0);
                int height = MainUtil.GetInt(MediaItem.InnerItem["Height"], 0);

                if (minWidth == maxWidth && minHeight == maxHeight && (width != minWidth || height != minHeight))
                    return GetResult("The image referenced in the Image field \"{0}\" does not match the size requirements. Image needs to be exactly {1}x{2} but is {3}x{4}",
                                     GetField().DisplayName, minWidth.ToString(), minHeight.ToString(), width.ToString(),
                                     height.ToString());

                if (aspectRatio != 0.00f && height != 0 && (Math.Round((double)width / height, 2) != Math.Round(aspectRatio, 2)))
                    return GetResult("The image referenced in the Image field \"{0}\" has an invalid aspect ratio. The aspect ratio needs to be {1} but is {2}.",
                                     GetField().DisplayName, Math.Round(aspectRatio, 2).ToString(),
                                     (Math.Round((double)width / height, 2).ToString()));

                if (width < minWidth)
                    return GetResult("The image referenced in the Image field \"{0}\" is too small. The width needs to be at least {1} pixels but is {2}.",
                                     GetField().DisplayName, minWidth.ToString(), width.ToString());

                if (height < minHeight)
                    return GetResult("The image referenced in the Image field \"{0}\" is too small. The height needs to be at least {1} pixels but is {2}.",
                                     GetField().DisplayName, minHeight.ToString(), height.ToString());

                if (width > maxWidth)
                    return GetResult("The image referenced in the Image field \"{0}\" is too big. The width needs to at most {1} pixels but is {2}.",
                                     GetField().DisplayName, maxWidth.ToString(), width.ToString());

                if (height > maxHeight)
                    return GetResult("The image referenced in the Image field \"{0}\" is too big. The height needs to be at most {1} pixels but is {2}.",
                                     GetField().DisplayName, maxHeight.ToString(), height.ToString());
            }
            return ValidatorResult.Valid;
        }

        /// <summary>
        /// Gets the media item.
        /// </summary>
        /// <value>The media item.</value>
        private MediaItem MediaItem
        {
            get
            {
                ItemUri itemUri = ItemUri;

                Field field = GetField();
                if (field == null)
                    return null;

                if (string.IsNullOrEmpty(field.Value))
                    return null;

                if (field.Value == "<image />")
                    return null;

                string attribute = new XmlValue(field.Value, "image").GetAttribute("mediaid");
                if (string.IsNullOrEmpty(attribute))
                    return null;

                Sitecore.Data.Database database = Factory.GetDatabase(itemUri.DatabaseName);
                Assert.IsNotNull(database, itemUri.DatabaseName);
                return database.GetItem(attribute);
            }
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns></returns>
        private ValidatorResult GetResult(string text, params string[] arguments)
        {
            Text = GetText(text, arguments);
            return GetFailedResult(ValidatorResult.Warning);
        }

    }
}