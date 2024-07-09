using MelonLoader;
using Il2Cpp;
using PMAPI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.Json;

namespace HeliumRecipeMod
{
    public class HeliumRecipeMod : MelonMod
    {

        public override void OnInitializeMelon()
        {
            // Init PMAPI
            PMAPIModRegistry.InitPMAPI(this);
            // make recipe
            CubeMerge.compoundablePairs.Add(new Il2CppSystem.ValueTuple<Substance, Substance>(Substance.Rubber, Substance.Slime), new Il2CppSystem.ValueTuple<float, Substance, float>(1f, Substance.Helium, 2f));
        }
    }
}