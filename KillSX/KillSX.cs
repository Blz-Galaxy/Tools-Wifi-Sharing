using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace KillSX
{
    class KillSX
    {
        public int GetProcessInfo(string ProcessName)
        {            
            MessageBox.Show("正在查找" + ProcessName);
            Process[] processes = Process.GetProcessesByName(ProcessName);
            foreach (Process instance in processes)
            {
                try
                {
                    if (instance.ProcessName == ProcessName)
                        return instance.Id;
                }
                catch { }
            }
            return -1;
        }



        #region 结束指定进程
        ///  
        /// 结束指定进程 
        ///  
        /// 进程的 Process ID 
        public static void EndProcess(int pid)
        {
            if (pid == -1)
            {
                MessageBox.Show("未能找到指定程序！");
                return;
            }
            try
            {
                Process process = Process.GetProcessById(pid);
                process.Kill();
                MessageBox.Show("成功关闭指定程序！");
            }
            catch { }
        }
        #endregion

        //static void Main(string[] args)
        //{
        //    KillSX k = new KillSX();
        //    int shanxun = k.GetProcessInfo(args[0]);
        //    EndProcess(shanxun);
        //}
    }
}
