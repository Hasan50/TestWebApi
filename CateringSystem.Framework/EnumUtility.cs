using System;
using System.ComponentModel;
using System.Linq;

namespace CateringSystem.Framework
{
    public class EnumUtility
    {
        public static string GetDescriptionFromEnumValue(Enum value)
        {
            DescriptionAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

       
        public enum PatientGender
        {
            [Description("Male")]
            Male,
            [Description("Female")]
            Female
        }
        public enum PatientInitialName
        {
            [Description("Mr.")]
            Mr=1,
            [Description("Advocate")]
            Advocate=2,
            [Description("Alhajj")]
            Alhajj=3,
            [Description("Dr.")]
            Dr=4,
            [Description("Engr.")]
            Engr=5,
            [Description("Master")]
            Master=6,
            [Description("Mawlana")]
            Mawlana=7,
            [Description("Md.")]
            Md=8,
            [Description("Miss")]
            Miss=9,
            [Description("Mrs.")]
            Mrs=10,
            [Description("Ms")]
            Ms=11,
            [Description("Mst.")]
            Mst=12,
            [Description("Muftie")]
            Mufti=13,
            [Description("Prof")]
            Prof=14
        }
        public enum PatientFindType
        {
            [Description("Id")]
            ID = 1,
            [Description("Phone no.")]
            PhoneNo = 2,
            [Description("Name")]
            Name = 3,
        }
        public enum DoctorPanelTheme
        {
            [Description("Blue Theme")]
            BlueTheme = 1,
            [Description("Green Theme")]
            GreenTheme = 2,
            [Description("Dark Green Theme")]
            DarkGreenTheme = 4,
            [Description("Light Sea Green Theme")]
            LightSeaGreenTheme = 5,

        }
    }
}
