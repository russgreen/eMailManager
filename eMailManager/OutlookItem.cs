// ***************************************************************************
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// This code sample is provided "AS IS" without warranty of any kind.
// 
// ***************************************************************************
using System.Reflection;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace eMailManager
{
    /// <summary>
    /// An internal static helper class that provides access to
    /// common Outlook item properties so that mail, appointment
    /// and task items can be treated polymorphically.
    /// </summary>
    internal sealed class OutlookItem
    {
        private OutlookItem()
        {
        }
        /// <summary>
	    /// Returns the Subject of the given Outlook item.
	    /// </summary>
	    /// <param name="item">Outlook item</param>
        internal static string GetSubject(object item)
        {
            // Use reflection to retrieve the item's Subject property.
            string subject = item.GetType().InvokeMember("Subject", BindingFlags.Public | BindingFlags.GetField | BindingFlags.GetProperty, null, item, new object[] { }, System.Globalization.CultureInfo.InvariantCulture) as string;
            return subject;
        }

        /// <summary>
	    /// Sets the FilterMatch user property on the given Outlook item.
	    /// </summary>
	    /// <param name="item">Outlook item</param>
	    /// <param name="value">User property value</param>
        internal static void SetFilterMatchProperty(object item, string value)
        {
            var prop = GetUserProperty(item, "FilterMatch");
            prop.Value = value;
            Save(item);
        }

        /// <summary>
        /// Returns the specified user property object from the given Outlook item.
        /// If the user property is not found within the item's UserProperty collection,
        /// then it will be added.
        /// </summary>
        /// <param name="item">Outlook item</param>
        /// <param name="propName">User Property name</param>
        /// <returns>UserProperty object</returns>
        private static Outlook.UserProperty GetUserProperty(object item, string propName)
        {
            Outlook.UserProperty prop = null;

            // Use reflection to retrieve the item's User Property.
            prop = item.GetType().InvokeMember("UserProperties", BindingFlags.Public | BindingFlags.GetField | BindingFlags.GetProperty, null, item, new object[] { propName }, System.Globalization.CultureInfo.InvariantCulture) as Outlook.UserProperty;

            // UserProperty not found so add it.
            if (prop is null)
            {
                Outlook.UserProperties userProps = item.GetType().InvokeMember("UserProperties", BindingFlags.Public | BindingFlags.GetField | BindingFlags.GetProperty, null, item, new object[] { }, System.Globalization.CultureInfo.InvariantCulture) as Outlook.UserProperties;
                prop = item.GetType().InvokeMember("Add", BindingFlags.Public | BindingFlags.InvokeMethod, null, userProps, new object[] { propName, Outlook.OlUserPropertyType.olText, true, Outlook.OlUserPropertyType.olText }, System.Globalization.CultureInfo.InvariantCulture) as Outlook.UserProperty;
            }

            return prop;
        }

        /// <summary>
	    /// Invokes the save method on the given Outlook item.
	    /// </summary>
	    /// <param name="item">Outlook item</param>
        private static void Save(object item)
        {
            // Use reflection to invoke the Save method.
            item.GetType().InvokeMember("Save", BindingFlags.Public | BindingFlags.InvokeMethod, null, item, new object[] { }, System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}