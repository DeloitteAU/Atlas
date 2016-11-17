using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DeloitteDigital.Atlas.Refactoring;

namespace DeloitteDigital.Atlas.Extensions
{
    [LegacyCode]
    public static class RepeaterExtensions
    {
        /// <summary>
        /// Return true if itemtype is Item or AlternatingItem
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool IsListingItem(this RepeaterItemEventArgs e)
        {
            return (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem);
        }

        /// <summary>
        /// Find control from Repeater control
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T FindControl<T>(this RepeaterItemEventArgs args, string id) where T : Control
        {
            var retValue = args.Item.FindControl(id) as T;
            return retValue;
        }

        /// <summary>
        /// Finds the HTML control and set attribute.
        /// </summary>
        /// <param name="args">The <see cref="System.Web.UI.WebControls.RepeaterItemEventArgs"/> instance containing the event data.</param>
        /// <param name="id">The id.</param>
        /// <param name="attributeKey">The attribute key.</param>
        /// <param name="attributeValue">The attribute value.</param>
        /// <returns></returns>
        public static HtmlControl FindHtmlControlAndSetAttribute(this RepeaterItemEventArgs args, string id, string attributeKey, string attributeValue)
        {
            var ctrl = args.Item.FindControl(id) as HtmlControl;
            if (ctrl != null)
            {
                ctrl.Attributes.Add(attributeKey, attributeValue);
            }
            return ctrl;
        }

        /// <summary>
        /// Find control from Repeater Control and apply string value
        /// </summary>
        /// <param name="args"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        public static void FindAndSetLiteralControl(this RepeaterItemEventArgs args, string id, string value)
        {
            var l = args.Item.FindControl(id) as Literal;
            if (l != null)
            {
                l.Text = value;
            }
        }

        /// <summary>
        /// Find control from Repeater control and apply Navigation URL and Navigation Text
        /// </summary>
        /// <param name="args"></param>
        /// <param name="id"></param>
        /// <param name="textValue"></param>
        /// <param name="linkValue"></param>
        /// <param name="cssClass"></param>
        public static void FindAndSetHyperLinkControl(this RepeaterItemEventArgs args, string id, string textValue, string linkValue, string cssClass = "")
        {
            var l = (HyperLink)args.Item.FindControl(id);
            l.Text = textValue;
            l.NavigateUrl = linkValue;
            if (cssClass != "")
                l.CssClass = cssClass;
        }

        /// <summary>
        /// Find control from repeater control and change visibility
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <param name="id"></param>
        public static void FindAndHideControl<T>(this RepeaterItemEventArgs args, string id) where T : Control
        {
            var retValue = args.Item.FindControl(id) as T;
            if (retValue != null) retValue.Visible = false;
        }
    }
}
