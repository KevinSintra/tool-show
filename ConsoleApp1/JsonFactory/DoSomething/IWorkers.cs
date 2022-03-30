using Newtonsoft.Json.Linq;

namespace ConsoleApp1.JsonFactory.DoSomething
{
    /// <summary>
    /// 做事情的
    /// </summary>
    public interface IWorkers
    {
        /// <summary>
        /// 做該做的事
        /// </summary>
        void DoWork();

        /// <summary>
        /// 設定細節對於 該做的事 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonModel"></param>
        void SetJsonModel<T>(T jsonModel) where T : JsonModel.IJsonModel;
    }
}
