using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeloitteDigital.Atlas.Mvvm.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ErrorWarningControl runat=\"server\"></{0}:ErrorWarningControl>")]
    public class ErrorWarning : WebControl, IErrorWarning
    {
        protected override void RenderContents(HtmlTextWriter output)
        {
            if (ShowWarning)
            {
                output.Write("<div style=\"color:red\">{0}</div>", 
                    "There was an error occurred while trying to render this control");
            }
        }

        public bool ShowWarning
        {
            get;
            set;
        }
    }
}
