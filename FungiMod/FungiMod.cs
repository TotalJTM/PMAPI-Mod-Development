using MelonLoader;
using Il2Cpp;
using PMAPI;
using UnityEngine;
using Il2CppInterop.Runtime.Injection;
using PMAPI.CustomSubstances;
using PMAPI.OreGen;
using UnityEngine.SceneManagement;
using System.Text.Json;
using System.Drawing;
using Il2CppSystem;

namespace FungiMod
{
    public class FungiMod : MelonMod
    {
        // define the multiple types of cap and single type of wood
        Substance fungiWood;
        Substance fungiCapBrown;
        Substance fungiCapRed;

        UnityEngine.Color brownFungiColor = new UnityEngine.Color(140,128,118,1);
        UnityEngine.Color redFungiColor = new UnityEngine.Color(140, 128, 118, 1);

        public override void OnInitializeMelon()
        {
            // Init PMAPI
            PMAPIModRegistry.InitPMAPI(this);

            // Register in our behaviour in IL2CPP so the game knows about it
            ClassInjector.RegisterTypeInIl2Cpp<FungiBeh>(new RegisterTypeOptions
            {
                Interfaces = new[] { typeof(ICubeBehavior), typeof(ISavable) }
            });

            // Registering modded substances
            RegisterFungiWood();
            RegisterFungiCapBrown();
            RegisterFungiCapRed();

            // Registering our substances in ore generation for now, will make a PlantManager at some point
            CustomOreManager.RegisterCustomOre(fungiWood, new CustomOreManager.CustomOreParams
            {
                chance = 0.05f,
                substanceOverride = Substance.Wood,
                maxSize = 0.1f,
                minSize = 0.1f,
                alpha = 1f
            });
        }

        // Gets called just when the world was loaded
        public void OnWorldWasLoaded()
        {
            // Outputting mod data. The question mark means that nothing will be outputted if mod data doesn't exist (data == null)
            // MelonLogger.Msg("Mod data is {0}", ExtDataManager.GetData<OurData>()?.Test);
        }

        public void OnBuild()
        {

        }

        // Make a new substance for fungi wood (will be used as base for fungi plants), register fungi behavior with this cube
        void RegisterFungiWood()
        {
            // Getting wood material and coloring it BLUE
            Material cmat = new(SubstanceManager.GetMaterial("Wood"))
            {
                name = "FungiWood",
                color = brownFungiColor
            };
            // Registering material
            CustomMaterialManager.RegisterMaterial(cmat);

            // Getting substance params that our substance is based on and modifying them
            var param = SubstanceManager.GetParameter(Substance.Leaf).MemberwiseClone().Cast<SubstanceParameters.Param>();

            // Should be unique
            param.displayNameKey = "SUB_FUNGWOOD";
            param.material = cmat.name;

            // Registering our substance as custom substance
            fungiWood = CustomSubstanceManager.RegisterSubstance("FungiWood", param, new CustomSubstanceParams
            {
                enName = "FungiWood",
                jpName = "FungiWood",
                behInit = (cb) =>
                {
                    var beh = cb.gameObject.AddComponent<FungiBeh>();
                    return beh;
                }
            });

            // CubeMerge.compoundablePairs.Add(new Il2CppSystem.ValueTuple<Substance, Substance>(Substance.Slime, Substance.Rubber), new Il2CppSystem.ValueTuple<float, Substance, float>(1f, blud, 1f));
        }

        // Make a new substance for one of the fungi caps (brown variant)
        void RegisterFungiCapBrown()
        {
            Material cmat = new(SubstanceManager.GetMaterial("Leaf"))
            {
                name = "FungiCapBrown",
                color = brownFungiColor
            };
            CustomMaterialManager.RegisterMaterial(cmat);

            var param = SubstanceManager.GetParameter(Substance.Leaf).MemberwiseClone().Cast<SubstanceParameters.Param>();
            param.displayNameKey = "SUB_FUNGCAP_B";
            param.material = cmat.name;
            param.isEdible = true;
            fungiCapBrown = CustomSubstanceManager.RegisterSubstance("FungiCapBrown", param, new CustomSubstanceParams
            {
                enName = "FungiCapBrown",
                jpName = "FungiCapBrown"
            });
        }

        // Make a new substance for one of the fungi caps (red variant)
        void RegisterFungiCapRed()
        {
            Material cmat = new(SubstanceManager.GetMaterial("Leaf"))
            {
                name = "FungiCapRed",
                color = redFungiColor
            };
            CustomMaterialManager.RegisterMaterial(cmat);

            var param = SubstanceManager.GetParameter(Substance.Leaf).MemberwiseClone().Cast<SubstanceParameters.Param>();
            param.displayNameKey = "SUB_FUNGCAP_R";
            param.material = cmat.name;
            param.isEdible = true;
            fungiCapRed = CustomSubstanceManager.RegisterSubstance("FungiCapRed", param, new CustomSubstanceParams
            {
                enName = "FungiCapRed",
                jpName = "FungiCapRed"
            });
        }

        public override void OnUpdate()
        {
            // Spawning blud above our head
            if (Input.GetKeyDown(KeyCode.S))
            {
                // Getting player position
                var mv = GameObject.Find("XR Origin").GetComponent<PlayerMovement>();

                // Generating the cube
                // CubeGenerator.GenerateCube(mv.cameraTransform.position + new Vector3(0f, 10f, 0f), Vector3(0f, 10f, 0f), fungi);
                MakeSmallFungi(fungiCapBrown, mv.cameraTransform.position + new Vector3(0f, 10f, 0f), 0);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                // Getting player position
                var mv = GameObject.Find("XR Origin").GetComponent<PlayerMovement>();

                // Generating the cube
                // CubeGenerator.GenerateCube(mv.cameraTransform.position + new Vector3(0f, 10f, 0f), Vector3(0f, 10f, 0f), fungi);
                MakeGiantFungi(fungiCapBrown, mv.cameraTransform.position + new Vector3(0f, 10f, 0f), 0);
            }
        }

        Vector3[] smallWoodSizes = { new Vector3(0.1f, 0.2f, 0.1f), new Vector3(0.13f, 0.28f, 0.13f), new Vector3(0.2f, 0.35f, 0.2f) };
        Vector3[] smallCapSizes = { new Vector3(0.2f, 0.06f, 0.2f), new Vector3(0.25f, 0.1f, 0.25f), new Vector3(0.3f, 0.15f, 0.3f) };

        Vector3[] giantWoodSizes = { new Vector3(0.3f, 0.8f, 0.3f), new Vector3(0.5f, 1.3f, 5f), new Vector3(0.7f, 2f, 0.7f) };
        Vector3[] giantCapSizes = { new Vector3(0.6f, 0.2f, 0.6f), new Vector3(0.9f, 0.3f, 9f), new Vector3(1.4f, 0.4f, 1.4f) };

        public void MakeSmallFungi(Substance capMaterial, Vector3 centerPos, int growthSize)
        {
            var margin = smallCapSizes[2].x;
            var pos = centerPos + new Vector3((CubeGenerator.chunkRandom.Value - 0.5f) * margin, 0, (CubeGenerator.chunkRandom.Value - 0.5f) * margin);

            var wood = CubeGenerator.GenerateCube(new Vector3(pos.x, pos.y + (smallWoodSizes[0].y / 2), pos.z), giantWoodSizes[growthSize], fungiWood, CubeAppearance.SectionState.Top | CubeAppearance.SectionState.Bottom);
            var cap = CubeGenerator.GenerateCube(pos + new Vector3(0, smallWoodSizes[0].y + smallCapSizes[0].y / 2, 0), smallCapSizes[growthSize], capMaterial);

            cap.GetComponent<CubeConnector>().anchor = CubeConnector.Anchor.Temporary;
            wood.GetComponent<CubeConnector>().anchor = CubeConnector.Anchor.Temporary;
            wood.GetComponent<CubeConnector>().Connect(cap.GetComponent<CubeConnector>());
        }

        public void MakeGiantFungi(Substance capMaterial, Vector3 centerPos, int growthSize)
        {
            var margin = giantCapSizes[2].x;
            var pos = centerPos + new Vector3((CubeGenerator.chunkRandom.Value - 0.5f) * margin, 0, (CubeGenerator.chunkRandom.Value - 0.5f) * margin);

            var wood = CubeGenerator.GenerateCube(new Vector3(pos.x, pos.y + (giantWoodSizes[0].y / 2), pos.z), giantWoodSizes[growthSize], fungiWood, CubeAppearance.SectionState.Top | CubeAppearance.SectionState.Bottom);
            var cap = CubeGenerator.GenerateCube(pos + new Vector3(0, giantWoodSizes[0].y + giantCapSizes[0].y / 2, 0), giantCapSizes[growthSize], capMaterial);

            cap.GetComponent<CubeConnector>().anchor = CubeConnector.Anchor.Temporary;
            wood.GetComponent<CubeConnector>().anchor = CubeConnector.Anchor.Temporary;
            wood.GetComponent<CubeConnector>().Connect(cap.GetComponent<CubeConnector>());
        }

        public void GenerateFungi(Vector3 centerPos)
        {
            System.Random rndGen = new System.Random();
            int rndNum = rndGen.Next(1,151);

            if (rndNum == 150)
            {
                MakeGiantFungi(fungiCapBrown, centerPos, 0);
            }
            else if (rndNum == 1)
            {
                MakeGiantFungi(fungiCapRed, centerPos, 0);
            }
            else if (rndNum <= 75)
            {
                MakeSmallFungi(fungiCapRed, centerPos, 0);
            }
            else // rndNum > 75 and any other case
            {
                MakeSmallFungi(fungiCapBrown, centerPos, 0);
            }
        }
    }

    public class FungiBeh : MonoBehaviour
    {
        public FungiBeh(System.IntPtr ptr) : base(ptr)
        {
            // Requesting load of cube save data
            CustomSaveManager.RequestLoad(this);
        }

        CubeConnector cubeConnector;

        // create variable to hold data (should be overridden when loading occurs)
        Data data = new(new Vector3(0,0,0));

        void OnInitialize()
        {
            // Get the cube base
            cubeConnector = GetComponent<CubeConnector>();
            //Vector3 currPos = cubeBase.transform.position;

            // Make the Fungi cap
            //GenerateFungi(cubeBase.gameObject, currPos);
            
        }

        // When cube is going to save it's data return the data that we want to save
        public string Save()
        {
            // Serialize into JSON
            return JsonSerializer.Serialize(data);
        }

        // When cube is loaded set our variable to loaded data
        public void Load(string json)
        {
            // From JSON to Data
            data = JsonSerializer.Deserialize<Data>(json);
        }

            public class Data
            {
                public Vector3 CenterPos { get; set; }
                public int GrowthState { get; set; }
                public int LastGrowthAccum { get; set; }

            public Data(Vector3 centerPos)
            {
                    CenterPos = centerPos;
                    GrowthState = 0;
                    LastGrowthAccum = 0;
            }
        }
    }
}