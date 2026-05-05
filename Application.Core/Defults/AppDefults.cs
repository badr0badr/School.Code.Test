using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Defults
{
    public static class AppDefults
    {
        public const string DecimalColumnType = "decimal(4,1)";
        public static JwtData Jwt = new();
    }
    public class JwtData
    {
        public string Key { get; } = "El3asalElNa7lN7twYa7nayashrbatatFeELKOBayatYak7kEL3edwgAREGEAPKSNJNOBhiygjbuvctuVYVTCUTVYK";
        public string Issuer { get; } = "https://schoolwebapitest.runasp.net";
        public string Audience { get; } = "School";
        public int DurationInDays { get; } = 90;
    }
}