using Newtonsoft.Json.Linq;
using System;
using ConsoleApp1.JsonFactory.DoSomething;
using ConsoleApp1.JsonFactory.JsonModel;

namespace ConsoleApp1.JsonFactory
{
    // 執行個個命令要做的事情
    public class JsonFactory : IJsonFactory
    {
        private IWorkers _doWorker = null;

        public JsonFactory(string jsonStr, string arg)
        {
            this.SetInit(jsonStr, arg);
        }

        public void Run()
        {
            this._doWorker.DoWork();
        }

        public void SetInit(string jsonStr, string arg)
        {
            var jObject = this._ReadJsonStrToJObject(jsonStr); // 讀 JsonStr 取 Jonject
            var jsonModel = this._CheckArgAndGetJsonModel(jObject, arg); // 取得對應的 JsonModel
            var worker = this._GetWorker(jsonModel);
            this._doWorker = worker;
        }

        // 讀檔並轉 Jobject
        private JObject _ReadJsonStrToJObject(string jsonStr)
        {
            JObject result = null;
            try // 讀檔
            {
                result = JObject.Parse(jsonStr);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // 由外部參數 arg 判斷要實力化哪個 JsonModel
        private IJsonModel _CheckArgAndGetJsonModel(JObject jObject, string arg)
        {
            const string jsonPerpoty = "Scheduler";
            try
            {
                var rootJObject = jObject[arg]; // 依據外部參數找到 根節點
                switch (rootJObject[jsonPerpoty].Value<string>()) // 依 Scheduler 屬性資料, 實例化對應的 JsonModel
                {
                    case "HaveFileOverTime":
                        return new HaveFileOverTimeModel(rootJObject);
                    case "IsHaveFile":
                        return new IsHaveFileModel(rootJObject);
                    case "DeleteFileOverTime":
                        return new DeleteFileOverTimeModel(rootJObject);
                    case "MoveFileToFtpServer":
                        return new MoveFileToFtpServerModel(rootJObject);
                    default:
                        throw new Exception("檔案查檢小工具是否有新方法?");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("解析 Json 對應屬性 Scheduler 時, 發生例外", ex);
            }
        }

        // 依據 JsonModel 的型別產生實力化
        private IWorkers _GetWorker(IJsonModel jsonModel)
        {
            if (jsonModel is HaveFileOverTimeModel)
                return new DoHaveFileOverTime(jsonModel as HaveFileOverTimeModel);

            if (jsonModel is IsHaveFileModel)
                return new DoIsHaveFile(jsonModel as IsHaveFileModel);

            if (jsonModel is DeleteFileOverTimeModel)
                return new DoDeleteFileOverTime(jsonModel as DeleteFileOverTimeModel);

            if(jsonModel is MoveFileToFtpServerModel)
                return new DoMoveFileToFtpServer(jsonModel as MoveFileToFtpServerModel);

            throw new Exception($" {jsonModel.Scheduler} 該JsonModel沒有對應的作法.");
        }
    }
}
