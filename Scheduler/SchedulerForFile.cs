using System;
using System.Linq;

namespace Scheduler
{
    public class SchedulerForFile : CommonFileAction
    {
        private SendMailService _sendMailService;

        public SchedulerForFile(SendMailService sendMailService)
        {
            this.SetMailSevice(sendMailService);
        }

        public void SetMailSevice(SendMailService sendMailService)
        {
            this._sendMailService = sendMailService;
        }

        /// <summary>
        /// 在該目錄中是否有過期(分鐘)的檔案, 有的話發 Mail
        /// </summary>
        /// <param name="path"></param>
        /// <param name="overMinutes"></param>
        public void HaveFileOverTime(string path, int overMinutes)
        {
            var topFile = base._GetTopDirectoryOnlyFiles(path);
            if (topFile.Any(x => (DateTime.Now - x.CreationTime).TotalMinutes > overMinutes))
                this._sendMailService.SendMail();
        }

        /// <summary>
        /// 根據參數 haveFile 在該目錄中是否有檔案, 並發 Mail.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="haveFile"></param>
        /// <param name="checkTodayFile">確認檔案中的檔名, 是否包含今日日期.</param>
        /// <param name="findFilter">篩選特定檔名的過濾器</param>
        /// <returns></returns>
        public bool IsHaveFile(string path, bool haveFile, bool checkTodayFile = false, string findFilter = "*.*")
        {
            var topFile = base._GetTopDirectoryOnlyFiles(path, findFilter);
            if (checkTodayFile)
                topFile = topFile.Where(x => x.Name.Contains(DateTime.Now.ToString("yyyyMMdd")));
            if (!topFile.Any() == haveFile)
            {
                this._sendMailService.SendMail();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 在該目錄(含子目錄)底下檔案若過期(天)的話, 清除掉
        /// </summary>
        /// <param name="path"></param>
        /// <param name="overDate"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteFileOverTime(string path, int overDate)
        {
            var topFile = base._GetAllDirectoriesFIles(path);
            var overTimeFile = topFile.Where(x => (DateTime.Now - x.CreationTime).TotalDays > overDate).ToList();
            if (overTimeFile.Any())
                overTimeFile.ForEach(x =>
                {
                    try
                    {
                        x.Delete();
                    }
                    catch (System.IO.IOException e)
                    {
                        throw new Exception(e.Message);
                    }
                });
        }

        /// <summary>
        /// 將本機指定路徑中所有檔案(不含子目錄), 傳送到指定 FTP SERVER 中. 若失敗則發信.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ftpIp"></param>
        /// <param name="ftpID"></param>
        /// <param name="ftpPwd"></param>
        /// <param name="fileFilter"></param>
        /// <param name="backPath"></param>
        /// <returns></returns>
        public bool MoveFileToFtpServer(string path, string ftpIp, string ftpID, string ftpPwd, string fileFilter, string backPath)
        {
            var ftpService = new FtpHelper();
            var ftpSendSucessed = true;
            var topFile = base._GetTopDirectoryOnlyFiles(path, fileFilter);
            topFile.ToList().ForEach(x =>
            {
                var ftpDestination = x.Name; // 取得 檔名+附檔名
                if (ftpService.SendFile(ftpIp, x.FullName, ftpDestination, ftpID, ftpPwd)) // 失敗要寄信
                {
                    var fullFileName = System.IO.Path.Combine(backPath, x.Name);
                    x.MoveTo(fullFileName);
                }
                else
                    ftpSendSucessed = false;
            });
            if (!ftpSendSucessed)
                this._sendMailService.SendMail();

            return ftpSendSucessed;
        }

        /// <summary>
        /// 移動檔案, 若該目錄沒有就新增.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="targetPath"></param>
        /// <param name="fileFilter"></param>
        /// <exception cref="Exception"></exception>
        public void MoveFileToOtherDirectory(string path, string targetPath, string fileFilter)
        {
            if (!System.IO.Directory.Exists(targetPath)) // 確認目錄
                System.IO.Directory.CreateDirectory(targetPath);
            var topFile = base._GetTopDirectoryOnlyFiles(path, fileFilter);
            try
            {
                topFile.ToList().ForEach(x =>
                {
                    var fullFileName = System.IO.Path.Combine(targetPath, x.Name);
                    x.MoveTo(fullFileName);
                });
            }
            catch (Exception ex)
            {
                throw new Exception("移動檔案失敗", ex);
            }
        }
    }
}