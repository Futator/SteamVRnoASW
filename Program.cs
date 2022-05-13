using System;
using System.IO;
using Microsoft.Win32;

namespace SteamVRnoASW
{
    public class SteamVRNoAsw
    {
        //create the command file for the oculus CLI
        public static void ASWCommandFile()
        {
            StreamWriter sw = new StreamWriter(".\\asw_off.txt");
            sw.WriteLine("server:asw.Off");
            sw.WriteLine("exit");
            sw.Close();
        }
        public static void GetCLIPath()
        {
            // second try at getting path but more complex and actually working
            string oculusPath = String.Empty;
            RegistryKey localKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            localKey = localKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\Oculus");
            if (localKey != null)
            {
                oculusPath = localKey.GetValue("InstallLocation").ToString();
            }
            string cliPath = oculusPath + @"Support\oculus-diagnostics\OculusDebugToolCLI.exe";
            System.Diagnostics.Process.Start(cliPath, "/C -f asw_off.txt");
        }
        public static void Main(string[] args)
        {
            ASWCommandFile();
            System.Threading.Thread.Sleep(1000);
            GetCLIPath();
            System.Threading.Thread.Sleep(1000);
            File.Delete(".\\asw_off.txt");
            System.Diagnostics.Process.Start("cmd.exe", "/C start steam://launch/250820");
        }
    }
}