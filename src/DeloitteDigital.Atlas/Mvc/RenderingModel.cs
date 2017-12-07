using System;
using DeloitteDigital.Atlas.Caching;
using DeloitteDigital.Atlas.FieldRendering;
using DeloitteDigital.Atlas.Logging;
using DeloitteDigital.Atlas.Mapping;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Mvc.Presentation;

namespace DeloitteDigital.Atlas.Mvc
{
    public abstract class RenderingModel<TViewModel> : RenderingModel
        where TViewModel : class
    {
        public TViewModel ViewModel { get; private set; }

        public bool HasError { get; set; }

        public virtual string ErrorMessage { get; set; }

        public string RenderingId
        {
            get
            {
                return string.Format("{0}{1}",
                    this.Rendering.RenderingItem.Name.Replace(" ", "_"),
                    this.Rendering.UniqueId.ToString().Substring(0, 6));
            }
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
        protected Item CurrentItem
        {
            get
            {
                return Sitecore.Context.Item;
            }
        }

        /// <summary>
        /// Gets boolean value that indicate whether the current rendering context in page editor mode
        /// </summary>
        /// <returns>True if is in page editor mode, otherwise false</returns>
        public bool IsPageEditor()
        {
            return Context.PageMode.IsExperienceEditor;
        }

        public bool HasValueOrIsPageEditor(IFieldRenderingString fieldRenderingString)
        {
            return IsPageEditor() || HasValue(fieldRenderingString);
        }

        public bool HasValue(IFieldRenderingString fieldRenderingString)
        {
            return (!string.IsNullOrEmpty(fieldRenderingString?.ToString()));
        }

        public bool HasValueOrIsPageEditor(IMediaRenderingString fieldRenderingString)
        {
            return IsPageEditor() || HasValue(fieldRenderingString);
        }

        public bool HasValue(IMediaRenderingString fieldRenderingString)
        {
            return (fieldRenderingString?.MediaItem != null);
        }

        /// <summary>
        /// Initialises the rendering model
        /// </summary>
        /// <param name="rendering"></param>
        public override void Initialize(Rendering rendering)
        {
            try
            {
                var dataSourceParser = new DataSourceParser();
                var dataSource = dataSourceParser.Parse(rendering.DataSource);
                ViewModel = InitialiseViewModel(rendering, dataSource);
                base.Initialize(rendering);
            }
            catch (Exception ex)
            {
                ILogService logService = null;

                try
                {
                    logService = (ILogService)Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetService(typeof(ILogService));
                }
                catch
                {
                    // The ILogService interface has not been configured - can't log
                }

                if (logService != null)
                {
                    logService.Error(ex.ToString(), this);
                }

                this.HasError = true;
                this.ErrorMessage = ex.Message;
            }
        }

        /// <summary>
        /// Initialises an instance of the TViewModel type
        /// </summary>
        /// <param name="rendering">Rendering context</param>
        /// <param name="dataSource">Data source</param>
        /// <returns>View model object</returns>
        protected abstract TViewModel InitialiseViewModel(Rendering rendering, DataSource dataSource);

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
    }
}
