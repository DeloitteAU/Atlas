using System.Collections.Generic;
using System.Linq;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Web.UI.WebControls;

namespace DeloitteDigital.Atlas.FieldRendering
{

    /// <summary>
    /// Performs late bound field rendering for generic field type
    /// </summary>
    public class FieldRenderingString : IFieldRenderingString
    {
        /// <summary>
        /// Gets or sets the default value if output is empty
        /// </summary>
        protected string DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use  the default value
        /// </summary>
        protected bool UseDefaultValue { get; set; }

        /// <summary>
        /// Gets or sets corresponding sitecore Item
        /// </summary>
        public Item Item { get; set; }

        /// <summary>
        /// Gets or sets Item's field name to render
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Gets or sets attribute dictionary
        /// </summary>
        protected IDictionary<string, object> Attributes { get; set; }

        /// <summary>
        /// Initialises a new instance of the FieldRenderingString class
        /// </summary>
        /// <param name="item">Sitecore item</param>
        /// <param name="fieldName">Field name</param>
        public FieldRenderingString(Item item, string fieldName)
        {
            Assert.IsNotNull(item, "item must not be null");
            Assert.IsNotNullOrEmpty(fieldName, "fieldName must not be null or empty");
            this.Item = item;
            this.FieldName = fieldName;
            this.Attributes = new Dictionary<string, object>();
        }

        /// <summary>
        /// Adds an attribute (usually recognised by the corresponding Sitecore field type) to be rendered when rendering
        /// </summary>
        /// <param name="attribute">Attribute name</param>
        /// <param name="value">Attribute value</param>
        /// <returns>The current field rendering string instance</returns>
        public IFieldRenderingString WithAttribute(string attribute, object value)
        {
            this.Attributes[attribute] = value;
            return this;
        }

        public IFieldRenderingString DisableWebEdit()
        {
            return this.WithAttribute("disable-web-editing", true);
        }

        public IFieldRenderingString DefaultValueIfEmpty(string defaultValue)
        {
            this.DefaultValue = defaultValue;
            this.UseDefaultValue = !string.IsNullOrWhiteSpace(this.DefaultValue);
            return this;
        }

        /// <summary>
        /// Renders the field as raw value
        /// </summary>
        /// <returns>The field raw value as stored in Sitecore database</returns>
        public string RawValue()
        {
            return this.RenderField(true);
        }

        /// <summary>
        /// Overrides the default ToString() method to render the field value as a string
        /// </summary>
        /// <returns>Field value as a string</returns>
        public override string ToString()
        {
            return this.RenderField(false);
        }

        /// <summary>
        /// Renders the field value when called by ToString()
        /// </summary>
        /// <param name="asRawValue">Whether to render raw value</param>
        /// <returns>Field value as a string</returns>
        protected virtual string RenderField(bool asRawValue)
        {
            var result = string.Empty;

            if (asRawValue)
            {
                var field = this.Item.Fields[this.FieldName];
                if (field != null)
                {
                    result = field.Value;
                }
            }
            else
            {
                var fieldRenderer = new FieldRenderer();
                fieldRenderer.Item = this.Item;
                fieldRenderer.FieldName = this.FieldName;
                if (this.Attributes.Count > 0)
                {
                    fieldRenderer.Parameters = string.Join("&", this.Attributes.Keys.Select(k => string.Format("{0}={1}", k, this.Attributes[k])));
                }

                result = fieldRenderer.Render();
            }

            if (this.UseDefaultValue && string.IsNullOrWhiteSpace(result))
            {
                result = this.DefaultValue;
            }

            return result;
        }

        /// <summary>
        /// Implementation of IHtmlString.ToHtmlString
        /// </summary>
        /// <returns>Rendered string</returns>
        public string ToHtmlString()
        {
            return this.ToString();
        }
    }
}
