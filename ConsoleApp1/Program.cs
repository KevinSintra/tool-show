using System;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{

    class Program
    {
        private readonly static string _fileName = "ExeSetting.json";
        private readonly static string _en64FileName = "En64ExeSetting.json";

        static void Main(string[] args)
        {
            try
            {
                EnAndDeCodeToFile.IfHaveFileForExeSetting(_fileName, _en64FileName); // 判斷有無明文檔
                var jsonStr = EnAndDeCodeToFile.ReadEn64File(_en64FileName); // 讀取加密檔

                //args = new string[1] { "F11" };
                if (args.FirstOrDefault() is null)
                    throw new Exception("無參數丟入");
                IJsonFactory jsonFactory = new JsonFactory.JsonFactory(jsonStr, args.First().Trim());
                jsonFactory.Run();
            }
            catch (Exception ex)  // 將錯誤訊息記錄到 .exe 檔的目錄下.
            {
                var fileName = "ErrorInfo.txt";
                var time = DateTime.Now;
                FileInfo fi1 = new FileInfo(fileName);
                StreamWriter sw;
                if (fi1.Exists)
                    sw = fi1.AppendText();
                else
                    sw = fi1.CreateText();
                using (sw)
                {
                    sw.WriteLine($"------------------------------------------------------{time.ToString()}");
                    sw.WriteLine($"錯誤時間: {time.ToString()}");
                    _CreateMsgTxt(sw, ex);
                    sw.WriteLine($"------------------------------------------------------{time.ToString()}");
                }
            }
        }

        // 依據 Exception 的階層關係, 印出每層中的 Exception 的 msg.
        private static StreamWriter _CreateMsgTxt(StreamWriter writer, Exception ex, int index = 1)
        {
            if (!(ex is null))
            {
                writer.WriteLine($"ExpectionMsg{index++}: {ex.Message}");
                _CreateMsgTxt(writer, ex.InnerException, index);
            }
            return writer;
        }
    }
}
