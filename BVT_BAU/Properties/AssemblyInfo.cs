using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MbUnit.Framework;
using Framework;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("BVT_BAU")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Aditi Technologies Pvt Ltd")]
[assembly: AssemblyProduct("BVT_BAU")]
[assembly: AssemblyCopyright("Copyright © Aditi Technologies Pvt Ltd 2015")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("56790a51-fc32-434a-bf7e-988431a1a62e")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

[assembly: ParallelismLimit]
namespace BVT_BAU
{

    [AssemblyFixture]

    class Sanity
    {


        [FixtureTearDown]
        public void AfterRunAssembly()
        {
            BaseTest.EndOfExecution();
            BaseTest bt = new BaseTest();
            ICE.DataRepository.Vegas_IMS_Data.Registration_Data.depLimit = bt.ReadxmlData("regdata", "DepLimit", ICE.DataRepository.DataFilePath.IP2_SeamlessWallet);
        }
    }
}