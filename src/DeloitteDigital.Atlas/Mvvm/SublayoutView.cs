using System;
using System.Web.UI;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mvvm
{
    using Sitecore = global::Sitecore;
    /// <summary>
    /// Serves as the sub-layout view base class
    /// </summary>
    /// <typeparam name="TViewModel">View model type</typeparam>
    public abstract class SublayoutView<TViewModel> : UserControl
    {
        /// <summary>
        /// View control helper
        /// </summary>
        private ViewControlHelper viewHelper = new ViewControlHelper();

        /// <summary>
        /// Gets or sets view model object
        /// </summary>
        protected TViewModel ViewModel { get; private set; }

        /// <summary>
        /// Gets or sets the value that indicates whether an error has occurred while trying to initialise the view model object
        /// </summary>
        protected bool HasError { get; private set; }

        /// <summary>
        /// Gets the boolean value that indicates whether it's in the experience editor mode
        /// </summary>
        protected bool IsExperienceEditor => viewHelper.IsExperienceEditor;

        /// <summary>
        /// Resolves a service instance
        /// </summary>
        /// <typeparam name="TService">Service interface type</typeparam>
        /// <returns>Service instance</returns>
        protected TService Resolve<TService>()
        {
            return this.viewHelper.ResolveService<TService>();
        }

        protected Item SiteRootItem
        {
            get { return this.viewHelper.GetSiteItem(); }
        }

        protected Item SiteStartItem
        {
            get { return this.viewHelper.GetSiteStartItem(); }
        }

        /// <summary>
        /// Runs when control initialises
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnInit(EventArgs e)
        {
            try
            {
                var dataSource = this.viewHelper.GetDataSource(this);

                if (!DisableDataSourceValidation && (dataSource == null))
                {
                    // Display no data source reactive warning control if available
                    this.viewHelper.ShowReactiveWarning<IDataSourceWarning>(this);
                }

                // Initialise view model instance by the inheriting sub-layout
                ViewModel = InitialiseViewModel(dataSource);
            }
            catch (Exception ex)
            {
                // Log unhandled exception to Sitecore log
                Sitecore.Diagnostics.Log.Error(ex.ToString(), this);
                // Display error reactive warning control if available
                this.viewHelper.ShowReactiveWarning<IErrorWarning>(this);
                // Turn on the HasError flag
                HasError = true;
            }

            base.OnInit(e);
        }

        /// <summary>
        /// When overriden by derrived class, initialises the view model object
        /// </summary>
        /// <param name="dataSource">Data source item</param>
        /// <returns>View model object</returns>
        protected abstract TViewModel InitialiseViewModel(Item dataSource);

        /// <summary>
        /// When overriden by derrived class, return "true" to disable data source validation
        /// </summary>
        protected virtual bool DisableDataSourceValidation
        {
            get { return false; }
        }
    }
}
