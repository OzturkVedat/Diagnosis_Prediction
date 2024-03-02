using System.ComponentModel.DataAnnotations;

namespace HearthDiseasePrediction.Models
{
    public class HearthDiseaseDiagnosis
    {
        [Range(18, 100, ErrorMessage = "Enter a valid age between 18 and 100.")]
        public int Age { get; set; }
        public Genders Gender { get; set; }    // 0 for female, 1 for male
        public ChestPains ChestPainType { get; set; }  // nominal, 0 to 3

        [Range(71, 202, ErrorMessage = "Enter a value between 71 and 202.")]
        public int MaxHearthRate { get; set; }

        [Range(94, 200, ErrorMessage = "Enter a value between 94 and 200.")]
        public int RestingBloodPressure { get; set; }

        [Range(126, 564, ErrorMessage = "Enter a value between 126 and 564.")]
        public int SerumCholesterol { get; set; }

        public Confirm FastingBloodSugar { get; set; }    //  0 for false, 1 for true (fbs >? 120 mg/dl)

        public RestingEKG RestingElectrocardio { get; set; }   // nominal, 0 to 2

        public Confirm ExerciseAngina { get; set; }    //  0= no, 1= yes 

        [Range(0, 6.2, ErrorMessage = "Enter a value between 0 and 6.2")]
        public decimal OldPeak { get; set; }  // treadmill test for ST depression (ranged 0-6.2)

        public SlopeST SlopeST { get; set; }   // slope of the peak exercise ST segment ,nominal, 1 to 3

        public int MajorVessels { get; set; }   // slope of the peak exercise ST segment ,nominal, 1 to 3

        public int Result { get; set; }

    }
    public class PredictionResult
    {
        public int Prediction { get; set; }
    }
    public enum ChestPains
    {
        [Display(Name = "Typical Angina")]
        TypicalAngina,
        [Display(Name = "Atypical Angina")]
        AtypicalAngina,
        [Display(Name = "Non-anginal Pain")]
        NonAnginalPain,
        [Display(Name = "Asymptomatic")]
        Asymptomatic
    }
    public enum RestingEKG
    {
        [Display(Name = "Normal")]
        Normal,
        [Display(Name = "ST-T Wave Abnormality")]
        HavingST_TWaveAbnormality,
        [Display(Name = "Probable/Definite Left Ventricular Hypertrophy by Estes' Criteria")]
        VentricularHypertrophy,
    }
    public enum SlopeST
    {
        Unsloping = 1,
        Flat,
        Downsloping
    }
}
