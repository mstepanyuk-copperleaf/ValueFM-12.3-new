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
	/// Project Execution Risk questionnaire formula calculates the risk of executing an investment (negative value) based on it's cost and various key success factors.
	/// Returns Annual Consequence values.  Expected to be used in conjunction with a likelihood formula that returns monthly values
	/// </summary>
	[Formula]
	public class ProjectExecutionRiskConsequenceMonthly : ProjectExecutionRiskConsequenceMonthlyBase
	{
		public override double?[] GetUnits(int startFiscalYear, int months,
		                                   TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
		{
			var spendValues = GetSpendForAccountType(months, timeInvariantData.InvestmentSpendByAccountType, CustomerConstants.CAPEXAccount);
			
			if (spendValues == null) return null;
			
			if (timeInvariantData.How_32_Well_32_Defined_32_will_32_Project_32_Scope_32_be == null
			    && timeInvariantData.Lining_32_up_32_Right_32_Implementation_32_Partners == null
			    && timeInvariantData.Team_39_s_32_Right_32_Internal_32_Skillset == null
			    && timeInvariantData.Skills_32_to_32_Manage_32_the_32_Project == null
			    && timeInvariantData.Type_32_of_32_Contract == null
			    && timeInvariantData.Proven_32_Implementation_32_Methodology == null
			    && timeInvariantData.Are_32_Initial_32_Effort_32_and_32_Schedule_32_Realistic == null
			    && timeInvariantData.Vendor_32_Industry_32_Experience == null
			    && timeInvariantData.Vendor_32_Technical_32_Experience == null) return null;
			
			double?[] result = new double?[months];
			for (int offset = 0; offset < months; offset++)
			{
				if (spendValues[offset] != null)
				{
					//Planning (Min = 3.3%; Max = 8.3%)
					double? planningFactor = (HelperFunctions.GetCustomFieldValue(timeInvariantData.How_32_Well_32_Defined_32_will_32_Project_32_Scope_32_be) ?? 0)
					                         + (HelperFunctions.GetCustomFieldValue(timeInvariantData.Lining_32_up_32_Right_32_Implementation_32_Partners) ?? 0)
					                         + (HelperFunctions.GetCustomFieldValue(timeInvariantData.Team_39_s_32_Right_32_Internal_32_Skillset) ?? 0);
						
					//Project Management (Min = 3.4%; Max = 8.4%)
					double? projectManagementFactor = (HelperFunctions.GetCustomFieldValue(timeInvariantData.Skills_32_to_32_Manage_32_the_32_Project) ?? 0)
					                                  + (HelperFunctions.GetCustomFieldValue(timeInvariantData.Type_32_of_32_Contract) ?? 0)
					                                  + (HelperFunctions.GetCustomFieldValue(timeInvariantData.Proven_32_Implementation_32_Methodology) ?? 0)
					                                  + (HelperFunctions.GetCustomFieldValue(timeInvariantData.Are_32_Initial_32_Effort_32_and_32_Schedule_32_Realistic) ?? 0);
					
					//Vendor Technical Capability (Min = 3.3%; Max = 8.3%)
					double? vendorFactor = (HelperFunctions.GetCustomFieldValue(timeInvariantData.Vendor_32_Industry_32_Experience) ?? 0)
					                       + (HelperFunctions.GetCustomFieldValue(timeInvariantData.Vendor_32_Technical_32_Experience) ?? 0);
					
					//Total = 25%
					result[offset] = spendValues[offset] * (planningFactor + projectManagementFactor + vendorFactor);
				}
			}
			return result;
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
