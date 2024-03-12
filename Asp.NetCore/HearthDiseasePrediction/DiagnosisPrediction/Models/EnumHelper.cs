using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

public static class EnumHelper
{
    public static string GetDisplayName(Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        if (field != null)
        {
            var attribute = (DisplayAttribute)Attribute.GetCustomAttribute(field, typeof(DisplayAttribute));
            if (attribute != null)
            {
                return attribute.Name;
            }
        }
        return value.ToString(); // If no display name attribute found, return the default enum string representation.
    }
}
