// 현재 실행 중인 어셈블리의 위치를 가져옴
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

[DllImport("kernel32")]
static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
                                                        int size, string filePath);
[DllImport("kernel32")]
static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

void Version_iniWrite()
{
    string version;

    // 버전 증가부분은 RunVersionIniWrite.bat로 만들어서 빌드이벤트 후 call로 넣어줌
    StringBuilder ret = new StringBuilder();

    string DirPath = Environment.CurrentDirectory + @"\ini";
    DirectoryInfo di = new DirectoryInfo(DirPath);
    if (!di.Exists) Directory.CreateDirectory(DirPath);

    // 빌드 할때마다 version을 자동으로 1씩 증가시킴

    GetPrivateProfileString("version", "build", "(NONE)", ret, 255, Environment.CurrentDirectory + @"\ini" + "\\version.ini");

    if (ret.ToString() == "(NONE)")
    {
        version = "1";
    }
    else
    {
        version = ret.ToString();
    }

    Console.WriteLine("Previous version : " + version);
    int versionnum = int.Parse(version);
    version = (versionnum + 1).ToString();


    WritePrivateProfileString("version",
            "build",
            version,
            Environment.CurrentDirectory + @"\ini" + "\\version.ini");
    Console.WriteLine("After version : " + version);
}

Version_iniWrite();