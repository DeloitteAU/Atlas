using System;

namespace DeloitteDigital.Atlas.Mvc.ErrorHandling
{
    public class ErrorRenderingModel
    {
        public string RenderingName { get; private set; }

        public Exception Exception { get; private set; }

        public ErrorRenderingModel(string renderingName, Exception exception)
        {
            RenderingName = renderingName;
            Exception = exception;
        }
    }
}
