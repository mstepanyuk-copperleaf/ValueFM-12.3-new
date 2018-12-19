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
	/// Productive Workplace Questionnaire formula calculates the consequence of an investment that improves employees recruitment, engagement or productivity.
	/// This formula uses constants specified in CustomerConstants.cs
	/// Returns Annual Consequence values.  Expected to be used in conjunction with a likelihood formula that returns monthly values
	/// </summary>
	[Formula]
	public class ProductWorkplaceBenBenefitConsequence : ProductWorkplaceBenBenefitConsequenceBase
	{
		public override double?[] GetUnits(int startFiscalYear, int months,
		                                   TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
		{
			return InterpolatePropagate<TimeVariantInputDTO>(timeVariantData, startFiscalYear, months,
	                                                         x => x.Number_32_of_32_Candidates_32_Attracted * x.Workplace_32_Impact_32_On_32_Attractiveness.Value *
	                                                                        CustomerConstants.ValuePerCandidateAttracted + x.Number_32_of_32_Employees_32_Affected *
	                                                                        x.Workplace_32_Impact_32_On_32_Productivity.Value * CustomerConstants.EmployeeCostPerYear +
			                                                                x.Number_32_of_32_Employees_32_At_32_Risk_32_Of_32_Leaving * x.Workplace_32_Impact_32_On_32_Productivity.Value *
			                                                                CustomerConstants.EmployeeCostToReplace * 0.1d);
		}

		public override double?[] GetZynos(int startFiscalYear, int months, TimeInvariantInputDTO timeInvariantData,
		                                   IReadOnlyList<TimeVariantInputDTO> timeVariantData, double?[] unitOutput)
		{
            return ConvertUnitsToZynos(unitOutput, CustomerConstants.DollarToZynoConversionFactor);
		}
	}
}
