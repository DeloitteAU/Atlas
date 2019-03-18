using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DeloitteDigital.Atlas.FieldRendering;

namespace DeloitteDigital.Atlas.Mvc
{
    internal class EmptyMvcLink : IDisposable
    {
        public void Dispose()
        {
        }
    }

    internal class MvcLink : IDisposable
    {
        private readonly ViewContext viewContext;
        private readonly TagBuilder tagBuilder;

        public MvcLink(ViewContext viewContext, ILinkFieldRenderingString linkField, string alternateTag, string linkTagClass = null)
        {
            this.viewContext = viewContext;

            if (!string.IsNullOrWhiteSpace(linkField?.Url))
            {
                // link given - render an anchor tag
                this.tagBuilder = new TagBuilder("a");
                this.tagBuilder.Attributes.Add("href", linkField.Url);
                // add optional attributes
                if (!string.IsNullOrWhiteSpace(linkField.Target))
                    this.tagBuilder.Attributes.Add("target", linkField.Target);
                if (!string.IsNullOrWhiteSpace(linkField.Description))
                    this.tagBuilder.Attributes.Add("title", linkField.Description);

                if (!string.IsNullOrWhiteSpace(linkField.Class) && !string.IsNullOrWhiteSpace(linkTagClass))
                {
                    var classes = new HashSet<string>();
                    foreach (var linkClass in linkField.Class.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                        classes.Add(linkClass);
                    foreach (var tagClass in linkTagClass.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                        classes.Add(tagClass);
                    this.tagBuilder.Attributes.Add("class", string.Join(" ", classes));
                } else if (!string.IsNullOrWhiteSpace(linkField.Class))
                    this.tagBuilder.Attributes.Add("class", linkField.Class);
                else if (!string.IsNullOrWhiteSpace(linkTagClass))
                    this.tagBuilder.Attributes.Add("class", linkTagClass);
            }
            else if (!string.IsNullOrWhiteSpace(alternateTag))
            {
                // no link given - render the alternate tag if provided
                this.tagBuilder = new TagBuilder(alternateTag);
            }

            // render the opening tag
            if (this.tagBuilder != null)
                viewContext.Writer.Write(this.tagBuilder.ToString(TagRenderMode.StartTag));
        }

        public void Dispose()
        {
            // render the closing tag
            if (this.tagBuilder != null)
                this.viewContext.Writer.Write(this.tagBuilder.ToString(TagRenderMode.EndTag));
        }
    }
}
