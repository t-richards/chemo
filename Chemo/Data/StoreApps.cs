using System.Collections.Generic;

namespace Chemo.Data
{
    class StoreApps
    {
        protected static readonly HashSet<string> AppsToRemove = new HashSet<string>
        {
            "46928bounde.EclipseManager",
            "828B5831.HiddenCityMysteryofShadows",
            "A278AB0D.DragonManiaLegends",
            "ActiproSoftwareLLC.562882FEEB491",
            "AdobeSystemsIncorporated.AdobePhotoshopExpress",
            "D5EA27B7.Duolingo-LearnLanguagesforFree",
            "Fitbit.FitbitCoach",
            "king.com.BubbleWitch3Saga",
            "king.com.CandyCrushSaga",
            "king.com.CandyCrushSodaSaga",
            "Microsoft.3DBuilder",
            "Microsoft.BingFinance",
            "Microsoft.BingFoodAndDrink",
            "Microsoft.BingHealthAndFitness",
            "Microsoft.BingNews",
            "Microsoft.BingSports",
            "Microsoft.BingTravel",
            "Microsoft.BingWeather",
            "Microsoft.DesktopAppInstaller",
            "Microsoft.FreshPaint",
            "Microsoft.GetHelp",
            "Microsoft.Getstarted",
            "Microsoft.Messaging",
            "Microsoft.Microsoft3DViewer",
            "Microsoft.MicrosoftOfficeHub",
            "Microsoft.MicrosoftPowerBIForWindows",
            "Microsoft.MicrosoftSolitaireCollection",
            "Microsoft.MicrosoftStickyNotes",
            "Microsoft.MinecraftUWP",
            "Microsoft.MSPaint",
            "Microsoft.NetworkSpeedTest",
            "Microsoft.Office.OneNote",
            "Microsoft.Office.Sway",
            "Microsoft.OneConnect",
            "Microsoft.People",
            "Microsoft.Print3D",
            "Microsoft.SkypeApp",
            "Microsoft.Wallet",
            "Microsoft.WindowsAlarms",
            "Microsoft.WindowsCamera",
            "microsoft.windowscommunicationsapps",
            "Microsoft.WindowsFeedbackHub",
            "Microsoft.WindowsMaps",
            "Microsoft.WindowsSoundRecorder",
            "Microsoft.Xbox.TCUI",
            "Microsoft.XboxApp",
            "Microsoft.XboxGameOverlay",
            "Microsoft.XboxGamingOverlay",
            "Microsoft.XboxIdentityProvider",
            "Microsoft.XboxSpeechToTextOverlay",
            "Microsoft.ZuneMusic",
            "Microsoft.ZuneVideo",
            "Nordcurrent.CookingFever",
            "NORDCURRENT.COOKINGFEVER",
            "PandoraMediaInc.29680B314EFC2",
            "ThumbmunkeysLtd.PhototasticCollage"
        };

        public static bool ShouldRemove(string packageName)
        {
            return AppsToRemove.Contains(packageName);
        }
    }
}
