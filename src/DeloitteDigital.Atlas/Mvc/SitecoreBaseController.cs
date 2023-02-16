using System;
using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.Mapping;
using Sitecore.Data.Items;
using Sitecore.Mvc.Controllers;

namespace DeloitteDigital.Atlas.Mvc
{    
    public class SitecoreBaseController : SitecoreController
    {

        /// <summary>
        /// Maps model property values from Sitecore item using FieldMapAttribute metadata from the model class
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="from">Sitecore item</param>
        /// <returns>Model</returns>
        /// <remarks>This API is currrently in beta</remarks>
        protected T Map<T>(T to, Item from)
            where T : class
        {
            return new ItemMapper(new WebCache()).Map<T>(to, from);
        }

        protected T Map<T>(Item from)
            where T : class
        {
            var model = Activator.CreateInstance<T>();
            return new ItemMapper(new WebCache()).Map<T>(model, from);
        }

        protected Item SiteRootItem
        {
            get
            {
                var database = Sitecore.Context.Database;
                return database.GetItem(Sitecore.Context.Site.RootPath);
            }
        }

        protected Item SiteStartItem
        {
            get
            {
                var database = Sitecore.Context.Database;
                return database.GetItem(Sitecore.Context.Site.StartPath);
            }
        }

        /// <summary>
        /// Gets the current item from the Sitecore.Context class
        /// </summary>
        protected Item CurrentItem => Sitecore.Context.Item;

        /// <summary>
        /// Gets boolean value that indicate whether the current rendering context in experience editor mode
        /// </summary>
        /// <returns>True if is in experience editor mode, otherwise false</returns>
        public bool IsExperienceEditor => Sitecore.Context.PageMode.IsExperienceEditor;

    }
}
