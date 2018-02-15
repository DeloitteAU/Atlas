using System.Web.UI;
using DeloitteDigital.Atlas.Logging;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace DeloitteDigital.Atlas.Mvvm
{    
    /// <summary>
    /// Provides common helper methods for view controls (layouts, sub-layouts, user-controls)
    /// </summary>
    internal class ViewControlHelper
    {
        /// <summary>
        /// Gets the data source item for a control, given it is a Sublayout
        /// </summary>
        /// <param name="control">Sublayout control</param>
        /// <returns>Data source item</returns>
        public Item GetDataSource(Control control)
        {
            var parent = control.Parent;

            if (parent is Sublayout)
            {
                var dataSourceItem = Sitecore.Context.Database.GetItem(((Sublayout)parent).DataSource);

                if (dataSourceItem != null)
                {
                    return dataSourceItem;
                }
            }

            return null;
        }

        public Item GetSiteItem()
        {
            var database = Sitecore.Context.Database;
            return database.GetItem(Sitecore.Context.Site.RootPath);
        }

        public Item GetSiteStartItem()
        {
            var database = Sitecore.Context.Database;
            return database.GetItem(Sitecore.Context.Site.StartPath);
        }

        /// <summary>
        /// Shows reactive warning control if any
        /// </summary>
        /// <typeparam name="TWarning">Warning control interface type</typeparam>
        /// <param name="control">Parent (container) control</param>
        public void ShowReactiveWarning<TWarning>(Control control)
            where TWarning : class, IReactiveWarning
        {
            foreach (var child in control.Controls)
            {
                if (typeof(TWarning).IsAssignableFrom(child.GetType()))
                {
                    (child as TWarning).ShowWarning = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Resolves a service instance for a given service interface type
        /// </summary>
        /// <typeparam name="TService">Service interface type</typeparam>
        /// <returns>Service instance</returns>
        public TService ResolveService<TService>()
        {
            return (TService) Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetService(typeof(TService));
        }

        /// <summary>
        /// Gets boolean value that indicate whether the current rendering context in page editor mode
        /// </summary>
        /// <returns>True if is in page editor mode, otherwise false</returns>
        public bool IsPageEditor()
        {
            return Sitecore.Context.PageMode.IsExperienceEditor;
        }

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="error">Error message or exception</param>
        /// <param name="owner">Error owner</param>
        public void LogError(string error, object owner)
        {
            var logService = this.ResolveService<ILogService>();
            logService.Error(error, owner);
        }
    }
}
