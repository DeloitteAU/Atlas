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
        /// Instructs the renderer to render the media URL only, not the image control or page editor control
        /// </summary>
        /// <returns>The field's media url</returns>
        public string UrlOnly()
        {
            var imageField = (Sitecore.Data.Fields.ImageField)this.Item.Fields[this.FieldName];

            if (imageField != null && imageField.MediaItem != null)
            {
                // TODO: add support for more media option attributes, for UrlOnly rendering                
                ////var mediaUrlOptions = MediaUrlOptions.GetThumbnailOptions(imageField.MediaItem);
                var mediaUrlOptions = MediaUrlOptions.GetShellOptions();
                mediaUrlOptions.Width = this.Attributes.ContainsKey("width") ? (int)this.Attributes["width"] : mediaUrlOptions.Width;
                mediaUrlOptions.Height = this.Attributes.ContainsKey("height") ? (int)this.Attributes["height"] : mediaUrlOptions.Height;
                return MediaManager.GetMediaUrl(imageField.MediaItem, mediaUrlOptions);
            }
            else
            {
                return String.Empty;
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
