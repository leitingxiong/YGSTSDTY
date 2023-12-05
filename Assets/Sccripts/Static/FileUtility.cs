using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace GameUtil 
{
    public class FileUtility
    {
        /// <summary>
        /// 路径格式转换成Unity下的路径格式
        /// </summary>
        /// <param name="path">原始路径</param>
        /// <returns></returns>
        public static string FormatToUnityPath(string path) 
        {
            return path.Replace("\\", "/");
        }

        /// <summary>
        /// 全路径转换为Assets内相对路径
        /// </summary>
        /// <param name="fullPath">全路径</param>
        /// <returns></returns>
        public static string FullPathToAssetsPath(string fullPath) 
        {
            return FormatToUnityPath(fullPath).Replace(Application.dataPath, "Assets");
        }

        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="withoutExtension">是否不需要扩展名称</param>
        /// <returns></returns>
        public static string GetFileName(string filePath, bool withoutExtension = true) 
        {
            if (withoutExtension)
            {
                return FormatToUnityPath(Path.GetFileNameWithoutExtension(filePath));
            }
            else
                return FormatToUnityPath(Path.GetFileName(filePath));
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="directPath">绝对路径</param>
        /// <param name="searchPattern">要匹配的字符</param>
        /// <param name="searchOption">搜索选项</param>
        /// <returns></returns>
        public static FileInfo[] GetFiles(string directPath, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories) 
        {
            //测试目录是否存在或者尝试确定指定目录是否存在
            if (Directory.Exists(directPath)) 
            {
                DirectoryInfo directorInfo = new DirectoryInfo(directPath);
                return directorInfo.GetFiles(searchPattern, searchOption);
            }
            Debug.LogError($"文件夹不存在{directPath}");
            return null;
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="directPath">绝对路径</param>
        /// <param name="searchPattern">要匹配的字符</param>
        /// <param name="searchOption">搜索选项</param>
        /// <returns></returns>
        public static string[] GetFilesPaths(string directPath, string searchPattern = "*", SearchOption searchOption = SearchOption.AllDirectories) 
        {
            List<string> paths = new List<string>();
            FileInfo[] files = GetFiles(directPath, searchPattern, searchOption);
            string tempStr;
            if (files != null && files.Length > 0) 
            {
                foreach (FileInfo file in files)
                {
                    //将完整路径转换为Unity里的相对路径
                    tempStr = FullPathToAssetsPath(file.FullName);
                    paths.Add(tempStr);
                }
            }
            return paths.ToArray();
        }

        /// <summary>
        /// 获取项目的名称
        /// </summary>
        /// <returns></returns>
        public static string GetProjectName()
        {
            string[] path = Application.dataPath.Split('/');
            //因为我每一个项目的根目录，其上层地址只有两层，即某盘/某文件夹/项目根目录
            return path[path.Length - 2];
        }

        /// <summary>
        /// 获取项目的路径
        /// </summary>
        /// <returns></returns>
        public static string GetProjectPath()
        {
            return Path.GetDirectoryName(Application.dataPath);
        }
    }
}
