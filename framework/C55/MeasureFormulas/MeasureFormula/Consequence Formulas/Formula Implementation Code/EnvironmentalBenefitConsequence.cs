using System.Collections.Generic;
using CL.FormulaHelper.Attributes;
using System.Linq;
using System;
using CL.FormulaHelper;
using MeasureFormulas.Generated_Formula_Base_Classes;
using MeasureFormula.Common_Code;
using MeasureFormula.SharedCode;

namespace CustomerFormulaCode
{
	/// <summary>
	/// Environmental Benefit  Questionnaire formula calculates the consequence of an investment that reduces CO2 emissions, SF6 emissions or energy use.
	/// This formula uses constants specified in CustomerConstants.cs
	/// Returns Annual Consequence values.  Expected to be used in conjunction with a likelihood formula that returns monthly values
	/// </summary>
	[Formula]
	public class EnvironmentalBenefitConsequence : EnvironmentalBenefitConsequenceBase
	{
		public override double?[] GetUnits(int startFiscalYear, int months,
		                                   TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
		{
	        var envBenefit = timeInvariantData.CO2_32_Reduction * (timeInvariantData.SystemGHGValue ?? 0) +
                timeInvariantData.SF6_32_Reduction * (timeInvariantData.SystemGHGValue ?? 0) * CommonConstants.TonnesCO2PerKgSF6 +
                timeInvariantData.Energy_32_Savings * (timeInvariantData.SystemEnergySavingsValueDollarsPerMWh ?? 0);

            return PopulateOutputWithValue (months, envBenefit);
            
		}
		
		public override double?[] GetZynos(int startFiscalYear, int months,
		                                   TimeInvariantInputDTO timeInvariantData,
		                                   IReadOnlyList<TimeVariantInputDTO> timeVariantData,
		                                   double?[] unitOutput)
		{
            return ConvertUnitsToZynos(unitOutput, CustomerConstants.DollarToZynoConversionFactor);
		}
	}
}
