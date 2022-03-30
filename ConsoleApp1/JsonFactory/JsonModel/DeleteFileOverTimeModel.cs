using Newtonsoft.Json.Linq;

namespace ConsoleApp1.JsonFactory.JsonModel
{
    public class DeleteFileOverTimeModel : IJsonModel
    {
        public string Scheduler { get; private set; }
        public string Path { get; private set; }
        public int OverDay { get; private set; }
        public string ErrMail { get; private set; }
        public DeleteFileOverTimeModel(JToken jToken)
        {
            this.SetJsonEntity(jToken);
        }

        public void SetJsonEntity(JToken jToken)
        {
            this.Scheduler = jToken["Scheduler"].Value<string>();
            this.Path = jToken["Path"].Value<string>();
            this.OverDay = jToken["OverDay"].Value<int>();
            this.ErrMail = jToken["ErrMail"].Value<string>();
        }
    }
}
