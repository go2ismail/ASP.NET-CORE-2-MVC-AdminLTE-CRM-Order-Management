using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace motekarteknologi.Motekar
{
    public class Modules
    {
        public const string Security = "Security";
        public const string CRM = "CRM";
        public const string Inventory = "Inventory";
        public const string SO = "Sales Order";
        public const string PO = "Purchase Order";

        public static readonly List<string> All = new List<string> {
            Security,
            CRM,
            Inventory,
            SO,
            PO
        };
        
    }

    public class SecurityModules
    {
        public const string User = "Application Users";
        public const string Role = "Application Roles";
        public const string Company = "Companies";
        public const string Dashboards = "Dashboards";

        public static readonly List<string> All = new List<string> {
            User,
            Role,
            Company,
            Dashboards
        };
    }
}
