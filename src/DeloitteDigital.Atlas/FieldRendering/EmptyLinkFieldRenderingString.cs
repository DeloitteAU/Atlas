using Sitecore.Data.Fields;

namespace DeloitteDigital.Atlas.FieldRendering
{
    public class EmptyLinkFieldRenderingString : ILinkFieldRenderingString
    {
        public string Url
        {
            get { return string.Empty; }
        }

        public string Target
        {
            get { return string.Empty; }
        }

        public string Title
        {
            get { return string.Empty; }
        }

        public string Text
        {
            get { return string.Empty; }
        }

        public string TargetPageTitle
        {
            get { return string.Empty; }
        }

        public LinkField LinkField
        {
            get { return null; }
            }

        public string RawValue()
        {
            return string.Empty;
        }

        public Sitecore.Data.Items.Item Item
        {
            get { return null; }
        }

        public string FieldName
        {
            get { return string.Empty; }
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

        public string ToHtmlString()
        {
            return string.Empty;
        }

        public string Class
        {
            get { return string.Empty; }
        }

        public string Description
        {
            get { return string.Empty; }
        }

        public string QueryString
        {
            get { return string.Empty; }
        }
    }
}
