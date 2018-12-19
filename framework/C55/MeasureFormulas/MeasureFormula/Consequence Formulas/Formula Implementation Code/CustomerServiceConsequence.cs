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
	public class CustomerServiceConsequence : CustomerServiceConsequenceBase
	{
		public override double?[] GetUnits(int startFiscalYear, int months,
		                                   TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
		{
			return InterpolatePropagate<TimeVariantInputDTO>(timeVariantData,
			                                                 startFiscalYear,
			                                                 months, (x => (x.Agent_32_Time_32_Saved*CustomerConstants.AgentTimeCostPerHour +
			                                                                x.Low_32_Effort_32_Resolutions*CustomerConstants.SavingsPerLowEffortResolution +
			                                                                x.Self_32_Service_32_Resolutions*CustomerConstants.SavingsPerSelfServiceResoultion)));
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
