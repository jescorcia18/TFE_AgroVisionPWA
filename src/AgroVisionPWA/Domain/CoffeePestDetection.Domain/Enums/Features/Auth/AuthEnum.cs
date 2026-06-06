using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoffeePestDetection.Domain.Enums.Features.Auth
{
    public class AuthEnum
    {
        public enum Roles
        {
            [Description("farmer")]
            Farmer,

            [Description("admin")]
            Admin
        }
    }
}
