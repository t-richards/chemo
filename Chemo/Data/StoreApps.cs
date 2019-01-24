using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chemo.Data
{
    class StoreApps
    {
        protected static readonly IDictionary<string, bool> AppsToRemove = new Dictionary<string, bool>
        {
            { "46928bounde.EclipseManager", true },
            { "828B5831.HiddenCityMysteryofShadows", true },
            { "A278AB0D.DragonManiaLegends", true },
            { "ActiproSoftwareLLC.562882FEEB491", true },
            { "AdobeSystemsIncorporated.AdobePhotoshopExpress", true },
            { "D5EA27B7.Duolingo-LearnLanguagesforFree", true},
            { "Fitbit.FitbitCoach", true },
            { "king.com.BubbleWitch3Saga", true },
            { "king.com.CandyCrushSaga", true },
            { "king.com.CandyCrushSodaSaga", true },
            { "Microsoft.3DBuilder", true },
            { "Microsoft.BingFinance", true },
            { "Microsoft.BingFoodAndDrink", true },
            { "Microsoft.BingHealthAndFitness", true },
            { "Microsoft.BingNews", true },
            { "Microsoft.BingSports", true },
            { "Microsoft.BingTravel", true },
            { "Microsoft.BingWeather", true },
            { "Microsoft.DesktopAppInstaller", true },
            { "Microsoft.FreshPaint", true },
            { "Microsoft.GetHelp", true },
            { "Microsoft.Getstarted", true },
            { "Microsoft.Messaging", true },
            { "Microsoft.Microsoft3DViewer", true },
            { "Microsoft.MicrosoftOfficeHub", true },
            { "Microsoft.MicrosoftPowerBIForWindows", true },
            { "Microsoft.MicrosoftSolitaireCollection", true },
            { "Microsoft.MicrosoftStickyNotes", true },
            { "Microsoft.MinecraftUWP", true },
            { "Microsoft.MSPaint", true },
            { "Microsoft.NetworkSpeedTest", true },
            { "Microsoft.Office.OneNote", true },
            { "Microsoft.Office.Sway", true },
            { "Microsoft.OneConnect", true },
            { "Microsoft.People", true },
            { "Microsoft.Print3D", true },
            { "Microsoft.SkypeApp", true },
            { "Microsoft.Wallet", true },
            { "Microsoft.WindowsAlarms", true },
            { "Microsoft.WindowsCamera", true },
            { "microsoft.windowscommunicationsapps", true },
            { "Microsoft.WindowsFeedbackHub", true },
            { "Microsoft.WindowsMaps", true },
            { "Microsoft.WindowsSoundRecorder", true },
            { "Microsoft.Xbox.TCUI", true },
            { "Microsoft.XboxApp", true },
            { "Microsoft.XboxGameOverlay", true },
            { "Microsoft.XboxGamingOverlay", true },
            { "Microsoft.XboxIdentityProvider", true },
            { "Microsoft.XboxSpeechToTextOverlay", true },
            { "Microsoft.ZuneMusic", true },
            { "Microsoft.ZuneVideo", true },
            { "Nordcurrent.CookingFever", true },
            { "NORDCURRENT.COOKINGFEVER", true },
            { "PandoraMediaInc.29680B314EFC2", true },
            { "ThumbmunkeysLtd.PhototasticCollage", true }
        };

        public static bool shouldRemove(string packageName)
        {
            AppsToRemove.TryGetValue(packageName, out bool shouldRemove);
            return shouldRemove;
        }
    }
}
