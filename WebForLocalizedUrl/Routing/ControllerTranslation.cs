using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForLocalizedUrl.Routing
{
        public class ControllerTranslation
        {
            public string ControllerName { get; set; }
            public List<Translation> Translation { get; set; }
            public List<ActionTranslation> ActionTranslations { get; set; }

            public ControllerTranslation(string controllerName, List<Translation> translation, List<ActionTranslation> actionsList)
            {
                this.ControllerName = controllerName;
                this.Translation = translation;
                this.ActionTranslations = actionsList;
            }
        }
}