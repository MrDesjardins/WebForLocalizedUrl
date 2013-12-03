using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForLocalizedUrl.Routing
{
    public class ActionTranslation
    {
        public string ActionName { get; set; }
        public List<Translation> Translation { get; set; }

        public ActionTranslation(string actionName, List<Translation> translation)
        {
            this.ActionName = actionName;
            this.Translation = translation;
        }

    }
}