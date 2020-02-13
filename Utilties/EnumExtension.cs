using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Utilties
{
  public static class EnumExtension
  {
    public static List<string> GetAllEnumTypes()
    {
      var enumList = new List<string>();
      var enumContainer = typeof(Enums);
      var nestedEnums = ((TypeInfo)enumContainer)?.DeclaredNestedTypes;

      if (nestedEnums == null) return enumList;

      foreach (var enumValue in nestedEnums)
      {
        enumList.Add(new string(enumValue.Name));
      }

      return enumList;
    }

    public static List<EnumJsonTemplate> GetEnumTypeByName(string enumName)
    {
      var enumList = new List<EnumJsonTemplate>();
      var enumContainer = typeof(Enums);
      var nestedEnums = ((TypeInfo)enumContainer)?.DeclaredNestedTypes;

      if (nestedEnums == null) return enumList;

      foreach (var enumValue in nestedEnums)
      {
        if (string.Equals(enumValue.Name.ToLower(), enumName.ToLower(), StringComparison.InvariantCulture))
        {
          return ToJsonString(enumValue);
        }
      }

      return enumList;
    }

    public static EnumJsonTemplate GetEnumTypeByNameById(string enumName, int id)
    {
      return GetEnumTypeByName(enumName)?.SingleOrDefault(x => x.Id == id);
    }

    public static string GetDescription<T>(this T e) where T : IConvertible
    {
      string description = null;

      if (!(e is Enum)) return description;

      var type = e.GetType();
      var values = Enum.GetValues(type);

      foreach (int val in values)
      {
        if (val != e.ToInt32(CultureInfo.InvariantCulture)) continue;

        var memInfo = type.GetMember(type.GetEnumName(val));
        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (descriptionAttributes.Length > 0)
        {
          description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
        }

        break;
      }

      return description;
    }


    public static IDictionary<int, string> ToDictionary<T>(this T e) where T : Type
    {
      var list = new Dictionary<int, string>();

      if (!e.IsEnum)
      {
        return list;
      }

      var values = Enum.GetValues(e);

      foreach (var value in values)
      {
        list.Add((int)value, value.ToString());
      }

      return list;
    }

    public static List<EnumJsonTemplate> ToJsonString<T>(this T e) where T : Type
    {
      var jsonList = new List<EnumJsonTemplate>();

      if (!e.IsEnum)
      {
        return jsonList;
      }

      var values = Enum.GetValues(e);

      foreach (var value in values)
      {
        var memInfo = e.GetMember(e.GetEnumName(value));
        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        var description = String.Empty;
        if (descriptionAttributes.Length > 0)
        {
          description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
        }

        jsonList.Add(new EnumJsonTemplate
        {
          Id = (int)value,
          Value = value.ToString(),
          Description = description
        });
      }

      return jsonList;
    }
  }
}
