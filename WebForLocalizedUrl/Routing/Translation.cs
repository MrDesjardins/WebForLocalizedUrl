using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace WebForLocalizedUrl.Routing
{
    public class Translation
    {
        public CultureInfo CultureInfo { get; set; }
        public string TranslatedValue { get; set; }
        public Translation(CultureInfo culture, string translatedValue)
        {
            CultureInfo = culture;
            TranslatedValue = translatedValue;
        }
    }
}