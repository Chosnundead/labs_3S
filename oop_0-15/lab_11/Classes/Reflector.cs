using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace lab_11.Classes
{
    public static class Reflector
    {
        public static string? getAssemblyName(string className)
        {
            return Type.GetType(className)?.Assembly.FullName + "\n";
        }
        public static bool? isPublicCtors(string className)
        {
            return Type.GetType(className)?.GetConstructors().Length != 0 ? true : false;
        }
        public static string? getPublicMethods(string className)
        {
            // return Type.GetType(className)?.GetMethods();
            string result = "";
            foreach (var item in Type.GetType(className)?.GetMethods())
            {
                result += item.ToString() + "\n";
            }
            return result;
        }
        public static string? getFieldsAndProps(string className)
        {
            string result = "fields:\n";
            foreach (var item in Type.GetType(className)?.GetFields())
            {
                result += item.ToString() + "\n";
            }
            result += "properties:\n";
            foreach (var item in Type.GetType(className)?.GetProperties())
            {
                result += item.ToString() + "\n";
            }
            return result;
        }
        public static string? getInterfaces(string className)
        {
            string result = "";
            foreach (var item in Type.GetType(className)?.GetInterfaces())
            {
                result += item.ToString() + "\n";
            }
            return result;
        }
        public static string? getMethodsByParams(string className, string paramType)
        {
            string result = "";
            foreach (var item in Type.GetType(className)?.GetMethods())
            {
                bool flag = false;
                foreach (var param in item.GetParameters())
                {
                    if (param.ToString().ToLower().Contains(paramType.ToLower()))
                    {
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    result += item.Name + "\n";
                }
            }
            return result;
        }
        public static string? Invoke(object? obj, string methodName, object?[]? parameters)
        {
            foreach (var item in obj.GetType().GetMethods())
            {
                if (methodName == item.Name)
                {
                    item.Invoke(obj, parameters);
                }
            }
            return null;
        }

        public static object? Create(string className)
        {
            return Type.GetType(className)?.GetConstructor(new Type[0])?.Invoke(new object[0]);
        }
    }
}