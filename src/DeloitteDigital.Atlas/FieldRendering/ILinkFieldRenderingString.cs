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
        /// Gets the link field styled as a button
        /// </summary>
        /// <param name="primary">Whether the button is primary.</param>
        /// <returns>
        /// The <see cref="ILinkFieldRenderingString"/> styled as a button
        /// </returns>
        ILinkFieldRenderingString AsButton(bool primary = true);

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
    }
}
