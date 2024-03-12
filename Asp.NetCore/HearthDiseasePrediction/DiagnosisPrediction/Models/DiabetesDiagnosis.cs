using System;
using System.ComponentModel.DataAnnotations;

namespace DiagnosisPrediction.Models
{
    public class DiabetesDiagnosis
    {
        [Range(18, 100, ErrorMessage = "Enter a valid age between 18 and 100.")]
        public int Age { get; set; }
        public Genders Sex { get; set; }    // 0 for female, 1 for male

        [Range(18, 30, ErrorMessage = "Enter a valid BMI between 18 and 30.")]
        public int BMI { get; set; }
        public HealthStatus GenHlth { get; set; }  // nominal, 1 to 5, 1 = excellent, 5 = poor
        public IncomeRange Income { get; set; }  
        public Confirm HighChol { get; set; }    // 0 = no high cholesterol, 1 = yes
        public Confirm CholCheck { get; set; }    // 0 = no cholesterol check in 5 years, 1 = yes
        public Confirm HearthDiseaseorAttack { get; set; }    // coronary heart disease (CHD) or myocardial infarction (MI), 0 = no, 1 = yes

        public Confirm HvyAlcoholConsump { get; set; }    // Heavy drinkers (adult men having more than 14 drinks per week and adult women having more than 7 drinks per week) 0 = no, 1 = yes

        [Range(0, 30, ErrorMessage = "Enter a valid number of days between 0 and 30.")]
        public int PhsylHlth { get; set; }      // 0 to 30, for how many days during the past 30 days your physical health wasn't good

        public int Result { get; set; }
    }

    public enum Genders
    {
        Female,
        Male
    }

    public enum Confirm
    {
        No,
        Yes
    }

    public enum HealthStatus
    {
        Excellent = 1,
        VeryGood,
        Good,
        Fair,
        Poor
    }

    public enum IncomeRange
    {
        [Display(Name = "Less Than $10,000")]LessThan10000 = 1,
        [Display(Name = "From $10,000 To $15,000")]From10000To15000,
        [Display(Name = "From $15,001 To $20,000")]From15001To20000,
        [Display(Name = "From $20,001 To $25,000")]From20001To25000,
        [Display(Name = "From $30,001 To $35,000")]From30001To35000,
        [Display(Name = "From $40,001 To $55,000")]From40001To55000,
        [Display(Name = "From $55,001 To $75,000")]From55001To75000,
        [Display(Name = "More Than $75,000")]MoreThan75000
    }
}
