using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.FieldRendering
{
    /// <summary>
    /// Represents an empty media field rendering
    /// </summary>
    public class EmptyMediaRenderingString : IMediaRenderingString
    {
        public string UrlOnly()
        {
            return string.Empty;
        }

        public IFieldRenderingString Scale(float percent)
        {
            return this;
        }

        public MediaItem MediaItem
        {
            get
            {
                return null;
            }
        }

        public string RawValue()
        {
            return string.Empty;
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

        public override string ToString()
        {
            return string.Empty;
        }

        public Item Item
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
