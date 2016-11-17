namespace DeloitteDigital.Atlas.Mvc
{
    public class ContextRenderingModel<TViewModel> : RenderingModel<TViewModel>
        where TViewModel : class
    {
        protected override TViewModel InitialiseViewModel(global::Sitecore.Mvc.Presentation.Rendering rendering, DataSource dataSource)
        {
            return this.Map<TViewModel>(rendering.Item);
        }
    }
}
