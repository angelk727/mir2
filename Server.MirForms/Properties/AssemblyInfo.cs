using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// 有关程序集的常规信息通过以下方式进行控制属性集。 
// 更改这些属性值以修改信息
// 与程序集关联
[assembly: AssemblyTitle("Server")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Server")]
[assembly: AssemblyCopyright("Copyright ©  2022")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 将ComVisible设置为false会使COM组件无法看到此程序集中的类型。
// 如果需要从COM访问此程序集中的类型
// 请将该类型的ComVisible属性设置为true。
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("b0bd7929-94e1-4cd6-b4e9-0628355f44f9")]

// 程序集的版本信息由以下四个值组成：
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// 可以指定所有值
// 也可以使用'*'默认内部版本号和修订号
// 如下所示：[assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("2022.06.12.00")]
[assembly: AssemblyFileVersion("2022.06.12.00")]

[assembly: NeutralResourcesLanguage("en-GB")]

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]