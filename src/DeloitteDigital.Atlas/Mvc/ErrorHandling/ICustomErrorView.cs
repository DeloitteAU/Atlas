namespace DeloitteDigital.Atlas.Mvc.ErrorHandling
{
    /// <summary>
    /// Implement this interface on Rendering Models to customise the rendering view used for 
    /// error messages. E.g. used if the rendering does not output HTML or to change the behaviour 
    /// of ExperienceEditor vs published site.
    /// </summary>
    public interface ICustomErrorView
    {
        string ErrorViewFile { get; }
    }
}
