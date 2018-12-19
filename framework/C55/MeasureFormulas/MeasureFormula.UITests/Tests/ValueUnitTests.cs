using System.Collections.Generic;
using MeasureFormula.UITests.Bases;
using MeasureFormula.UITests.ExtensionMethods;
using MeasureFormula.UITests.Pages;
using NUnit.Framework;

namespace MeasureFormula.UITests.Tests
{
    [Ignore("August 08, 2018 = needs discussion")]
    public class ValueUnitTests : TestBase
    {
        public override void BeforeAll()
        {
            Actions.LoginAsAdmin(Driver);
        }

        [Test]
        public void ValueMeasuresPage_ConsequenceUnitUsesValueUnits()
        {
            var failedLinks = new List<string>();

            var valueMeasuresPage = ValueMeasuresPage.NavigateToThisPageViaUrl(Driver);
            var riskValueMeasuresList = valueMeasuresPage.GetRiskValueMeasuresPageLinks();
            foreach (var riskValueMeasure in riskValueMeasuresList)
            {
                riskValueMeasure.Click();
                if (!valueMeasuresPage.IsConsequenceUnitUsesValueUnit(riskValueMeasure))
                {
                    failedLinks.Add(riskValueMeasure.Text);
                }
            }
            Assert.That(failedLinks.Count == 4);
        }

        [Test]
        public void UnitsPage_ValueUnitHasDescription()
        {
            const string expectedDescription = "Units as described by the risk matrix.";

            var unitsPage = UnitsPage.NavigateToThisPageViaUrl(Driver);
            Assert.AreEqual(expectedDescription, unitsPage.ValueUnitDescription);
        }
    }
}
