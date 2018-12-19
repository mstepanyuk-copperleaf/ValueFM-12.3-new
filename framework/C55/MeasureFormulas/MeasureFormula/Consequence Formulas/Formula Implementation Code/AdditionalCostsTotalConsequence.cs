using System.Collections.Generic;
using CL.FormulaHelper.Attributes;
using System.Linq;
using System;
using CL.FormulaHelper;
using MeasureFormulas.Generated_Formula_Base_Classes;
using MeasureFormula.Common_Code;

namespace CustomerFormulaCode
{
	[Formula]
	/// <summary>
	/// Returns a positive value for the total additional costs incurred by completing an investment (either capital or O&M).  it is assumed that negative polarity will be applied in the value function.
	/// Annual values entered in the questionnaire are converted to a monthly equivalent as no probability is specified for this formula.
	/// </summary>
	public class AdditionalCostsTotalConsequence : AdditionalCostsTotalConsequenceBase
	{
		public override double?[] GetUnits(int startFiscalYear, int months,
		                                   TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
		{
        	double applicableLabourRate = 
        		(timeInvariantData.Account_32_Type.ValueAsInteger == CustomerConstants.OMAAcctID)  ?
        		CustomerConstants.OPEXLabourHour : CustomerConstants.CAPEXLabourHour;

			return InterpolatePropagate<TimeVariantInputDTO>(timeVariantData,
			                                                 startFiscalYear,
			                                                 months, (x => (x.Additional_32_Costs + 
			                                                 	x.Additional_32_Hours * applicableLabourRate)));
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
