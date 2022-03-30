using System;
using System.IO;
using System.Net;
using System.Text;

namespace Scheduler
{
    public class FtpHelper
    {
        public bool SendFile(string strServerIP, string strFile_Source, string strFile_Destination
                          , string strUserName, string strPassword)
        {
            try
            {
                //先組合檔案的路徑       
                Uri FileURL = new Uri($"ftp://{strServerIP}/{strFile_Destination}");
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FileURL);
                request.Method = WebRequestMethods.Ftp.UploadFile;

                //驗證身份   
                request.Credentials = new NetworkCredential(strUserName, strPassword);
                Stream ftpsream = request.GetRequestStream();

                byte[] fileContents;
                using (var sr = new StreamReader(strFile_Source, Encoding.UTF8)) // 讀取檔案
                {
                    fileContents = Encoding.UTF8.GetBytes(sr.ReadToEnd());
                }

                request.ContentLength = fileContents.Length;
                ftpsream.Write(fileContents, 0, fileContents.Length); // 上傳檔案.
                ftpsream.Close();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                response.Close();
                return true;

                //if (response.StatusCode == FtpStatusCode.ClosingData || response.StatusDescription == "206")
                //{
                //TODO: 還不知如何判別動作是否失敗或成功 
                //}
            }
            catch (WebException e)
            {
                return false;
                throw new Exception("WebException Raised. The following error occured : " + e.Status);
            }
            catch (Exception e)
            {
                return false;
                throw new Exception("The following Exception was raised : " + e.Message);
            }
        }

        /// <summary>
        /// 從 ftp Server 取得單一檔案
        /// </summary>
        /// <param name="strServerIP"> domain </param>
        /// <param name="strFile_Source"> ftp 上的檔案完整路徑(含檔案名稱) </param>
        /// <param name="strFile_Destination"> 檔案的存放位置 </param>
        /// <param name="strUserName"> ftp 帳號 </param>
        /// <param name="strPassword"> ftp 密碼 </param>
        public bool GetFile(string strServerIP, string strFile_Source, string strFile_Destination, string strUserName, string strPassword)
        {
            try
            {
                Uri FileURL = new Uri($"ftp://{strServerIP}/{strFile_Destination}"); //先組合檔案的路徑
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FileURL);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(strUserName, strPassword); //驗證身份  
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                using (FileStream writeStream = new FileStream(strFile_Destination, FileMode.Create))
                {
                    int Length = 2048;
                    Byte[] buffer = new Byte[Length];
                    int bytesRead = responseStream.Read(buffer, 0, Length);
                    while (bytesRead > 0)
                    {
                        writeStream.Write(buffer, 0, bytesRead);
                        bytesRead = responseStream.Read(buffer, 0, Length);
                    }//end while
                }//end using

                response.Close();
                return true;
            }
            catch (WebException e)
            {
                return false;
                throw new Exception("WebException Raised. The following error occured : " + e.Status);
            }
            catch (Exception e)
            {
                return false;
                throw new Exception("The following Exception was raised : " + e.Message);
            }
        }

    }
}
