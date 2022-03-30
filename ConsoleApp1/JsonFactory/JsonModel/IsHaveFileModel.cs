using Newtonsoft.Json.Linq;

namespace ConsoleApp1.JsonFactory.JsonModel
{
    public class IsHaveFileModel : IJsonModel
    {
        public string Scheduler { get; private set; }
        public string Path { get; private set; }
        public int OverTime { get; private set; }
        public bool IsHaveTodeyFile { get; private set; }
        public string ErrMail { get; private set; }
        public string IsHaveFile { get; private set; }
        public string ErrDesc { get; private set; }
        public string FindFilter { get; private set; }
        public IsHaveFileModel(JToken jToken)
        {
            this.SetJsonEntity(jToken);
        }

        public void SetJsonEntity(JToken jToken)
        {
            this.Scheduler = jToken["Scheduler"].Value<string>();
            this.Path = jToken["Path"].Value<string>();
            this.OverTime = jToken["OverTime"].Value<int>();
            this.IsHaveTodeyFile = jToken["IsHaveTodayFile"].Value<bool>();
            this.ErrMail = jToken["ErrMail"].Value<string>();
            this.IsHaveFile = jToken["IsHaveFile"].Value<string>();
            this.ErrDesc = jToken["ErrDesc"].Value<string>();
            this.FindFilter = jToken["FindFilter"].Value<string>();
        }
    }
}
