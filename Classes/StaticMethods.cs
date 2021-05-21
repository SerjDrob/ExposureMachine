using System;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace ExposureMachine.Classes
{
    public static class StaticMethods
    {
        public static string GetDescription<T>(this T enumValue) where T : struct, IConvertible            
        {
            if (!typeof(T).IsEnum)
                return null;

            var description = enumValue.ToString();
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            if (fieldInfo != null)
            {
                var attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs != null && attrs.Length > 0)
                {
                    description = ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return description;
        }
        public static string ByteToString(this byte theByte)
        {
            String str = "";
            for (int i = 0; i < 8; i++)
            {
                str = ((((1 << i) & theByte) > 0) ? "1" : "0") + str;
            }
            return str;
        }
        public static bool SerializeToJson(this object obj, string filename)
        {
            try
            {
                File.WriteAllText(filename, JsonSerializer.Serialize(obj));
            }
            catch (Exception)
            {
                return false;
            }
            return true;            
        }
       
        public static T DeSerializeObjectJson<T>(string filename)
        {
            try
            {
                var file = File.ReadAllText(filename);
                return JsonSerializer.Deserialize<T>(file);
            }
            catch (Exception)
            {
                return default(T);
            }
            
        }
    }
}
