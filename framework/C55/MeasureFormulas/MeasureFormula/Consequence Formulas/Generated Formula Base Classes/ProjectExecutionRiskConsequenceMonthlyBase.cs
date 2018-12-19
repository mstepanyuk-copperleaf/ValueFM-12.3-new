// GENERATED CODE - DO NOT EDIT !!!
using System.Collections.Generic;
using CL.FormulaHelper;
using CL.FormulaHelper.Attributes;
using CL.FormulaHelper.DTOs;
using System.Runtime.Serialization;

namespace MeasureFormulas.Generated_Formula_Base_Classes
{
    [FormulaBase]
    public abstract class ProjectExecutionRiskConsequenceMonthlyBase : FormulaConsequenceBase
    {
        [DataContract]
        public class TimeInvariantInputDTO
        {
            public TimeInvariantInputDTO(
                CL.FormulaHelper.DTOs.CustomFieldListItemDTO p_Are_32_Initial_32_Effort_32_and_32_Schedule_32_Realistic,
                CL.FormulaHelper.DTOs.CustomFieldListItemDTO p_How_32_Well_32_Defined_32_will_32_Project_32_Scope_32_be,
                CL.FormulaHelper.DTOs.DistributionByAccountTypeDTO p_InvestmentSpendByAccountType,
                CL.FormulaHelper.DTOs.CustomFieldListItemDTO p_Lining_32_up_32_Right_32_Implementation_32_Partners,
                System.String p_Methodology_32_and_32_Track_32_Record,
                System.String p_PM_39_s_32_Previous_32_Experience,
                CL.FormulaHelper.DTOs.CustomFieldListItemDTO p_Proven_32_Implementation_32_Methodology,
                CL.FormulaHelper.DTOs.CustomFieldListItemDTO p_Skills_32_to_32_Manage_32_the_32_Project,
                CL.FormulaHelper.DTOs.CustomFieldListItemDTO p_Team_39_s_32_Right_32_Internal_32_Skillset,
                CL.FormulaHelper.DTOs.CustomFieldListItemDTO p_Type_32_of_32_Contract,
                CL.FormulaHelper.DTOs.CustomFieldListItemDTO p_Vendor_32_Industry_32_Experience,
                CL.FormulaHelper.DTOs.CustomFieldListItemDTO p_Vendor_32_Technical_32_Experience)
            {
                Are_32_Initial_32_Effort_32_and_32_Schedule_32_Realistic = p_Are_32_Initial_32_Effort_32_and_32_Schedule_32_Realistic;
                How_32_Well_32_Defined_32_will_32_Project_32_Scope_32_be = p_How_32_Well_32_Defined_32_will_32_Project_32_Scope_32_be;
                InvestmentSpendByAccountType = p_InvestmentSpendByAccountType;
                Lining_32_up_32_Right_32_Implementation_32_Partners = p_Lining_32_up_32_Right_32_Implementation_32_Partners;
                Methodology_32_and_32_Track_32_Record = p_Methodology_32_and_32_Track_32_Record;
                PM_39_s_32_Previous_32_Experience = p_PM_39_s_32_Previous_32_Experience;
                Proven_32_Implementation_32_Methodology = p_Proven_32_Implementation_32_Methodology;
                Skills_32_to_32_Manage_32_the_32_Project = p_Skills_32_to_32_Manage_32_the_32_Project;
                Team_39_s_32_Right_32_Internal_32_Skillset = p_Team_39_s_32_Right_32_Internal_32_Skillset;
                Type_32_of_32_Contract = p_Type_32_of_32_Contract;
                Vendor_32_Industry_32_Experience = p_Vendor_32_Industry_32_Experience;
                Vendor_32_Technical_32_Experience = p_Vendor_32_Technical_32_Experience;
            }
            
            [PromptInput("Are Initial Effort and Schedule Realistic")]
            [DataMember]
            public CL.FormulaHelper.DTOs.CustomFieldListItemDTO Are_32_Initial_32_Effort_32_and_32_Schedule_32_Realistic  { get; private set; }
            
            [PromptInput("How Well Defined will Project Scope be")]
            [DataMember]
            public CL.FormulaHelper.DTOs.CustomFieldListItemDTO How_32_Well_32_Defined_32_will_32_Project_32_Scope_32_be  { get; private set; }
            
            [CoreFieldInput(FormulaCoreFieldInputType.InvestmentSpendByAccountType)]
            [DataMember]
            public CL.FormulaHelper.DTOs.DistributionByAccountTypeDTO InvestmentSpendByAccountType  { get; private set; }
            
            [PromptInput("Lining up Right Implementation Partners")]
            [DataMember]
            public CL.FormulaHelper.DTOs.CustomFieldListItemDTO Lining_32_up_32_Right_32_Implementation_32_Partners  { get; private set; }
            
            [PromptInput("Methodology and Track Record")]
            [DataMember]
            public System.String Methodology_32_and_32_Track_32_Record  { get; private set; }
            
            [PromptInput("PM's Previous Experience")]
            [DataMember]
            public System.String PM_39_s_32_Previous_32_Experience  { get; private set; }
            
            [PromptInput("Proven Implementation Methodology")]
            [DataMember]
            public CL.FormulaHelper.DTOs.CustomFieldListItemDTO Proven_32_Implementation_32_Methodology  { get; private set; }
            
            [PromptInput("Skills to Manage the Project")]
            [DataMember]
            public CL.FormulaHelper.DTOs.CustomFieldListItemDTO Skills_32_to_32_Manage_32_the_32_Project  { get; private set; }
            
            [PromptInput("Team's Right Internal Skillset")]
            [DataMember]
            public CL.FormulaHelper.DTOs.CustomFieldListItemDTO Team_39_s_32_Right_32_Internal_32_Skillset  { get; private set; }
            
            [PromptInput("Type of Contract")]
            [DataMember]
            public CL.FormulaHelper.DTOs.CustomFieldListItemDTO Type_32_of_32_Contract  { get; private set; }
            
            [PromptInput("Vendor Industry Experience")]
            [DataMember]
            public CL.FormulaHelper.DTOs.CustomFieldListItemDTO Vendor_32_Industry_32_Experience  { get; private set; }
            
            [PromptInput("Vendor Technical Experience")]
            [DataMember]
            public CL.FormulaHelper.DTOs.CustomFieldListItemDTO Vendor_32_Technical_32_Experience  { get; private set; }
        }
        
        [DataContract]
        public class TimeVariantInputDTO : ITimeVariantInputDTO
        {
            public TimeVariantInputDTO(
                CL.FormulaHelper.DTOs.TimePeriodDTO p_TimePeriod)
            {
                TimePeriod = p_TimePeriod;
            }
            
            [CoreFieldInput(FormulaCoreFieldInputType.TimePeriod)]
            [DataMember]
            public CL.FormulaHelper.DTOs.TimePeriodDTO TimePeriod  { get; private set; }
        }
        
        public abstract double?[] GetUnits(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData, IReadOnlyList<TimeVariantInputDTO> timeVariantData);
            
        public abstract double?[] GetZynos(int startFiscalYear, int months,
            TimeInvariantInputDTO timeInvariantData,
            IReadOnlyList<TimeVariantInputDTO> timeVariantData,
            double?[] unitOutput);
        
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
