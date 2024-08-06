using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NeonCollate;
using MelonLoader;

namespace NeonCollate.GameObjects
{
    internal class GoToJobArchive
    {
        private static GameObject _goToJobArchive;
        private static GameObject _mainMenu;

        public static void Show()
        {
            if (_goToJobArchive != null)
            {
                _goToJobArchive.SetActive(true);
            }
        }

        public static void Hide()
        {
            if (_goToJobArchive != null)
            {
                _goToJobArchive.SetActive(false);
            }
        }

        public static LocationData FindLocationHub()
        {
            LocationData[] allLocationData = Resources.FindObjectsOfTypeAll<LocationData>();
            foreach (LocationData locationData in allLocationData)
            {
                if (locationData.name == "Location_Portal")
                    return locationData;
            }
            return null;
        }

        public static void Click()
        {
            if (_mainMenu == null || _mainMenu.GetComponent<MainMenu>() == null)
                return;

            MainMenu _mainMenuC = _mainMenu.GetComponent<MainMenu>();
            Singleton<Audio>.Instance.StopMusic(2f);

            _mainMenuC.PauseGameNoStateChange(false);
            if (RM.time != null)
            {
                RM.time.SetTargetTimescale(1f, false);
            }

            LocationData HubLocation = FindLocationHub();
            Singleton<Game>.Instance.PlayLevel("HUB_HEAVEN", false, delegate ()
            {
                _mainMenuC.SetState(MainMenu.State.Level, true, true, false, false);
                _mainMenuC.EnterLocation(HubLocation, true, true);
                _mainMenuC.SelectCampaign("C_MAINQUEST", true);
                _mainMenuC.CurrentActiveMenuScreen = _mainMenuC._screenMission;
                _mainMenuC.CurrentActiveMenuScreen.TrySelectActiveElementByPriority(MenuScreen.SelectionPriority.FIRST_NAV, false);
            });
        }

        internal static void Initialize()
        {
            if (_goToJobArchive != null) return;

            GameObject backButton = GameObject.Find("Main Menu/Canvas/BackButtonHolderHolder/Back Button Holder/Button");
            _mainMenu = GameObject.Find("Main Menu");

            _goToJobArchive = GameObject.Instantiate(backButton, backButton.transform.parent);

            _goToJobArchive.transform.localPosition = new Vector3(0f, 140f);

            TextMeshProUGUI text = _goToJobArchive.GetComponentInChildren<TextMeshProUGUI>();
            text.SetText("Go to Job Archive");

            Button button = _goToJobArchive.GetComponent<Button>();
            button.onClick.AddListener(Click);



        }
    }
}
