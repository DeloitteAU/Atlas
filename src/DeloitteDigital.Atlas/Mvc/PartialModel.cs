namespace DeloitteDigital.Atlas.Mvc
{
    public class PartialModel<TViewModel>
    {
        public TViewModel ViewModel { get; set; }

        public PartialModel(TViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        /// <summary>
        /// Gets boolean value that indicate whether the current rendering context in page editor mode
        /// </summary>
        /// <returns>True if is in page editor mode, otherwise false</returns>
        public bool IsPageEditor()
        {
            return Sitecore.Context.PageMode.IsExperienceEditor;
        }
    }
}
