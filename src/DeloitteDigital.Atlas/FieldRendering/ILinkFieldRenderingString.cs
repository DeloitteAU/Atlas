using Sitecore.Data.Fields;

namespace DeloitteDigital.Atlas.FieldRendering
{
    /// <summary>
    /// The LinkFieldRenderingString interface.
    /// </summary>
    public interface ILinkFieldRenderingString : IFieldRenderingString
    {
        /// <summary>
        /// Gets the URL only
        /// </summary>
        /// <returns>The field's url</returns>
        string Url { get; }

        /// <summary>
        /// Gets the target.
        /// </summary>
        string Target { get; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Gets the link class
        /// </summary>
        string Class { get; }

        /// <summary>
        /// Gets the link description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        string Text { get; }
        
        /// <summary>
        /// Gets the target page title.
        /// </summary>
        string TargetPageTitle { get; }

        /// <summary>
        /// Gets or sets the link field.
        /// </summary>
        LinkField LinkField { get; }

        /// <summary>
        /// Gets the query string
        /// </summary>
        string QueryString { get; }
    }
}
