using Newtonsoft.Json.Linq;

namespace ConsoleApp1.JsonFactory.JsonModel
{
    public class MoveFileToFtpServerModel : IJsonModel
    {
        public string Scheduler { get; private set; }
        public string Path { get; private set; }
        public string FtpIp { get; private set; }
        public string BakPath { get; private set; }
        public string FtpID { get; private set; }
        public string FtpPwd { get; private set; }
        //public string FtpDestination { get; private set; }
        //public bool SuccessdIsDelete { get; private set; }
        public string FileFilter { get; private set; }
        public string ErrMail { get; private set; }
        public MoveFileToFtpServerModel(JToken jToken)
        {
            this.SetJsonEntity(jToken);
        }

        public void SetJsonEntity(JToken jToken)
        {
            this.Scheduler = jToken["Scheduler"].Value<string>();
            this.Path = jToken["Path"].Value<string>();
            this.FtpIp = jToken["FtpIp"].Value<string>();
            this.BakPath = jToken["BakPath"].Value<string>();
            this.FtpID = jToken["FtpID"].Value<string>();
            this.FtpPwd = jToken["FtpPwd"].Value<string>();
            //this.FtpDestination = rootJObject["FtpDestination"].Value<string>();
            //this.SuccessdIsDelete = jToken["SuccessdIsDelete"].Value<bool>();
            this.FileFilter = jToken["FileFilter"].Value<string>();
            this.ErrMail = jToken["ErrMail"].Value<string>();
        }
    }
}
