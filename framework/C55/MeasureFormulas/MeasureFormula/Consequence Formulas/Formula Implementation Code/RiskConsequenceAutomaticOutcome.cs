using System.Collections.Generic;
using CL.FormulaHelper.Attributes;
using System.Linq;
using System;
using CL.FormulaHelper;
using MeasureFormulas.Generated_Formula_Base_Classes;
using MeasureFormula.Common_Code;

namespace CustomerFormulaCode
{
    /// <summary>
    /// Returns 0 consequence values.
    /// </summary>
    [Formula]
	public class RiskConsequenceAutomaticOutcome : RiskConsequenceAutomaticOutcomeBase
	{
		public override double?[] GetUnits(int startFiscalYear, int months,
		                                   TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
		{
			return PopulateOutputWithValue(months, 0);

		}
		
		public override double?[] GetZynos(int startFiscalYear, int months, TimeInvariantInputDTO timeInvariantData,
            IReadOnlyList<TimeVariantInputDTO> timeVariantData, double?[] unitOutput)
		{
			return unitOutput;
		}
	}
}
