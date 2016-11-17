using System.Web.UI.WebControls;
using DeloitteDigital.Atlas.Refactoring;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace DeloitteDigital.Atlas.Extensions
{
    [LegacyCode]
    public static class ScRepeaterExtensions
    {
        /// <summary>
        /// Return DataItem as Sitecore item
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static Item GetSitecoreItem(this RepeaterItemEventArgs args)
        {
            return args.Item.DataItem as Item;
        }

        /// <summary>
        /// Finds the and set field renderer.
        /// </summary>
        /// <param name="args">The <see cref="RepeaterItemEventArgs" /> instance containing the event data.</param>
        /// <param name="id">The id.</param>
        /// <param name="item">The item.</param>
        public static void FindAndSetFieldRenderer(this RepeaterItemEventArgs args, string id, Item item)
        {
            var ctrl = args.Item.FindControl(id) as FieldRenderer;
            if (ctrl != null)
                ctrl.Item = item;
        }

        /// <summary>
        /// Finds the and set edit frame.
        /// </summary>
        /// <param name="args">The <see cref="RepeaterItemEventArgs" /> instance containing the event data.</param>
        /// <param name="id">The id.</param>
        /// <param name="item">The item.</param>
        public static void FindAndSetEditFrame(this RepeaterItemEventArgs args, string id, Item item)
        {
            var ctrl = args.Item.FindControl(id) as EditFrame;
            if (ctrl != null)
                ctrl.DataSource = item.Paths.Path;
        }

        /// <summary>
        /// Finds the and set link.
        /// </summary>
        /// <param name="args">The <see cref="RepeaterItemEventArgs" /> instance containing the event data.</param>
        /// <param name="id">The id.</param>
        /// <param name="item">The item.</param>
        public static void FindAndSetLink(this RepeaterItemEventArgs args, string id, Item item)
        {
            var ctrl = args.Item.FindControl(id) as Link;
            if (ctrl != null)
                ctrl.Item = item;
        }

        public static void FindAndSetImage(this RepeaterItemEventArgs args, string id, Item item)
        {
            var ctrl = args.Item.FindControl(id) as global::Sitecore.Web.UI.WebControls.Image;
            if (ctrl != null)
                ctrl.Item = item;
        }
    }
}
