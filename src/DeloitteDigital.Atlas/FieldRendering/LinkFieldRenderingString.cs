using Sitecore.Data.Fields;
using Sitecore.Data.Items;

namespace DeloitteDigital.Atlas.FieldRendering
{
    /// <summary>
    /// The link field rendering string.
    /// </summary>
    public class LinkFieldRenderingString : FieldRenderingString, ILinkFieldRenderingString
    {
        /// <summary>
        /// Initializes a new instance of the LinkFieldRenderingString class
        /// </summary>
        /// <param name="item">Sitecore item</param>
        /// <param name="fieldName">Field name</param>
        public LinkFieldRenderingString(Item item, string fieldName)
            : base(item, fieldName)
        {
            this.LinkField = this.Item.Fields[this.FieldName];
        }

        /// <summary>
        /// Gets or sets the link field.
        /// </summary>
        public LinkField LinkField { get; set; }

        /// <summary>
        /// Gets the URL only
        /// </summary>
        public string Url
        {
            get
            {
                return this.LinkField == null ? string.Empty : this.LinkField.GetFriendlyUrl();
            }
        }

        /// <summary>
        /// Gets the target page title
        /// </summary>
        public string TargetPageTitle
        {
            get
            {
                if (this.LinkField == null || this.LinkField.TargetItem == null)
                {
                    return string.Empty;
                }

                return string.IsNullOrEmpty(this.LinkField.Text) ? this.LinkField.TargetItem.DisplayName : this.LinkField.Text;
            }
        }

        /// <summary>
        /// Gets the target page title
        /// </summary>
        public string Target
        {
            get
            {
                if (this.LinkField == null)
                {
                    return string.Empty;
                }

                return this.LinkField.Target;
            }
        }

        /// <summary>
        /// Gets the link title
        /// </summary>
        public string Title
        {
            get
            {
                if (this.LinkField == null)
                {
                    return string.Empty;
                }

                return this.LinkField.Title;
            }
        }

        /// <summary>
        /// Gets the link text
        /// </summary>
        public string Text
        {
            get
            {
                if (this.LinkField == null)
                {
                    return string.Empty;
                }

                return this.LinkField.Text;
            }
        }


        public string Class
        {
            get { return LinkField.Class; }
        }

        public string Description
        {
            get { return LinkField.Text; }
        }

        /// <summary>
        /// Gets the link query string
        /// </summary>
        public string QueryString
        {
            get
            {
                if (this.LinkField == null)
                {
                    return string.Empty;
                }

                return this.LinkField.QueryString;
            }
        }
    }
}
