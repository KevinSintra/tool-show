using System;
using System.IO;
using System.Text;

namespace ConsoleApp1
{
    /// <summary>
    /// 判斷加解密檔案用
    /// </summary>
    public sealed class EnAndDeCodeToFile
    {
        /// <summary>
        /// 判斷有無明文檔案, 有的話加密產生新檔, 並刪除明文檔
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="newEncodeFileName"></param>
        /// <exception cref="Exception"></exception>
        public static void IfHaveFileForExeSetting(string fileName, string newEncodeFileName)
        {
            if (!File.Exists(fileName)) // 查檢有無明文檔案
                return;

            var fileStr = _ReadFile(fileName);

            //try // 刪除檔案
            //{
            //    File.Delete(fileName);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}

            var en64Str = Base64.EncodeBase64ByUTF8(fileStr); // 加密內容

            try // 新增加密檔案
            {
                using (var fs = File.Create(newEncodeFileName))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes(en64Str);
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 讀取加密檔案, 並返回明文
        /// </summary>
        /// <param name="EncodeFileName"></param>
        /// <returns></returns>
        public static string ReadEn64File(string EncodeFileName)
        {
            string en64FileStr = _ReadFile(EncodeFileName);

            var de64Str = Base64.DecodeBase64ByUTF8(en64FileStr);
            return de64Str;
        }

        /// <summary>
        /// 讀檔
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string _ReadFile(string fileName)
        {
            string result = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }
    }
}
