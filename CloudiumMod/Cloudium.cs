using MelonLoader;
using Il2Cpp;
using PMAPI;
using UnityEngine;
using Il2CppInterop.Runtime.Injection;
using PMAPI.CustomSubstances;
using HarmonyLib;

namespace Cloudium
{
    public class Cloudium : MelonMod
    {
        Substance cloudium;

        public override void OnInitializeMelon()
        {
            // Init PMAPI
            PMAPIModRegistry.InitPMAPI(this);

            // Register in our behaviour in IL2CPP so the game knows about it
            ClassInjector.RegisterTypeInIl2Cpp<CloudiumBeh>(new RegisterTypeOptions
            {
                Interfaces = new[] { typeof(ICubeBehavior), typeof(ISavable) }
            });

            // Registering modded substances
            RegisterCloudium();


        }

        // 
        void RegisterCloudium()
        {
            Color cloudColor = Color.white;
            cloudColor.a = 0.6f;
            // Getting wood material and coloring it BLUE
            Material cmat = new(SubstanceManager.GetMaterial("Leaf"))
            {
                name = "Cloudium",
                color = cloudColor,
            };
            // Registering material
            CustomMaterialManager.RegisterMaterial(cmat);

            // Getting substance params that our substance is based on and modifying them
            var param = SubstanceManager.GetParameter(Substance.Leaf).MemberwiseClone().Cast<SubstanceParameters.Param>();

            // Should be unique
            param.displayNameKey = "SUB_CLOUD";
            param.material = cmat.name;
            param.strength = 75;
            param.stiffness = 0.5f;
            param.isFlammable = false;
            param.density = 0.02f;
            param.hardness = 10;
            param.flashPoint = 100;
            param.physicMaterial = "RawRubber";
            param.collisionSound = "papa1";
            param.defaultPitch = 3f;


            // Registering our substance as custom substance
            cloudium = CustomSubstanceManager.RegisterSubstance("cloudium", param, new CustomSubstanceParams
            {
                enName = "Cloudium",
                jpName = "雲",
                behInit = (cb) =>
                {
                    var beh = cb.gameObject.AddComponent<CloudiumBeh>();
                    return beh;
                }
            });
        }

        /*[HarmonyPatch(typeof(CubeGenerator), nameof(CubeGenerator.GenerateSkyTerrainObjects))]
        internal static class CubeGeneratorGenerateSkyTerrainObjectsPatch
        {
            private static void Postfix(ref Vector2Int chunkPos)
            {
                int randOccur = Mathf.RoundToInt(CubeGenerator.chunkRandom.Range(0f, 50 - 1));

                if (randOccur == 0)
                {
                    Substance cloudium = CustomSubstanceManager.GetSubstanceByEID("cloudium");

                    int yOffset = Mathf.RoundToInt(CubeGenerator.chunkRandom.Range(0f, 10 - 1));
                    Vector3 worldPos = CubeGenerator.ChunkToWorldPos(chunkPos);
                    Vector3 clusterPos = new Vector3(worldPos.x, worldPos.y + 55 + yOffset, worldPos.z);
                    int randSel = Mathf.RoundToInt(CubeGenerator.chunkRandom.Range(0f, 5 - 1));

                    float sizeScale = CubeGenerator.chunkRandom.Range(1.0f, 4f);
                    float bias = CubeGenerator.chunkRandom.Range(0.5f, 1.5f);

                    var anchorCube = CubeGenerator.GenerateCube(clusterPos, new Vector3((0.5f * bias) * sizeScale, 0.2f * sizeScale, (0.5f / bias) * sizeScale), cloudium);
                    //var anchorCube = CubeGenerator.GenerateCube(clusterPos, new Vector3(0.1f, 0.1f, 0.1f), cloudium);
                    anchorCube.GetComponent<CubeConnector>().anchor = CubeConnector.Anchor.Permanent;


                    for (int i = 0; i < randSel + 4; i++)
                    {
                        Vector3 offset = new Vector3();
                        offset.x = CubeGenerator.chunkRandom.Range(0f, 4 - 1);
                        offset.z = CubeGenerator.chunkRandom.Range(0f, 4 - 1);
                        offset.y = CubeGenerator.chunkRandom.Range(0f, 1 - 1);
                        sizeScale = CubeGenerator.chunkRandom.Range(0.5f, 2f);
                        bias = CubeGenerator.chunkRandom.Range(0.5f, 1.5f);

                        var newCube = CubeGenerator.GenerateCube(clusterPos+offset, new Vector3((1 * bias) * sizeScale, 0.2f * sizeScale, (1 / bias) * sizeScale), cloudium);
                        newCube.GetComponent<CubeConnector>().anchor = CubeConnector.Anchor.Temporary;
                        anchorCube.GetComponent<CubeConnector>().Connect(newCube.GetComponent<CubeConnector>());
                    }
                }
            }*/

        [HarmonyPatch(typeof(TerrainMeshGenerator), nameof(TerrainMeshGenerator.GenerateSkyTerrains))]
        internal static class TerrainMeshGeneratorGenerateSkyTerrainsPatch
        {
            private static void Postfix(ref Vector2Int chunkPos)
            {
                int randOccur = Mathf.RoundToInt(CubeGenerator.chunkRandom.Range(0f, 50 - 1));

                if (randOccur == 0)
                {
                    Substance cloudium = CustomSubstanceManager.GetSubstanceByEID("cloudium");

                    int yOffset = Mathf.RoundToInt(CubeGenerator.chunkRandom.Range(0f, 10 - 1));
                    Vector3 worldPos = CubeGenerator.ChunkToWorldPos(chunkPos);
                    Vector3 clusterPos = new Vector3(worldPos.x, worldPos.y + 55 + yOffset, worldPos.z);
                    int randSel = Mathf.RoundToInt(CubeGenerator.chunkRandom.Range(0f, 5 - 1));

                    float sizeScale = CubeGenerator.chunkRandom.Range(1.0f, 4f);
                    float bias = CubeGenerator.chunkRandom.Range(0.5f, 1.5f);

                    var anchorCube = CubeGenerator.GenerateCube(clusterPos, new Vector3((0.5f * bias) * sizeScale, 0.2f * sizeScale, (0.5f / bias) * sizeScale), cloudium);
                    //var anchorCube = CubeGenerator.GenerateCube(clusterPos, new Vector3(0.1f, 0.1f, 0.1f), cloudium);
                    anchorCube.GetComponent<CubeConnector>().anchor = CubeConnector.Anchor.Permanent;


                    for (int i = 0; i < randSel + 4; i++)
                    {
                        Vector3 offset = new Vector3();
                        offset.x = CubeGenerator.chunkRandom.Range(0f, 4 - 1);
                        offset.z = CubeGenerator.chunkRandom.Range(0f, 4 - 1);
                        offset.y = CubeGenerator.chunkRandom.Range(0f, 1 - 1);
                        sizeScale = CubeGenerator.chunkRandom.Range(0.5f, 2f);
                        bias = CubeGenerator.chunkRandom.Range(0.5f, 1.5f);

                        var newCube = CubeGenerator.GenerateCube(clusterPos + offset, new Vector3((1 * bias) * sizeScale, 0.2f * sizeScale, (1 / bias) * sizeScale), cloudium);
                        newCube.GetComponent<CubeConnector>().anchor = CubeConnector.Anchor.Temporary;
                        anchorCube.GetComponent<CubeConnector>().Connect(newCube.GetComponent<CubeConnector>());
                    }
                }
            }
        }

        public override void OnUpdate()
        {
            /*if (Input.GetKeyDown(KeyCode.C))
            {
                // Getting player position
                var mv = GameObject.Find("XR Origin").GetComponent<PlayerMovement>();

                // Generating the cube
                CubeGenerator.GenerateCube(mv.cameraTransform.position + new Vector3(0f, 0.5f, 2f), new Vector3(1,0.3f,1), cloudium);
            }*/
        }
    }

    public class CloudiumBeh : MonoBehaviour
    {
        public CloudiumBeh(IntPtr ptr) : base(ptr)
        {
        }

        CubeBase cubeBase;

        void OnInitialize()
        {
            // Get the cube base
            cubeBase = GetComponent<CubeBase>();
        }

        void Update()
        {
            // destroy the cube if it gets too hot
            if (cubeBase.heat.GetCelsiusTemperature() > 600.0)
            {
                Destroy(cubeBase);
            }
        }
    }
}