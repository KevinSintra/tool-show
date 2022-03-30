using ConsoleApp1.JsonFactory.JsonModel;
using Scheduler;

namespace ConsoleApp1.JsonFactory.DoSomething
{
    public class DoMoveFileToFtpServer : IWorkers
    {
        private MoveFileToFtpServerModel _jsonModel;
        public DoMoveFileToFtpServer(MoveFileToFtpServerModel moveFileToFtpServerModel)
        {
            this._SetJsonModel(moveFileToFtpServerModel);
        }
        public void DoWork()
        {
            // 將檔案傳送到 ftp server
            var schedulerForFile = new SchedulerForFile(new SendMailService("EC@banpro.com.tw", this._jsonModel.ErrMail, "FTP搬檔異常通知", "檔案移動至FTP失敗"));
            var isSuccessed = schedulerForFile.MoveFileToFtpServer(this._jsonModel.Path, this._jsonModel.FtpIp, this._jsonModel.FtpID, this._jsonModel.FtpPwd, this._jsonModel.FileFilter, this._jsonModel.BakPath);

            //if (isSuccessed) // 若成功 將檔案傳送到 ftp server, 將檔案移動到 bak 目錄下.
            //    schedulerForFile.MoveFileToOtherDirectory(this._jsonModel.Path, this._jsonModel.BakPath, this._jsonModel.FileFilter);
        }
        private void _SetJsonModel(MoveFileToFtpServerModel moveFileToFtpServerModel)
        {
            this._jsonModel = moveFileToFtpServerModel;
        }

        public void SetJsonModel<T>(T jsonModel) where T : IJsonModel
        {
            this._SetJsonModel(jsonModel as MoveFileToFtpServerModel);
        }
    }
}
