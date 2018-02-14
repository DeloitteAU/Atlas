using Sitecore.Data.Items;
using Sitecore.Resources.Media;

namespace DeloitteDigital.Atlas.FieldRendering
{
    /// <summary>
    /// Represents a field rendering wrapper for media field type (image) as a string
    /// </summary>
    public interface IMediaRenderingString : IFieldRenderingString
    {
        /// <summary>
        /// Instructs the renderer to render the media URL only, not the image control or page editor control
        /// </summary>
        /// <returns>The field's media url</returns>
        string UrlOnly();

        /// <summary>
        /// Instructs the renderer to render the medial URL with default options
        /// </summary>
        /// <returns>The field's media URL</returns>
        string Url();

        /// <summary>
        /// Instructs the renderer to render the medial URL with specified url options
        /// </summary>
        /// <returns>The field's media URL</returns>
        string Url(MediaUrlOptions mediaUrlOptions);

        /// <summary>
        /// Scales the image size by a specified percentage
        /// </summary>
        /// <param name="percent">The percent.</param>
        /// <returns>itself.</returns>
        IFieldRenderingString Scale(float percent);

        MediaItem MediaItem { get; }
    }
}
