namespace DeloitteDigital.Atlas.Mvc.ErrorHandling
{
    /// <summary>
    /// Implement this interface on Rendering Models to prevent rendering the view renderings if an
    /// exception was thrown during the rendering process.
    /// Note: Only implement this on leave renderings (e.g. content modules) but not on containers 
    /// (e.g. layouts) since it would hide the entire layout if one nested module throws an error.
    /// </summary>
    public interface IHideOnError
    {
    }
}
