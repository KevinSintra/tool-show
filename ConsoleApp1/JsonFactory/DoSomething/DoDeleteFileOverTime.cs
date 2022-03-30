using ConsoleApp1.JsonFactory.JsonModel;
using Scheduler;

namespace ConsoleApp1.JsonFactory.DoSomething
{
    public class DoDeleteFileOverTime : IWorkers
    {
        private DeleteFileOverTimeModel _jsonModel;
        public DoDeleteFileOverTime(DeleteFileOverTimeModel deleteFileOverTimeModel)
        {
            this._SetJsonModel(deleteFileOverTimeModel);
        }        
        public void DoWork()
        {
            new SchedulerForFile(new SendMailService("kevin79713pro@gmail.com", this._jsonModel.ErrMail, "", "")).
                DeleteFileOverTime(this._jsonModel.Path, this._jsonModel.OverDay);
        }

        private void _SetJsonModel(DeleteFileOverTimeModel deleteFileOverTimeModel)
        {
            this._jsonModel = deleteFileOverTimeModel;
        }

        public void SetJsonModel<T>(T jsonModel) where T : IJsonModel
        {
            this._SetJsonModel(jsonModel as DeleteFileOverTimeModel);
        }
    }
}
