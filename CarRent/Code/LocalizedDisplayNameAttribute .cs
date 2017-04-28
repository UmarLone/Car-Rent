/****************************** Module Header ******************************\
Module Name:  <LocalizedDisplayNameAttribute>
Project:      CarRent
Author :    Umar Farooq
\***************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
namespace Code
{
    public class LocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        /// <summary>
        /// Global Variables
        /// </summary>
        private PropertyInfo _nameProperty;
        private Type _resourceType;

        public LocalizedDisplayNameAttribute(string displayNameKey)
            : base(displayNameKey)
        {

        }

        /// <summary>m
        /// Get Type of localize attribute
        /// </summary>
        public Type NameResourceType
        {
            get{ return _resourceType;}

            set{
                _resourceType = value;                
                _nameProperty = _resourceType.GetProperty(base.DisplayName,BindingFlags.Static | BindingFlags.Public);
            }

        }

        /// <summary>
        /// Update values of localize value and display updated name
        /// </summary>
        public override string DisplayName
        {
            get
            {                
                if (_nameProperty == null){  return base.DisplayName;}                
                return (string)_nameProperty.GetValue(_nameProperty.DeclaringType, null);
            }

        }
    }
}