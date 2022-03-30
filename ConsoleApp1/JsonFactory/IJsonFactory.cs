namespace ConsoleApp1
{
    /// <summary>
    /// 執行各個命令要做的事情
    /// </summary>
    public interface IJsonFactory
    {
        /// <summary>
        /// 執行對應欲執行命令
        /// </summary>
        void Run();

        /// <summary>
        /// 重新設定 命令, json檔路徑
        /// </summary>
        /// <param name="jsonPath"></param>
        /// <param name="arg"></param>
        void SetInit(string jsonPath, string arg);
    }
}
