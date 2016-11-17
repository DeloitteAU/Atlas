using System.Web;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.FieldRendering
{
    /// <summary>
    /// Represents a field rendering wrapper as a string
    /// </summary>
    /// <remarks>
    /// Recommended Sitecore types:
    /// + Single-Line Text
    /// + Multi-Line Text
    /// + Rich Text
    /// </remarks>
    public interface IFieldRenderingString : IHtmlString
    {
        /// <summary>
        /// Renders the field as raw value
        /// </summary>
        /// <returns>The field raw value as stored in Sitecore database</returns>
        string RawValue();

        /// <summary>
        /// Gets the item that the field rendering is associated with
        /// </summary>
        Item Item { get; }

        /// <summary>
        /// Gets the field name of the field rendering
        /// </summary>
        string FieldName { get; }

        /// <summary>
        /// Adds an attribute (usually recognised by the corresponding Sitecore field type) to be rendered when rendering
        /// </summary>
        /// <param name="attribute">Attribute name</param>
        /// <param name="value">Attribute value</param>
        /// <returns>The current field rendering string instance</returns>
        IFieldRenderingString WithAttribute(string attribute, object value);        

        /// <summary>
        /// Disable rendering of editable component
        /// </summary>
        /// <returns>The <see cref="IFieldRenderingString"/>.</returns>
        IFieldRenderingString DisableWebEdit();

        /// <summary>
        /// Adds a default value if the output is empty
        /// </summary>
        /// <param name="defaultValue">The default value to use if output is empty.</param>
        /// <returns>The <see cref="IFieldRenderingString"/>.</returns>
        IFieldRenderingString DefaultValueIfEmpty(string defaultValue);
    }
}
