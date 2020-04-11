using System;
using System.Web.UI;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.Mvvm
{
    /// <summary>
    /// Serves as the layout view base class
    /// </summary>
    /// <typeparam name="TViewModel">View model type</typeparam>
    public abstract class LayoutView<TViewModel> : Page
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

        protected Item SiteRootItem => this.viewHelper.GetSiteItem();

        protected Item SiteStartItem => this.viewHelper.GetSiteStartItem();

        /// <summary>
        /// Runs when control initialises
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnInit(EventArgs e)
        {
            try
            {
                // Initialise view model instance by the inheriting sub-layout
                ViewModel = InitialiseViewModel();
            }
            catch (Exception ex)
            {
                // Log unhandled exception to Sitecore log
                this.viewHelper.LogError(ex.ToString(), this);

                // Display error reactive warning control if available
                this.viewHelper.ShowReactiveWarning<IErrorWarning>(this);

                // Turn on the HasError flag
                HasError = true;
            }

            base.OnInit(e);
        }

        /// <summary>
        /// Runs on control loads
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnLoad(EventArgs e)
        {
            Page.DataBind();
            base.OnLoad(e);
        }

        /// <summary>
        /// When overriden by derrived class, initialises the view model object
        /// </summary>
        /// <returns>View model object</returns>
        protected abstract TViewModel InitialiseViewModel();
    }
}
