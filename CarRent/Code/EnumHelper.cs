using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq.Expressions;


namespace Codes
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum Enumeration)
        {
            string Value = Enumeration.ToString();
            Type EnumType = Enumeration.GetType();
            var DescAttribute = (System.ComponentModel.DescriptionAttribute[])EnumType
                .GetField(Value)
                .GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            ;
            return DescAttribute.Length > 0 ? ((System.ComponentModel.DescriptionAttribute)DescAttribute[0]).Description : Value;
        }

        public static Dictionary<int, string> ToDictionary(this Type enumType)
        {
            return Enum.GetValues(enumType)
                .Cast<object>()
                .ToDictionary(k => (int)k, v => ((Enum)v).GetDescription());
        }
    }


}

