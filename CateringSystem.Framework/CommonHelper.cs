using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CateringSystem.Framework
{
    public class AgeOb
    {
        public int Years { get; set; }
        public int Month { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
    }
    public static class CommonHelper
    {
        public static AgeOb CalculateYourAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;
            int Minutes = Now.Subtract(PastYearDate).Minutes;
            int Seconds = Now.Subtract(PastYearDate).Seconds;
            AgeOb age = new AgeOb
            {
                Years=Years,
                Month=Months,
                Days=Days,
                Hours=Hours,
                Minutes=Minutes,
                Seconds=Seconds
            };
            return age;
            //return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s) {3} Hour(s) {4} Second(s)",
            //Years, Months, Days, Hours, Seconds);
        }
        public static bool IsValidFile(string ext)
        {
            string[] validFileFormate = new string[] { "jpg", "png" };
            for (int i = 0; i < validFileFormate.Length; i++)
            {
                string vF = "." + validFileFormate[i];
                if (vF == ext)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
