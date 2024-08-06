using MelonLoader;
using NeonCollate.GameObjects;
using UnityEngine.SceneManagement;
using UnityEngine;
using HarmonyLib;
using System.Security.Cryptography;

namespace NeonCollate
{
    public class NeonCollate : MelonMod
    {
        internal static new HarmonyLib.Harmony Harmony { get; private set; }
        public static void PushButtonsDown()
        {
            RedSidequests.PushDown();
            VioletSidequests.PushDown();
            YellowSidequests.PushDown();
        }

        public override void OnLateInitializeMelon()
        {
            base.OnLateInitializeMelon();

            Harmony = HarmonyInstance;
            Harmony.PatchAll();
        }
        public override void OnSceneWasLoaded(int buildindex, string sceneName)
        {
            if (sceneName.Equals("Heaven_Environment") || sceneName.Equals("HUB_HEAVEN"))
            {
                GoToJobArchive.Initialize();
                GoToJobArchive.Hide();

                RedSidequests.Initialize();
                VioletSidequests.Initialize();
                YellowSidequests.Initialize();
                return;
            }
        }
        public override void OnApplicationQuit()
        {
            Harmony.UnpatchAll();
            base.OnApplicationQuit();
        }

        [HarmonyPatch(typeof(MainMenu), "SetState")]
        public class MainMenuSetStatePatch
        {
            [HarmonyPostfix]
            static void Postfix()
            {
                if (!GameObject.Find("Main Menu") || !GameObject.Find("Main Menu").GetComponent<MainMenu>())
                    return;

                LocationData currentLocation = GameObject.Find("Main Menu").GetComponent<MainMenu>().GetCurrentLocation();

                if (currentLocation && currentLocation.name == "Location_WhitesRoom")
                    GoToJobArchive.Show();
                else
                    GoToJobArchive.Hide();
            }
        }

        [HarmonyPatch(typeof(MainMenu), "SelectCampaign")]
        public class MainMenuSelectCampaignPatch
        {
            [HarmonyPostfix]
            static void Postfix()
            {
                PushButtonsDown();
            }
        }

    }
}
