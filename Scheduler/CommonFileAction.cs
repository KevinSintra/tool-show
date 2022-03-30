using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scheduler
{
    /// <summary>
    /// 檔案查檢的共通方法
    /// </summary>
    public abstract class CommonFileAction
    {
        /// <summary>
        /// 取得第一層目錄中所有檔案
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileFilter"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected IEnumerable<FileInfo> _GetTopDirectoryOnlyFiles(string path, string fileFilter = "*.*")
        {
            if (!Directory.Exists(path))
                throw new Exception("無此路徑");
            DirectoryInfo dirDirectory = new DirectoryInfo(path);
            return this._MakeFileInfos(dirDirectory, SearchOption.TopDirectoryOnly, fileFilter);
        }

        /// <summary>
        /// 取得第目錄(含子目錄)中所有檔案
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileFilter"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected IEnumerable<FileInfo> _GetAllDirectoriesFIles(string path, string fileFilter = "*.*")
        {
            if (!Directory.Exists(path))
                throw new Exception("無此路徑");
            DirectoryInfo dirDirectory = new DirectoryInfo(path);
            return this._MakeFileInfos(dirDirectory, SearchOption.AllDirectories, fileFilter);
        }

        /// <summary>
        /// 製作指定目錄底下檔案的 FileInfo
        /// </summary>
        /// <param name="directoryInfo"></param>
        /// <param name="searchOption"></param>
        /// <param name="fileFilter"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private IEnumerable<FileInfo> _MakeFileInfos(DirectoryInfo directoryInfo, SearchOption searchOption, string fileFilter = "*.*")
        {
            try
            {
                var fileInfos = directoryInfo.EnumerateFiles(fileFilter, searchOption).AsEnumerable();
                return fileInfos;
            }
            catch (UnauthorizedAccessException UAEx)
            {
                throw new Exception(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                throw new Exception(PathEx.Message);
            }
        }
    }
}
