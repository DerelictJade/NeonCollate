using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NeonCollate
{
    internal class Returns
    {
        private static GameObject _mainMenu;
        private static bool _activated = false;

        public static void Initialize()
        {
            if (_activated)
                return;

            _mainMenu = GameObject.Find("Main Menu");
            GameObject _pauseReturn = GameObject.Find("Main Menu/Canvas/Ingame Menu/Menu Holder/Pause Menu/Pause Menu Holder/Pause Buttons/Sidequest Shop Button Holder");
            GameObject _levelCompleteReturn = GameObject.Find("Main Menu/Canvas/Ingame Menu/Menu Holder/Results Panel/Results Buttons/Button Sidequest Shop");

            if (!_mainMenu || !_pauseReturn || !_levelCompleteReturn)
                return;

            _pauseReturn.transform.Find("Button").gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText("Return to Job Archive");
            _pauseReturn.transform.Find("Button").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            _pauseReturn.transform.Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(Click);

            _levelCompleteReturn.transform.Find("Button").gameObject.GetComponentInChildren<TextMeshProUGUI>().SetText("Return to Job Archive");
            _levelCompleteReturn.transform.Find("Button").gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
            _levelCompleteReturn.transform.Find("Button").gameObject.GetComponent<Button>().onClick.AddListener(Click);

            _activated = true;
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
                _mainMenuC.GoToLastLevelSelected();
                _mainMenuC.CurrentActiveMenuScreen = _mainMenuC._screenLevel;
                _mainMenuC.CurrentActiveMenuScreen.TrySelectActiveElementByPriority(MenuScreen.SelectionPriority.FIRST_NAV, false);
            });
        }
    }


}
