using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MbUnit.Framework;
using Framework;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Regression_AnW_stg")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("Regression_AnW_stg")]
[assembly: AssemblyCopyright("Copyright © Microsoft 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("7ae8d702-d3c0-4d49-a25a-0ba330b24b2f")]

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
namespace Regression_AnW_Suite2
{
    [AssemblyFixture]
    class Regression_AnW_Suite2
    {


        [FixtureTearDown]
        public void AfterRunAssembly()
        {
            BaseTest.EndOfExecution();
        }
    }
}