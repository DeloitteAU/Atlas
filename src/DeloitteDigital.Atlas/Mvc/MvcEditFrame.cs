using System;
using System.IO;
using System.Web.UI;
using Sitecore.Web.UI.WebControls;

namespace DeloitteDigital.Atlas.Mvc
{
    public class MvcEditFrame : IDisposable
    {
        private readonly EditFrame editFrame;
        private readonly HtmlTextWriter htmlWriter;

        public MvcEditFrame(TextWriter writer, string dataSource, string buttons)
        {
            htmlWriter = new HtmlTextWriter(writer);
            editFrame = new EditFrame { DataSource = dataSource, Buttons = buttons };
            editFrame.RenderFirstPart(htmlWriter);
        }

        public void Dispose()
        {
            editFrame.RenderLastPart(htmlWriter);
            htmlWriter.Dispose();
        }
    }
}
