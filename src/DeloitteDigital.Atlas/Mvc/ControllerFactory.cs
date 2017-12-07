using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DeloitteDigital.Atlas.Mvc
{
    /// <summary>
    /// Overrides the default controller factory and injects dependencies to controller instance if any using service locator
    /// </summary>
    public class ControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Retrieves the controller instance for the specified request context and controller
        /// type using the ServiceLocator to resolve the controller dependencies.
        /// </summary>
        /// <param name="reqContext">Request context</param>
        /// <param name="controllerType">Controller type</param>
        /// <returns>The controller instance</returns>
        protected override IController GetControllerInstance(RequestContext reqContext, Type controllerType)
        {
            var controller = default(IController);

            // Controller not found
            if (controllerType == null)
                throw new HttpException(404, string.Format(
                    "The controller for path '{0}' could not be found or it does not implement IController.",
                    reqContext.HttpContext.Request.Path));

            // Controller type does not implement IController interface
            if (!typeof(IController).IsAssignableFrom(controllerType))
                throw new ArgumentException(string.Format(
                    "Type requested is not a controller: {0}", controllerType.Name),
                    "controllerType");
            try
            {
                // Create an instance from the controller type and resolve its dependencies
                controller = Sitecore.DependencyInjection.ServiceLocator.ServiceProvider.GetService(controllerType) as IController;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format(
                    "Error resolving controller {0}", controllerType.Name), ex);
            }

            return controller;
        }
    }
}


 
