using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Routing;
using WebForLocalizedUrl.Routing;
namespace WebForLocalizedUrl.Routing
{
    public class TranslatedRoute : Route
    {

        public List<ControllerTranslation> Controllers { get; private set; }

        public TranslatedRoute(string url, RouteValueDictionary defaults, List<ControllerTranslation> controllers, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
            this.Controllers = controllers;
        }

        public TranslatedRoute(string url, RouteValueDictionary defaults, List<ControllerTranslation> controllers, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
            this.Controllers = controllers;
        }

        /// <summary>
        /// Translate URL to route
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData routeData = base.GetRouteData(httpContext);
            if (routeData == null) return null;

            string controllerFromUrl = routeData.Values["controller"].ToString();
            string actionFromUrl = routeData.Values["action"].ToString();
            var controllerTranslation = this.Controllers.FirstOrDefault(d => d.Translation.Any(rf=>rf.TranslatedValue == controllerFromUrl));
            var controllerCulture = this.Controllers.SelectMany(d => d.Translation).FirstOrDefault(f => f.TranslatedValue == controllerFromUrl).CultureInfo;
            if (controllerTranslation != null)
            {
                routeData.Values["controller"] = controllerTranslation.ControllerName;
                var actionTranslation = controllerTranslation.ActionTranslations.FirstOrDefault(d => d.Translation.Any(rf => rf.TranslatedValue == actionFromUrl));
                if (actionTranslation != null)
                {
                  
                    routeData.Values["action"] = actionTranslation.ActionName;
                    
                }
                System.Threading.Thread.CurrentThread.CurrentCulture = controllerCulture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = controllerCulture;
            }
            

            return routeData;
        }

        /// <summary>
        /// Used in Html helper to create link
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {

            var requestedController = values["controller"];
            var requestedAction = values["action"];
            var controllerTranslation = this.Controllers.FirstOrDefault(d => d.Translation.Any(rf => rf.TranslatedValue == requestedController));
            var actionTranslation = controllerTranslation.ActionTranslations.FirstOrDefault(d => d.Translation.Any(rf => rf.TranslatedValue == requestedAction));
            var controllerTranslatedName = controllerTranslation.Translation.FirstOrDefault(d => d.CultureInfo == System.Threading.Thread.CurrentThread.CurrentCulture).TranslatedValue;
            if (controllerTranslatedName != null)
                values["controller"] = controllerTranslatedName;
            var actionTranslate = controllerTranslation.ActionTranslations.FirstOrDefault(d => d.Translation.Any(rf => rf.TranslatedValue == requestedAction));
            if (actionTranslate != null)
            {
                var actionTranslateName = actionTranslate.Translation.FirstOrDefault(d => d.CultureInfo == System.Threading.Thread.CurrentThread.CurrentCulture).TranslatedValue;
                if (actionTranslateName != null)
                    values["action"] = actionTranslateName;
            }
            return base.GetVirtualPath(requestContext, values);
        }
    }

}