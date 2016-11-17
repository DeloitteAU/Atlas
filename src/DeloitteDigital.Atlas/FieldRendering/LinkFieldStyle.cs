namespace DeloitteDigital.Atlas.FieldRendering
{
    /// <summary>
    /// The link field style.
    /// </summary>
    public class LinkFieldStyle
    {
        /// <summary>
        /// The default link style.
        /// </summary>
        private static readonly LinkFieldStyle DefaultLinkStyle = new LinkFieldStyle { PrimaryButtonClass = string.Empty, SecondaryButtonClass = string.Empty };

        /// <summary>
        /// Gets the default.
        /// </summary>
        public static LinkFieldStyle Default
        {
            get
            {
                return DefaultLinkStyle;
            }
        }

        /// <summary>
        /// Gets or sets the primary button class.
        /// </summary>
        public string PrimaryButtonClass { get; set; }

        /// <summary>
        /// Gets or sets the secondary button class.
        /// </summary>
        public string SecondaryButtonClass { get; set; }
    }
}
