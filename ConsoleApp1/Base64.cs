using System;
using System.Text;

namespace ConsoleApp1
{
    public sealed class Base64
    {

        private static string EncodeBase64(Encoding encode, string source)
        {
            byte[] bytes = encode.GetBytes(source);
            string result;
            try
            {
                result = Convert.ToBase64String(bytes);
            }
            catch
            {
                result = source;
            }
            return result;
        }

        public static string EncodeBase64ByUTF8(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }


        private static string DecodeBase64(Encoding encode, string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }

        public static string DecodeBase64ByUTF8(string result)
        {
            return DecodeBase64(Encoding.UTF8, result);
        }
    }
}
