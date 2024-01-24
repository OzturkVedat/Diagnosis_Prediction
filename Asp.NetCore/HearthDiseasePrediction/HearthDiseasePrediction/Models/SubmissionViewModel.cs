using System.ComponentModel.DataAnnotations;

namespace HearthDiseasePrediction.Models
{
    public class SubmissionViewModel
    {
        [Range(18, 100, ErrorMessage = "Enter a valid age between 18 and 100.")]
        public int Age { get;set; }
        public Boolean Gender {  get; set; }    // 0 for female, 1 for male
        public int ChestPainType { get; set; }  // nominal, 0 to 3

        [Range(71, 202, ErrorMessage = "Enter a value between 71 and 202.")]
        public int MaxHearthRate { get; set; }

        [Range(94, 200, ErrorMessage = "Enter a value between 94 and 200.")]
        public int RestingBloodPressure {  get; set; }

        [Range(126, 564, ErrorMessage = "Enter a value between 126 and 564.")]
        public int SerumCholesterol { get; set; }

        public Boolean FastingBloodSugar { get; set; }    //  0 for false, 1 for true (fbs >? 120 mg/dl)

        public int RestingElectrocardio { get; set; }   // nominal, 0 to 2

        public Boolean ExerciseAngina { get; set; }    //  0= no, 1= yes 

        [Range(0, 6.2, ErrorMessage = "Enter a value between 0 and 6.2")]
        public decimal OldPeak { get; set; }  // treadmill test for ST depression (ranged 0-6.2)

        public int SlopeST { get; set; }   // slope of the peak exercise ST segment ,nominal, 1 to 3

        [Range(0, 3, ErrorMessage = "Enter a value between 0 and 3")]
        public int MajorVessels { get; set; }   // slope of the peak exercise ST segment ,nominal, 1 to 3

    }
    public class PredictionResult
    {
        public int Prediction { get; set; }
    }
}
