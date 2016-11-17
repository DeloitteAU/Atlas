using System;
using System.Collections.Generic;

namespace DeloitteDigital.Atlas.Caching.Events
{
    public class ClearCacheOnPublishEnd
    {
        public void OnPublishComplete(object sender, EventArgs e)
        {
            // clear the ICacheService managed items for any sites that have remote publish
            var registeredSites = GetSiteNamesForPublish();

            foreach (var registeredSite in registeredSites)
            {
                CacheHelper.ClearItemsWithPublishDependancy(registeredSite.Item1, registeredSite.Item2);
            }            
        }

        private static IEnumerable<Tuple<string, string>> GetSiteNamesForPublish()
        {
            const string EventName = "publish:end:remote";

            // get the sitelist, look in events.config for the list of sites
            // TODO - can this be done more elegantly against the API?
            var siteList = global::Sitecore.Configuration.Factory.GetConfigNodes(
                string.Format("/sitecore/events/event[@name='{0}']/handler[@type='Sitecore.Publishing.HtmlCacheClearer, Sitecore.Kernel']/sites/site",
                EventName));

            // make sure we have a site list to clean up
            if (siteList == null) yield break;

            // cycle through the site lists
            foreach (System.Xml.XmlNode xNode in siteList)
            {
                var site = Sitecore.Configuration.Factory.GetSite(xNode.InnerText);
                if (site != null)
                {
                    // clear the caching util
                    yield return new Tuple<string, string>(site.Name, site.Database.Name);
                }
            }
        }
    }
}
