using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WinFormLib.Core;

namespace WinFormLib.Components
{
    public class FormBase : System.Windows.Forms.Form
    {
        public FormBase()
        {
            
        }
        protected void WriteXml(string fileName, params ISerializeStyle[] SerializeItem)
        {

            try
            {
                string dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TableView");
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
                int index = 1;
                foreach (ISerializeStyle item in SerializeItem)
                {
                    if (item == null) continue;
                    string FileName = Path.Combine(dirName, fileName + "-" + index + ".xml");
                    if (File.Exists(FileName))
                        File.Delete(FileName);

                    using (FileStream fs = new FileStream(FileName, FileMode.CreateNew))
                    {
                        StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                        sw.Write(item.ToSerializeStyle());
                        sw.Close();
                    }
                    index++;
                }
                MessageBoxHelper.Show("保存视图成功!");
            }
            catch (Exception ex)
            {
                MessageBoxHelper.Show(ex.Message);
            }


        }
        protected void ReadXml(string fileName, params ISerializeStyle[] SerializeItem)
        {
            string dirName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TableView");
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            int index = 1;
            foreach (ISerializeStyle item in SerializeItem)
            {
                if (item == null) continue;
                string FileName = Path.Combine(dirName, fileName + "-" + index + ".xml");
                string xml = string.Empty;
                if (File.Exists(FileName))
                {
                    using (FileStream fs = new FileStream(FileName, FileMode.Open))
                    {
                        StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                        xml = sr.ReadToEnd();
                        item.FromSerializeStyle(xml);
                        sr.Close();
                    }
                }
                index++;
            }
        }
    }
}
