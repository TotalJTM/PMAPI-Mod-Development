using Il2Cpp;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;
//using PMAPI.CustomMenus;
using PMAPI.CustomSubstances;
using PMAPI.Debugging;

namespace PMAPI
{
    internal class EntryPoint : MelonMod
    {
        PlayerHelper playerHelper = new PlayerHelper();

        public override void OnInitializeMelon()
        {
            CustomMaterialManager.Init();
            CustomSubstanceManager.Init();
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            MelonLogger.Msg("On scene initialized called");
            CustomLocalizer.Reload();

            playerHelper.Reload();
            // CustomMenuManager.Init();

            // dump initial scene data (start menu space) and resource data (substance names, material names, sounds)
            // ResourceXmlDumper.DumpAllToFile();
            // HierarchyXmlDumper.DumpSceneToFile();
        }

        public override void OnApplicationQuit()
        {
            // used to get scene data from a world you load into
            // HierarchyXmlDumper.DumpSceneToFile();
        }
    }
}