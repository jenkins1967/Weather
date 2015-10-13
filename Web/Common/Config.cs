using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Web.Common
{
    public static class Config
    {
        public static bool SkipCertificateChecks
        {
            get { return bool.Parse(ConfigurationManager.AppSettings["SkipCertificateChecks"]); }
        }
    }
}