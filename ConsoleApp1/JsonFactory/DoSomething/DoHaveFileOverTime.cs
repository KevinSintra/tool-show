using ConsoleApp1.JsonFactory.JsonModel;
using Scheduler;

namespace ConsoleApp1.JsonFactory.DoSomething
{
    public class DoHaveFileOverTime : IWorkers
    {
        private HaveFileOverTimeModel _jsonModel;
        public DoHaveFileOverTime(HaveFileOverTimeModel haveFileOverTimeModel)
        {
            this._SetJsonModel(haveFileOverTimeModel);
        }
        private void _SetJsonModel(HaveFileOverTimeModel haveFileOverTimeModel)
        {
            this._jsonModel = haveFileOverTimeModel;
        }

        public void DoWork()
        {
            new SchedulerForFile(new SendMailService("EC@bankpro.com.tw", this._jsonModel.ErrMail, "通知=>檔案存在超過時間", "通知=>檔案存在超過時間=>" + this._jsonModel.ErrDesc)).
                        HaveFileOverTime(this._jsonModel.Path, this._jsonModel.OverTime);
        }

        public void SetJsonModel<T>(T jsonModel) where T : IJsonModel
        {
            this._SetJsonModel(jsonModel as HaveFileOverTimeModel);
        }
    }
}
