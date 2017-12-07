using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Templates;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.Resources.Media;
using Sitecore.SecurityModel;
using Sitecore.Web.UI.WebControls;

namespace DeloitteDigital.Atlas.Extensions
{

    /// <summary>
    /// Provides extension methods on the Sitecore.Data.Items.Item type
    /// </summary>
    public static class ItemExtensions
    {
        /// <summary>
        /// Gets field value from item with page editor support, and parameters for the field renderer
        /// </summary>
        /// <param name="item">Sitecore item</param>
        /// <param name="fieldName">Field name</param>
        /// <param name="pageEditorSupport">True if page editor support is required, otherwise false</param>
        /// <param name="attributes">Field renderer parameters</param>
        /// <returns>Field value or field renderer rendered control</returns>
        public static string GetFieldValue(this Item item, string fieldName, bool pageEditorSupport = false, IDictionary<string, string> attributes = null)
        {
            if (pageEditorSupport)
            {
                var fieldRenderer = new FieldRenderer();
                fieldRenderer.Item = item;
                fieldRenderer.FieldName = fieldName;
                if (attributes != null)
                {
                    fieldRenderer.Parameters = string.Join("&",
                        attributes.Keys.Select(k => string.Format("{0}={1}", k, attributes[k])));
                }
                return fieldRenderer.Render();
            }
            else
            {
                var field = item.Fields[fieldName];
                return field != null ? field.Value : string.Empty;
            }
        }

        /// <summary>
        /// Gets list field value as a collection of items
        /// </summary>
        /// <param name="item">Sitecore item</param>
        /// <param name="fieldName">Field name</param>
        /// <returns>An item collection from the list field</returns>
        public static Item[] GetListFieldValueItems(this Item item, string fieldName)
        {
            var items = new Item[0];
            var field = item.Fields[fieldName];

            if (field != null)
            {
                var listField = (MultilistField)field;
                if (listField != null)
                {
                    items = listField.GetItems();
                }
            }

            return items;
        }

        public static string GetItemUrl(this Item item, string siteName = "", string hostName = "")
        {
            if (string.IsNullOrEmpty(siteName))
            {
                return LinkManager.GetItemUrl(item);
            }
            else
            {
                var itemUrl = string.Empty;
                var originalSiteName = Context.Site.Name;
                Context.SetActiveSite(siteName);
                itemUrl = string.Format("//{0}{1}", hostName, LinkManager.GetItemUrl(item));
                Context.SetActiveSite(originalSiteName);
                return itemUrl;
            }
        }

        public static bool GetFieldValueAsBool(this Item item, string fieldName)
        {
            var fieldValue = item.GetFieldValue(fieldName, false);
            return fieldValue.Equals("1");
        }

        public static string GetFieldValueAsDateTime(this Item item, string fieldName, string format = "")
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "dd MMM yyyy";
            }
            var fieldValue = item.GetFieldValue(fieldName);
            var dateValue = DateUtil.IsoDateToDateTime(fieldValue, DateTime.MinValue);
            return dateValue.ToString(format);
        }

        public static string GetImageFieldMediaUrl(this Item item, string fieldName, int width = 0, int height = 0)
        {
            var imageField = (ImageField)item.Fields[fieldName];

            if (imageField?.MediaItem != null)
            {
                var mediaUrlOptions = MediaUrlOptions.GetThumbnailOptions(imageField.MediaItem);
                mediaUrlOptions.Width = width > 0 ? width : mediaUrlOptions.Width;
                mediaUrlOptions.Height = height > 0 ? height : mediaUrlOptions.Height;
                mediaUrlOptions.UseItemPath = true;
                return MediaManager.GetMediaUrl(imageField.MediaItem, mediaUrlOptions);
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetLinkFieldItemFieldValue(this Item item, string linkFieldName, string linkItemFieldName)
        {
            var linkFieldValue = item.GetFieldValue(linkFieldName);
            var linkItemId = ID.Null;

            if (ID.TryParse(linkFieldValue, out linkItemId))
            {
                var linkItem = item.Database.GetItem(linkItemId);

                if (linkItem != null)
                {
                    return linkItem.GetFieldValue(linkItemFieldName);
                }
            }

            return string.Empty;
        }

        public static Item GetLinkFieldItem(this Item item, string fieldName)
        {
            var linkFieldValue = item.GetFieldValue(fieldName);
            var linkItemId = ID.Null;

            if (ID.TryParse(linkFieldValue, out linkItemId))
            {
                return item.Database.GetItem(linkItemId);
            }

            return null;
        }

        public static int GetFieldValueAsInt(this Item item, string fieldName)
        {
            var value = item.GetFieldValue(fieldName);
            var intValue = 0;
            int.TryParse(value, out intValue);
            return intValue;
        }

        public static Item GetFieldValueAsItem(this Item item, string fieldName)
        {
            var fieldValue = item.GetFieldValue(fieldName);

            return string.IsNullOrEmpty(fieldValue)
                ? null
                : item.Database.GetItem(fieldValue);
        }

        public static DateTime? GetDateTime(this Item item, string fieldName)
        {
            DateTime? dateTime = null;
            if (item.Fields[fieldName] != null)
            {
                DateField field = item.Fields[fieldName];
                dateTime = field.DateTime;
            }
            return dateTime;
        }

        public static bool IsChecked(this Item datasource, string fieldName)
        {
            if (datasource == null) return false;

            var field = datasource.Fields[fieldName];

            if (field == null) return false;

            var checkbox = (CheckboxField)field;
            var check = checkbox != null && checkbox.Checked;

            return check;
        }

        public static bool IsMediaItem(this Item item)
        {
            if (item != null && item.Paths != null)
            {
                return item.Paths.IsMediaItem;
            }

            return false;
        }

        /// <summary>
        /// Returns true if Template inherits from given template id
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <param name="id">Id of template to check for</param>
        /// <returns>True if item inherits from template</returns>
        public static bool InheritsFromTemplate(this Item item, ID id)
        {
            if (item == null || StandardValuesManager.IsStandardValuesHolder(item)) return false;

            // Check if it is directly inheriting
            if (item.TemplateID == id) return true;

            // Else check the base templates.
            var template = Sitecore.Data.Managers.TemplateManager.GetTemplate(item);

            if (template == null) return false;

            foreach (Template template2 in template.GetBaseTemplates())
            {
                if (template2 == null) continue;
                if (template2.ID.Guid == id.Guid) return true;
            }

            return false;
        }

        public static bool HasRendering(this Item item)
        {
            // Resolve device
            var device = DeviceItem.ResolveDevice(item.Database);
            if (device == null) return false;

            return item.Visualization.GetRenderings(device, false).Any();
        }

        public static IEnumerable<Item> GetDescendantsThatInheritTemplate(this Item rootItem, TemplateID templateId)
        {
            Assert.ArgumentNotNull(rootItem, "root item");
            Assert.ArgumentNotNull(templateId, "template id");
            return rootItem.Axes.GetDescendants().Where(item => item.InheritsFromTemplate(templateId));
        }

    }
}
