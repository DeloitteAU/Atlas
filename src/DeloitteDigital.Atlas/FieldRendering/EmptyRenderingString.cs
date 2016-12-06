using System;

namespace DeloitteDigital.Atlas.FieldRendering
{
    /// <summary>
    /// Represents an empty field rendering
    /// </summary>
    public class EmptyRenderingString : IFieldRenderingString
    {
        private string emptyStringValue = string.Empty;

        public EmptyRenderingString() { }

        public EmptyRenderingString(string emptyStringValue)
        {
            this.emptyStringValue = emptyStringValue;
        }

        public IFieldRenderingString WithAttribute(string attribute, object value)
        {
            return this;
        }

        public IFieldRenderingString DisableWebEdit()
        {
            return this;
        }

        public IFieldRenderingString DefaultValueIfEmpty(string defaultValue)
        {
            return this;
        }

        public string RawValue()
        {
            return emptyStringValue;
        }

        public override string ToString()
        {
            return emptyStringValue;
        }


        public Sitecore.Data.Items.Item Item
        {
            get { return null; }
        }

        public string FieldName
        {
            get { return string.Empty; }
        }

        public string ToHtmlString()
        {
            return string.Empty;
        }
    }
}
