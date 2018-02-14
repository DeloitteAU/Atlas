using System;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace DeloitteDigital.Atlas.FieldRendering
{
    /// <summary>
    /// Represents an empty media field rendering
    /// </summary>
    public class EmptyMediaRenderingString : IMediaRenderingString
    {
        [Obsolete("This method is obsolete. Please use Url() or Url(MediaOptions) instead")]
        public string UrlOnly()
        {
            return string.Empty;
        }

        public string Url()
        {
            return string.Empty;
        }

        public string Url(MediaUrlOptions mediaUrlOptions)
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
