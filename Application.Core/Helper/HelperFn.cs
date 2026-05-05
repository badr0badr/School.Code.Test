﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Helper
{
    public static class HelperFn
    {
        public static sbyte GetLevel(string ClassFullId)
        {
            sbyte l = 0;
            var x = ClassFullId.Split(' ');
            if (x.Length > 0)
            {
                if (x[1] == "ب")
                {
                    if (x[0].Split('-')[0] == "1" || x[0].Split('-')[0] == "2") l = 1;
                    else l = 2;
                }
                else if (x[1] == "ع") l = 3;
                else l = 4;
            }
            return l;
        }
    }
}