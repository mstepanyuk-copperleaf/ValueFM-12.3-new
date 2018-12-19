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
    [Formula]
    public class EmployeeProductivityBenefitConsequence : EmployeeProductivityBenefitConsequenceBase
    {
        public override double?[] GetUnits(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
            return InterpolatePropagate<TimeVariantInputDTO>(timeVariantData,
			                                                 startFiscalYear,
			                                                 months, (x => ((x.NoFieldEmployees*
			                                                          x.FieldHoursSaved*
			                                                          timeInvariantData.SystemFieldEmployeeCostperHour +
			                                                          x.NoManagerEmployees*
			                                                          x.ManagerHoursSaved*
			                                                          timeInvariantData.SystemManagerCostperHour +
			                                                          x.NoOfficeEmployees*
			                                                          x.OfficeHoursSaved*
			                                                          timeInvariantData.SystemOfficeEmployeeCostperHour)*
			                                                          timeInvariantData.SystemProbabilityofRepurposing
																	- (x.NoFieldEmployees*
			                                                          x.FieldHoursAdditional*
			                                                          timeInvariantData.SystemFieldEmployeeCostperHour +
			                                                          x.NoManagerEmployees*
			                                                          x.ManagerHoursAdditional*
			                                                          timeInvariantData.SystemManagerCostperHour +
			                                                          x.NoOfficeEmployees*
			                                                          x.OfficeHoursAdditional*
			                                                          timeInvariantData.SystemOfficeEmployeeCostperHour)			                                                          
			                                                         )));
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
