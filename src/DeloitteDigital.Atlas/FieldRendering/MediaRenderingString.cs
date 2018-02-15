using System;
using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace DeloitteDigital.Atlas.FieldRendering
{
    /// <summary>
    /// Performs late bound field rendering for media field type
    /// </summary>
    public class MediaRenderingString : FieldRenderingString, IMediaRenderingString
    {
        /// <summary>
        /// Initialises a new instance of the MediaRenderingString class
        /// </summary>
        /// <param name="item">Sitecore item</param>
        /// <param name="fieldName">Field name</param>
        public MediaRenderingString(Item item, string fieldName)
            : base(item, fieldName)
        {
        }

        /// <summary>
        /// Instructs the renderer to render the media URL only, not the image control or page editor control.
        /// This method will not respect the .ashx setting in the Sitecore configuration
        /// </summary>
        /// <returns>The field's media url</returns>
        [Obsolete("This method is obsolete. Please use Url() or Url(MediaOptions) instead")]
        public string UrlOnly()
        {
            return Url(MediaUrlOptions.GetShellOptions());
        }

        /// <summary>
        /// Instructs the render to render the media URL only using the default Sitecore Media URL rendering options
        /// </summary>
        public string Url()
        {
            return Url(new MediaUrlOptions());
        }

        /// <summary>
        /// Instructs the render to render the media URL only using the provided media url options
        /// </summary>
        public string Url(MediaUrlOptions mediaUrlOptions)
        {
            var imageField = (Sitecore.Data.Fields.ImageField)Item.Fields[FieldName];

            if (imageField?.MediaItem != null)
            {
                mediaUrlOptions.Width = Attributes.ContainsKey("width") ? (int)Attributes["width"] : mediaUrlOptions.Width;
                mediaUrlOptions.Height = Attributes.ContainsKey("height") ? (int)Attributes["height"] : mediaUrlOptions.Height;
                return MediaManager.GetMediaUrl(imageField.MediaItem, mediaUrlOptions);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Scales the image size by a specified percentage
        /// </summary>
        /// <param name="percent">The percent.</param>
        /// <returns>
        /// itself.
        /// </returns>
        public IFieldRenderingString Scale(float percent)
        {
            var imageField = (Sitecore.Data.Fields.ImageField)this.Item.Fields[this.FieldName];

            this.Attributes["height"] = (int)(int.Parse(imageField.Height) * percent);
            this.Attributes["width"] = (int)(int.Parse(imageField.Width) * percent);

            return this;
        }

        public MediaItem MediaItem
        {
            get
            {
                var imageField = (Sitecore.Data.Fields.ImageField)this.Item.Fields[this.FieldName];
                return imageField == null ? null : imageField.MediaItem;
            }
        }
    }
}
