using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace IDM_ReInstaller;

internal class Program
{

    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    const int SW_HIDE = 0;
    const int SW_SHOW = 5;

    private static string _sid { get; set; }
    private static List<string> IDMTrialRegistryKeys = new List<string>()
    {
        "{7B8E9164-324D-4A2E-A46D-0165FB2000EC}",
        "{6DDF00DB-1234-46EC-8356-27E7B2051192}",
        "{D5B91409-A8CA-4973-9A0B-59F713D25671}",
        "{07999AC3-058B-40BF-984F-69EB1E554CA7}",
        "{5ED60779-4DE2-4E07-B862-974CA4FF2E9C}"
    };
    private static List<string> _registerNames = new List<string>(){ "FName", "LName", "Email", "Serial", "CheckUpdtVM", "scansk", "tvfrdt", "LastCheckQU", "LstCheck", "radxcnt", "InstallStatus" };
    private static bool _hiddenWindow = false;
    static void Main(string[] args)
    {
        if (args.Contains("-hidden"))
        {
            IntPtr hWnd = GetConsoleWindow();
            ShowWindow(hWnd, SW_HIDE);
            _hiddenWindow = true;
        }

        Console.Title = "IDM ReInstaller - Nereqla";
        // if UAC disable
        if (!IsUserAdministrator())
        {
            WriteToConsole("Lütfen uygulamayı Yönetici olarak çalıştırın!");
            if (!_hiddenWindow) Console.ReadKey();
        }

        _sid = WindowsIdentity.GetCurrent().User.Value;

        KillProcesses();
        DeleteAllRegister();
        DeleteOldSettingsBAKFile();
        DeleteRegistrationInfo();

        if (!_hiddenWindow)
        {
            WriteToConsole("Bitti");
            Console.ReadKey();
        }
    }

    private static void WriteToConsole(string msg)
    {
        if(!_hiddenWindow) Console.WriteLine(msg);
    }

    private static bool IsUserAdministrator()
    {
        bool isAdmin;
        try
        {
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch (UnauthorizedAccessException)
        {
            isAdmin = false;
        }
        catch
        {
            isAdmin = false;
        }
        return isAdmin;
    }

    private static bool KillProcesses()
    {
        var processNameList = new List<string>()
        {
            "IDMan",
            "IEMonitor",
            "IDMMsgHost",
            "IDMIntegrator64",
            "idmBroker",
            "IDMGrHlp",
            "IEMonitor",
            "MediumILStart",
        };
        try
        {
            processNameList.ForEach(processName =>
            {
                foreach (var process in Process.GetProcessesByName(processName))
                {
                    process.Kill();
                    WriteToConsole($"{process.ProcessName} işlemi kapatıldı!");
                }
            });
        }
        catch
        {
            throw new Exception("IDM'yi kapatırken bir hata oluştu!");
        }
        return true;
    }

    private static void DeleteAllRegister()
    {
        IDMTrialRegistryKeys.ForEach(key => 
        {
            DeleteRegistry(key);
            WriteToConsole($"{key} girdisi sistemden silindi!");
        });
    }

    private static void DeleteRegistry(string registryValue)
    {
        Registry.ClassesRoot.DeleteSubKeyTree(@"CLSID\" + registryValue, false);
        Registry.CurrentUser.DeleteSubKeyTree(@"Software\Classes\Wow6432Node\CLSID\" + registryValue, false);
        Registry.CurrentUser.DeleteSubKeyTree(@"Software\Classes\CLSID\" + registryValue, false);
        Registry.LocalMachine.DeleteSubKeyTree(@"Software\Classes\Wow6432Node\CLSID\" + registryValue, false);
        Registry.LocalMachine.DeleteSubKeyTree(@"Software\Classes\CLSID\" + registryValue, false);
        Registry.Users.DeleteSubKeyTree(_sid + @"\Software\Classes\CLSID\" + registryValue, false);
        Registry.Users.DeleteSubKeyTree(_sid + @"_Classes\CLSID\" + registryValue, false);
    }

    private static string DeleteOldSettingsBAKFile()
    {
        string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string FolderToDelete = Path.Combine(AppDataFolder, "DMCache");

        try
        {
            if (Directory.Exists(FolderToDelete))
            {
                Directory.Delete(FolderToDelete, true);
                WriteToConsole($"{FolderToDelete} klasörü silindi!");
            }
        }
        catch (Exception ex)
        {
            WriteToConsole($"Hata: {ex.Message}");
        }

        return FolderToDelete;
    }

    public static bool DeleteRegistrationInfo()
    {
        try
        {
            var subkeyHKCU = Registry.CurrentUser.CreateSubKey(@"Software\DownloadManager", true);
            var subkeyHKLM = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Wow6432Node\Internet Download Manager", true);

            DeleteAll(subkeyHKCU);
            DeleteAll(subkeyHKLM);

        }
        catch (Exception ex)
        {
            WriteToConsole($"Hata: {ex.Message}");
        }
        return true;
    }

    private static void DeleteAll(RegistryKey subkey)
    {
        foreach (var name in _registerNames)
        {
            try
            {
                subkey.DeleteValue(name, true);
                WriteToConsole($"{Path.Combine(subkey.Name,name)} silindi!");
            }
            catch (Exception) { continue; }
        }
    }
}
