using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Text;  
using System.IO;

namespace LineChar
{
    public static class ConfigUtil
    {
        
        /// <summary>  
        /// 配置文件的目录  
        /// </summary>   
        static string appPath = System.Windows.Forms.Application.ExecutablePath;
        public static string conFilePath = appPath.Substring(0,appPath.Length-3)+"ini"; 
        static bool isRead = false;
        /// <summary>  
        /// 配置文件属性值  
        /// </summary>  
        static List<string> configName = new List<string>();//名称集合  
        static List<string> configValue = new List<string>(); //数值集合  

        /// <summary>  
        /// 读取配置文件的属性值  
        /// </summary>  
        static bool ReadConfig()  
        {
            if (isRead == true)
                return true;
            isRead = true;
            //检查配置文件是否存在  
            if (!File.Exists(conFilePath))  
            {
                //FileStream fs =  File.Create(conFilePath);
                //fs.Close();
                return true;
            }  
            StreamReader sr = new StreamReader(conFilePath, Encoding.Default);  
            string line;  
            while ((line = sr.ReadLine()) != null)  
            {  
                line = line.Trim();  
                string cName, cValue;  
                string[] cLine = line.Split('=');  
                if (cLine.Length == 2)  
                {  
                    cName = cLine[0].ToLower();  
                    cValue = cLine[1].ToLower();  
                    configName.Add(cName);  
                    configValue.Add(cValue);  
                }  
            }  
            sr.Close();  
            return true;  
        }    
        /// <summary>  
        /// 返回变量的字符串值  
        /// </summary>  
        /// <param name="cName">变量名称</param>  
        /// <returns>变量值</returns>  


        public static string GetValue(string cName)  
        {
            ReadConfig();
            for (int i = 0; i < configName.Count; i++)  
            {  
                if (configName[i].Equals(cName.ToLower()))  
                {  
                    return configValue[i];
                }  
            }
            return null;  
        }

        /// <summary>  
        /// 设置写入配置文件的值  
        /// </summary>  
        /// <param name="cName">属性名称</param>  
        /// <param name="cValue">值</param>  
        public static void SetValue(string cName, string cValue)
        {
            bool ishere = false;
            ReadConfig();
            //检查是否已经存在.  
            if (configName.Count != 0)
            {
                for (int i = 0; i < configName.Count; i++)
                {
                    if (configName[i].Equals(cName.ToLower()))
                    {
                        configValue[i] = cValue;
                        ishere = true;
                    }
                }
            }
            if (!ishere)
            {
                configName.Add(cName);
                configValue.Add(cValue);
            }
        }  

        /// <summary>  
        /// 将设置的值写入到ini文件中.  
        /// </summary>   
        /// <returns></returns>  
        public static bool Save()
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(conFilePath);
                for (int i = 0; i < configName.Count; i++)
                {
                    sw.WriteLine("{0}={1}", configName[i].ToLower(), configValue[i]);
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                sw.Close();
            }
            return true;
        }
  
    }  
}
