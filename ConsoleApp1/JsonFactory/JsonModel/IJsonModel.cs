using Newtonsoft.Json.Linq;

namespace ConsoleApp1.JsonFactory.JsonModel
{
    /// <summary>
    /// 將 Json 檔案讀出, 並使用 物件屬性的方式存取 Json 資料
    /// </summary>
    public interface IJsonModel
    {
        string Scheduler { get; }

        /// <summary>
        /// 取得外部 Json, 並 map Worker 所需資料.
        /// </summary>
        /// <param name="jToken"></param>
        void SetJsonEntity(JToken jToken);
    }
}
