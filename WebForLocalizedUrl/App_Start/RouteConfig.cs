using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebForLocalizedUrl.Routing;

namespace WebForLocalizedUrl
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var cultureEN = CultureInfo.GetCultureInfo("en-US");
            var cultureFR = CultureInfo.GetCultureInfo("fr-FR");

            var translationTables = new List<ControllerTranslation>{ 
                new ControllerTranslation("Home"
                     , new List<Translation>{
                                 new Translation(cultureEN, "Home")
                                ,new Translation(cultureFR, "Demarrer")
                     }
                    ,new List<ActionTranslation>{
                         new ActionTranslation("About"
                            , new List<Translation>{
                                 new Translation(cultureEN, "About")
                                ,new Translation(cultureFR, "Infos")
                            })
                         , new ActionTranslation("Home"
                            , new List<Translation>{
                                 new Translation(cultureEN, "Home")
                                ,new Translation(cultureFR, "Demarrer")
                            })
                        , new ActionTranslation("Contact"
                            , new List<Translation>{
                                 new Translation(cultureEN, "Contact")
                                ,new Translation(cultureFR, "InformationSurLaPersonne")
                            })
                    })
                ,new ControllerTranslation("Account"
                     ,
                     new List<Translation>{
                                 new Translation(cultureEN, "Account")
                                ,new Translation(cultureFR, "Compte")
                     }
                     ,
                     new List<ActionTranslation>
                     {
                         new ActionTranslation("Login"
                            , new List<Translation>{
                                 new Translation(cultureEN, "Login")
                                ,new Translation(cultureFR, "Authentification")
                            })
                            , 
                            new ActionTranslation("Register"
                            , new List<Translation>{
                                 new Translation(cultureEN, "Register")
                                ,new Translation(cultureFR, "Enregistrement")
                            })

                     }
                )
            };

            routes.Add("LocalizedRoute", new TranslatedRoute(
               "{controller}/{action}/{id}",
               new RouteValueDictionary(new { controller = "Home", action = "Index", id = "" }),
               translationTables,
               new MvcRouteHandler()));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
  
}
