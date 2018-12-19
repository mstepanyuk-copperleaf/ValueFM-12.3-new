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
    public class CMCarbonT : CMCarbonTBase
    {
        public override double?[] GetUnits(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
    		double?[] companyMileage= timeInvariantData.Mileage_Total_32_mileage_32__8211__32_Company_LikelihoodUnitOutput;
    		double carbonConversion = timeInvariantData.SystemCarbon_32_conversion_32_factor_32__40_per_32_mile_41_ ?? 0;
    		
    		return ArrayHelper.MultiplyStreamOfValuesByConstant(companyMileage, carbonConversion/CustomerConstants.TonnesToKgs); 		
        }
        
        public override double?[] GetZynos(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData,
            IReadOnlyList<TimeVariantInputDTO> timeVariantData,
            double?[] unitOutput)
        {
    		return unitOutput;
        }
    }
}
