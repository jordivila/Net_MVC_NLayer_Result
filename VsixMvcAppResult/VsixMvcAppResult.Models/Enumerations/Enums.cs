using System;
using System.Runtime.Serialization;

namespace VsixMvcAppResult.Models.Enumerations
{

    [Serializable]
    [Flags]
    public enum SiteRoles
    {
        [EnumMember(Value = "Administrator")]
        Administrator = 1,

        [EnumMember(Value = "Guest")]
        Guest = 2
    }
    public enum MediaType
    {
        javascript,
        stylesheet,
        none
    }
    public enum ControllerResourceType
    {
        Javascript,
        Stylesheet
    }
    public enum Gender
    {
        Male,
        Female,
        Other
    }
    public enum Position
    {
        Horizontal,
        Vertical,
    }
    public enum ThemesAvailable
    {
        [EnumMember(Value="Base")]
        Base,

        [EnumMember(Value = "Black_Tie")]
        Black_Tie,

        [EnumMember(Value = "Blitzer")]
        Blitzer,

        [EnumMember(Value = "Cupertino")]
        Cupertino,

        [EnumMember(Value = "Dark_Hive")]
        Dark_Hive,

        [EnumMember(Value = "Dot_Luv")]
        Dot_Luv,

        [EnumMember(Value = "Eggplant")]
        Eggplant,

        [EnumMember(Value = "Excite_Bike")]
        Excite_Bike,

        [EnumMember(Value = "Flick")]
        Flick,

        [EnumMember(Value = "Hot_Sneaks")]
        Hot_Sneaks,

        [EnumMember(Value = "Humanity")]
        Humanity,

        [EnumMember(Value = "Le_Frog")]
        Le_Frog,

        [EnumMember(Value = "Mint_Choc")]
        Mint_Choc,

        [EnumMember(Value = "Overcast")]
        Overcast,

        [EnumMember(Value = "Pepper_Grinder")]
        Pepper_Grinder,

        [EnumMember(Value = "Redmond")]
        Redmond,

        [EnumMember(Value = "Smoothness")]
        Smoothness,

        [EnumMember(Value = "South_Street")]
        South_Street,

        [EnumMember(Value = "Start")]
        Start,

        [EnumMember(Value = "Sunny")]
        Sunny,

        [EnumMember(Value = "Swanky_Purse")]
        Swanky_Purse,

        [EnumMember(Value = "Trontastic")]
        Trontastic,

        [EnumMember(Value = "UI_Darkness")]
        UI_Darkness,

        [EnumMember(Value = "UI_Lightness")]
        UI_Lightness,

        [EnumMember(Value = "Vader")]
        Vader
    }

    //public static class LoggerCategories
    //{
    //    public static string WCFGeneral = "WCFGeneral";
    //    public static string WCFBeginRequest = "WCFBeginRequest";
    //    public static string UIGeneral = "UIGeneral";
    //    public static string UIBeginRequest = "UIBeginRequest";
    //    public static string UIServerSideUnhandledException = "UIServerSideUnhandledException";
    //    public static string UIClientSideJavascriptError = "UIClientSideJavascriptError";
    //}


    public enum LoggerCategories
    {
        WCFGeneral, //, = "WCFGeneral";
        WCFBeginRequest , //= "WCFBeginRequest";
        UIGeneral , //= "UIGeneral";
        UIBeginRequest , //= "UIBeginRequest";
        UIServerSideUnhandledException , //= "UIServerSideUnhandledException";
        UIClientSideJavascriptError, //= "UIClientSideJavascriptError";
    }

    public enum LoggerSeverities
    { 
        Information = 1,
        Error = 2
    }


    public enum PageSizesAvailable : int
    {
        RowsPerPage10 = 10,
        RowsPerPage20 = 20,
        RowsPerPage30 = 30,
        RowsPerPage40 = 40,
        RowsPerPage50 = 50,
    }
    public enum DataResultMessageType : int
    {
        Success = 0,
        Warning = 1,
        Error = 2,
        Confirm = 3
    }
    public enum HostingPlatform
    {
        Custom,
        Azure
    }

}