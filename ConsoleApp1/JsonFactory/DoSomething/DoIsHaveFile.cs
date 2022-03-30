using ConsoleApp1.JsonFactory.JsonModel;
using Scheduler;

namespace ConsoleApp1.JsonFactory.DoSomething
{
    public class DoIsHaveFile : IWorkers
    {
        private IsHaveFileModel _jsonModel;
        public DoIsHaveFile(IsHaveFileModel isHaveFileModel)
        {
            this._SetJsonModel(isHaveFileModel);
        }
        public void DoWork()
        {
            #region initiate
            string findFilter;
            if (string.IsNullOrEmpty(this._jsonModel.FindFilter)) // 判斷有無過檔名濾器
                findFilter = "*.*";
            else
                findFilter = this._jsonModel.FindFilter;

            var mailService = new SendMailService("EC@bankpro.com.tw", this._jsonModel.ErrMail, "通知=>檔案存在否檢核", "通知=>檔案存在否檢核=>" + this._jsonModel.ErrDesc);
            var schedulerForFile = new SchedulerForFile(mailService);
            #endregion

            // 先做沒檔案的查檢
            var ifNotHaveFile = schedulerForFile.IsHaveFile(this._jsonModel.Path, bool.Parse(this._jsonModel.IsHaveFile), findFilter: findFilter);

            #region 再來查檢需存在檔案, 查檢今天的日期的檔案(選項)
            if (bool.Parse(this._jsonModel.IsHaveFile) && !ifNotHaveFile)
            {
                if (this._jsonModel.IsHaveTodeyFile) // 有指定日期
                {
                    // 目錄中沒有指定的日期檔案則發信
                    mailService.SetMailInfomation("EC@bankpro.com.tw", this._jsonModel.ErrMail, "通知=>檔案存在否檢核", "通知=>檔案存在否檢核=>" + this._jsonModel.ErrDesc);
                    schedulerForFile.IsHaveFile(this._jsonModel.Path, bool.Parse(this._jsonModel.IsHaveFile), true, findFilter: findFilter);
                    return;
                }
                //mailService.SetMailInfomation("EC@bankpro.com.tw", this._jsonModel.ErrMail, "通知=>檔案存在否檢核", "通知=>檔案存在否檢核" + this._jsonModel.ErrDesc);
                //schedulerForFile.IsHaveFile(this._jsonModel.Path, true);
            }
            #endregion
        }
        private void _SetJsonModel(IsHaveFileModel isHaveFileModel)
        {
            this._jsonModel = isHaveFileModel;
        }

        public void SetJsonModel<T>(T jsonModel) where T : IJsonModel
        {
            this._SetJsonModel(jsonModel as IsHaveFileModel);
        }
    }
}
