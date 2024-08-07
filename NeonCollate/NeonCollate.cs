using MelonLoader;

namespace NeonCollate
{
    public class NeonCollate : MelonMod
    {
        internal static new HarmonyLib.Harmony Harmony { get; private set; }

        public override void OnLateInitializeMelon()
        {
            base.OnLateInitializeMelon();
            Harmony = HarmonyInstance;
            Harmony.PatchAll();
        }
        public override void OnApplicationQuit()
        {
            Harmony.UnpatchAll();
            base.OnApplicationQuit();
        }
        public override void OnSceneWasLoaded(int buildindex, string sceneName)
        {
            if (sceneName.Equals("Heaven_Environment") || sceneName.Equals("HUB_HEAVEN"))
            {
                Sidequests.Initialize();
                Returns.Initialize();
            }
        }
    }
}