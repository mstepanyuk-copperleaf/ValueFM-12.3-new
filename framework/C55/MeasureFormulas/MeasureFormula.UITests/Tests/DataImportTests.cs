using System.IO;
using MeasureFormula.UITests.Bases;
using MeasureFormula.UITests.ExtensionMethods;
using MeasureFormula.UITests.Helpers;
using MeasureFormula.UITests.Pages;
using NUnit.Framework;

namespace MeasureFormula.UITests.Tests
{
    public class DataImportTests : TestBase
    {
        private string ValueFrameworkFilePath => GetFilePath();

        public override void BeforeEach()
        {
            Actions.LoginAsAdmin(Driver);
        }


        [Test, Ignore("July 26, wait until we get CI setup")]
        public void Can_import_value_framework()
        {
            var dataImportPage = DataImportPage.NavigateToThisPageViaUrl(Driver);
            Assert.IsTrue(dataImportPage.ImportFile(ValueFrameworkFilePath));

            var errorLogPage = ErrorLogPage.NavigateToThisPageViaUrl(Driver);
            var sussessfulImport = errorLogPage.GetDataImportMessageAndParse();
            Assert.IsTrue(sussessfulImport);
        }

        private static string GetFilePath()
        {
            var assemblyLocalPath = AssemblyPathManager.SetupAssemblyPath();
            return new FileInfo(Path.Combine(assemblyLocalPath,
                @"..\..\..\..\..\..\tests\VFLoader.xlsx")).FullName;
        }
    }
}
