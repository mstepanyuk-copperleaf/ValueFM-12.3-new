// GENERATED CODE - DO NOT EDIT !!!
using System.Collections.Generic;
using CL.FormulaHelper;
using CL.FormulaHelper.Attributes;
using CL.FormulaHelper.DTOs;
using System.Runtime.Serialization;

namespace MeasureFormulas.Generated_Formula_Base_Classes
{
    [FormulaBase]
    public abstract class FireOrExplosionLikelihoodBase : FormulaLikelihoodBase
    {
        [DataContract]
        public class TimeInvariantInputDTO
        {
            public TimeInvariantInputDTO()
            {
            }
        }
        
        [DataContract]
        public class TimeVariantInputDTO : ITimeVariantInputDTO
        {
            public TimeVariantInputDTO(
                System.Double p_FailureProbability,
                System.Double p_Gen_32_Asset_32_Prob_32_Failure_32_Creates_32_Hazard,
                System.Double p_Gen_32_Asset_32_Prob_32_Person_32_In_32_Danger_32_Zone,
                System.Double p_Gen_32_Asset_32_Prob_32_Person_32_Injured,
                CL.FormulaHelper.DTOs.TimePeriodDTO p_TimePeriod)
            {
                FailureProbability = p_FailureProbability;
                Gen_32_Asset_32_Prob_32_Failure_32_Creates_32_Hazard = p_Gen_32_Asset_32_Prob_32_Failure_32_Creates_32_Hazard;
                Gen_32_Asset_32_Prob_32_Person_32_In_32_Danger_32_Zone = p_Gen_32_Asset_32_Prob_32_Person_32_In_32_Danger_32_Zone;
                Gen_32_Asset_32_Prob_32_Person_32_Injured = p_Gen_32_Asset_32_Prob_32_Person_32_Injured;
                TimePeriod = p_TimePeriod;
            }
            
            [PromptInput("FailureProbability")]
            [DataMember]
            public System.Double FailureProbability  { get; private set; }
            
            [PromptInput("Gen Asset Prob Failure Creates Hazard")]
            [DataMember]
            public System.Double Gen_32_Asset_32_Prob_32_Failure_32_Creates_32_Hazard  { get; private set; }
            
            [PromptInput("Gen Asset Prob Person In Danger Zone")]
            [DataMember]
            public System.Double Gen_32_Asset_32_Prob_32_Person_32_In_32_Danger_32_Zone  { get; private set; }
            
            [PromptInput("Gen Asset Prob Person Injured")]
            [DataMember]
            public System.Double Gen_32_Asset_32_Prob_32_Person_32_Injured  { get; private set; }
            
            [CoreFieldInput(FormulaCoreFieldInputType.TimePeriod)]
            [DataMember]
            public CL.FormulaHelper.DTOs.TimePeriodDTO TimePeriod  { get; private set; }
        }
        
        public abstract double?[] GetLikelihoodValues(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData);
            
        
        ///
        /// Class to enable formula debugging
        ///
        [DataContract]
        public class FormulaParams : IFormulaParams
        {
            public FormulaParams(CL.FormulaHelper.MeasureOutputType measureOutputType,
                string measureName,
                long alternativeId,
                string formulaImplClassName,
                bool isProbabilityFormula,
                int fiscalYearEndMonth,
                int startFiscalYear,
                int months,
                TimeInvariantInputDTO timeInvariantData,
                IReadOnlyList<TimeVariantInputDTO> timeVariantData,
                double?[] unitOutput,
                object formulaOutput,
                string exceptionMessage)
            {
                MeasureOutputType = measureOutputType;
                MeasureName = measureName;
                AlternativeId = alternativeId;
                FormulaImplClassName = formulaImplClassName;
                IsProbabilityFormula = isProbabilityFormula;
                FiscalYearEndMonth = fiscalYearEndMonth;
                StartFiscalYear = startFiscalYear;
                Months = months;
                TimeInvariantData = timeInvariantData;
                TimeVariantData = timeVariantData;
                UnitOutput = unitOutput;
                FormulaOutput = formulaOutput;
                ExceptionMessage = exceptionMessage;
            }
            [DataMember(Order = 0)]
            public CL.FormulaHelper.MeasureOutputType MeasureOutputType { get; set; }
            [DataMember(Order = 1)]
            public string MeasureName { get; set; }
            [DataMember(Order = 2)]
            public long AlternativeId { get; set; }
            [DataMember(Order = 3)]
            public string FormulaImplClassName { get; set; }
            [DataMember(Order = 4)]
            public bool IsProbabilityFormula { get; set; }
            [DataMember(Order = 5)]
            public int FiscalYearEndMonth { get; set; }
            [DataMember(Order = 6)]
            public int StartFiscalYear { get; set; }
            [DataMember(Order = 7)]
            public int Months { get; set; }
            [DataMember(Order = 8)]
            public TimeInvariantInputDTO TimeInvariantData { get; set; }
            [DataMember(Order = 9)]
            public IReadOnlyList<TimeVariantInputDTO> TimeVariantData { get; set; }
            [DataMember(Order = 10)]
            public double?[] UnitOutput { get; set; }
            [DataMember(Order = 11)]
            public object FormulaOutput { get; set; }
            [DataMember(Order = 12)]
            public string ExceptionMessage { get; set; }
        }
    }
}
// GENERATED CODE - DO NOT EDIT !!!
