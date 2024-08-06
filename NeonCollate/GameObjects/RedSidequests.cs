using MelonLoader;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

namespace NeonCollate.GameObjects
{
    internal class RedSidequests
    {
        private static GameObject _redSidequests;
        private static GameObject _mainMenu;

        public static void PushDown()
        {
            if (_redSidequests != null)
                _redSidequests.transform.SetAsLastSibling();
        }

        public static void Click()
        {
            _mainMenu.GetComponent<MainMenu>().SelectMission("M_SIDEQUESTS_RED", true, false);
        }

        internal static void Initialize()
        {
            if (_redSidequests != null)
                return;

            _mainMenu = GameObject.Find("Main Menu");
            GameObject missionButton = GameObject.Find("Main Menu/Canvas/Main Menu/Panel/Missions Panel/Scroll View/Viewport/Buttons Holder/Mission Button Holder");


            if (_mainMenu == null || missionButton == null)
            {
                MelonLogger.Msg("Houston, we have a problem");
                return;
            }

            _redSidequests = GameObject.Instantiate(missionButton, missionButton.transform.parent);
            _redSidequests.name = "Red Sidequests Mission Button";

            // Disable animator to prevent hiding
            _redSidequests.GetComponent<Animator>().enabled = false;

            Button _button = _redSidequests.GetComponentInChildren<Button>();

            ColorBlock _buttonColors = _button.colors;
            _buttonColors.normalColor = new Color(1f, 1f, 1f, 1f);
            _buttonColors.highlightedColor = new Color(0.5074f, 0.9782f, 1f, 1f);
            _buttonColors.pressedColor = new Color(0.0077f, 0.5221f, 0.4999f, 1f);
            _buttonColors.selectedColor = new Color(0.5074f, 0.9782f, 1f, 1f);
            _buttonColors.disabledColor = new Color(0.7843f, 0.7843f, 0.7843f, 0.502f);
            _button.colors = _buttonColors;

            _redSidequests.GetComponentInChildren<Image>().color = new Color(1f, 1f, 1f, 1f);

            TextMeshProUGUI text = _redSidequests.transform.Find("Button").GetComponentInChildren<TextMeshProUGUI>();
            text.SetText("Red Sidequests");
            text.color = new Color(0f, 0f, 0f, 1f);

            TextMeshProUGUI counterText = _redSidequests.transform.Find("Button").transform.Find("CounterText").GetComponentInChildren<TextMeshProUGUI>();
            counterText.SetText("R");
            counterText.color = new Color(0.502f, 0.502f, 0.502f, 1f);

            _button.onClick.AddListener(Click);

        }
    }
}
