using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace NETSpider
{
    static class Program
    {
        private static ApplicationContext context;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                bool canCreate = false;
                Mutex mutex = new Mutex(true, "79AA56EC-00DE-478d-8717-FD873BD10870", out canCreate);
                if (!canCreate)
                {
                    MessageBox.Show("程序已经在运行！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //处理UI线程异常   
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                //处理非UI线程异常   
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                frmStart sf = new frmStart();
                sf.Show();
                context = new ApplicationContext();
                context.Tag = sf;
                Application.Idle += new EventHandler(Application_Idle); //注册程序运行空闲去执行主程序窗体相应初始化代码
                Application.Run(context);
            }
            catch (Exception ex)
            {
                string str = "";
                string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";

                if (ex != null)
                {
                    str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                         ex.GetType().Name, ex.Message, ex.StackTrace);
                }
                else
                {
                    str = string.Format("应用程序线程错误:{0}", ex);
                }


                DMSFrame.Loggers.LoggerManager.FileLogger.LogWithTime(str);
                MessageBox.Show("发生致命错误，请及时联系作者！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private static void Application_Idle(object sender, EventArgs e)
        {
            Application.Idle -= new EventHandler(Application_Idle);
            if (context.MainForm == null)
            {
                frmMain mf = new frmMain();
                context.MainForm = mf;
                frmStart sf = (frmStart)context.Tag;
                sf.label3.Text = "正在初始化中.....";
                mf.FormInit();
                sf.label3.Text = "初始化成功.....";
                Application.DoEvents();
                sf.Close();                                 //关闭启动窗体
                sf.Dispose();
                mf.Show();                                  //启动主程序窗体

            }
        }
        public static string GetConfigPath()
        {
            string dirPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            return dirPath;
        }
        public static string GetProgramPath(string filePath)
        {
            string dirPath = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(filePath))
            {
                dirPath = System.IO.Path.Combine(dirPath, filePath);
            }
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }
            return dirPath;
        }
        public static string GetConfigPath(string fileName)
        {
            return System.IO.Path.Combine(GetConfigPath(), fileName);
        }

        /// <summary>
        ///这就是我们要在发生未处理异常时处理的方法，我这是写出错详细信息到文本，如出错后弹出一个漂亮的出错提示窗体，给大家做个参考
        ///做法很多，可以是把出错详细信息记录到文本、数据库，发送出错邮件到作者信箱或出错后重新初始化等等
        ///这就是仁者见仁智者见智，大家自己做了。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {

            string str = "";
            string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            Exception error = e.Exception as Exception;
            if (error != null)
            {
                str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                     error.GetType().Name, error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("应用程序线程错误:{0}", e);
            }

            DMSFrame.Loggers.LoggerManager.FileLogger.LogWithTime(str);
            MessageBox.Show("发生致命错误，请及时联系作者！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string str = "";
            Exception error = e.ExceptionObject as Exception;
            string strDateInfo = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            if (error != null)
            {
                str = string.Format(strDateInfo + "Application UnhandledException:{0};\n\r堆栈信息:{1}", error.Message, error.StackTrace);
            }
            else
            {
                str = string.Format("Application UnhandledError:{0}", e);
            }

            DMSFrame.Loggers.LoggerManager.FileLogger.LogWithTime(str);
            MessageBox.Show("发生致命错误，请停止当前操作并及时联系作者！", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
