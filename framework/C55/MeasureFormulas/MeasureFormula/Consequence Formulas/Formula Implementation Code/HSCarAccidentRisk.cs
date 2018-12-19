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
    public class HSCarAccidentRisk : HSCarAccidentRiskBase
    {
        public override double?[] GetUnits(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData)
        {
            return null;
            
            // TODO - add measure
//            double?[] NoCar = timeInvariantData.Health_32_and_32_safety_Number_32_of_32_car_32_accidents_LikelihoodUnitOutput;
//    		double CostCar = timeInvariantData.SystemCarAccidentCost ?? 0;
    		
//    		return ArrayHelper.MultiplyStreamOfValuesByConstant(NoCar, CostCar);
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
