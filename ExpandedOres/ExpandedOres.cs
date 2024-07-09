using MelonLoader;
using Il2Cpp;
using PMAPI;
using UnityEngine;
using Il2CppInterop.Runtime.Injection;
using PMAPI.CustomSubstances;
using PMAPI.OreGen;

namespace ExpandedOres
{
    public class ExpandedOres : MelonMod
    {
        // conductive and resistive
        Substance copperOre;
        Substance copper;

        // lightweight malleable metal
        Substance bauxiteOre;
        Substance aluminum;

        // semiconducter
        Substance sphaleriteOre;
        Substance zinc;

        // waterproof iron
        Substance galvanizedIron;

        // better fuel
        Substance coal;

        public override void OnInitializeMelon()
        {
            // Init PMAPI
            PMAPIModRegistry.InitPMAPI(this);

            // Register in our behaviour in IL2CPP so the game knows about it
            /*ClassInjector.RegisterTypeInIl2Cpp<CopperBeh>(new RegisterTypeOptions
            {
                Interfaces = new[] { typeof(ICubeBehavior) }
            });

            ClassInjector.RegisterTypeInIl2Cpp<ZincBeh>(new RegisterTypeOptions
            {
                Interfaces = new[] { typeof(ICubeBehavior) }
            });*/

            ClassInjector.RegisterTypeInIl2Cpp<OreBeh>(new RegisterTypeOptions
            {
                Interfaces = new[] { typeof(ICubeBehavior) }
            });

            /*ClassInjector.RegisterTypeInIl2Cpp<SemiconductorBehavior>(new RegisterTypeOptions
            {
                Interfaces = new[] { typeof(ICubeBehavior) }
            });*/

            ClassInjector.RegisterTypeInIl2Cpp<NoBeh>(new RegisterTypeOptions
            {
                Interfaces = new[] { typeof(ICubeBehavior) }
            });

            // Registering modded substances
            RegisterCopperOre();
            RegisterCopper();
            RegisterBauxiteOre();
            RegisterAluminum();
            RegisterSphalerite();
            RegisterZinc();
            RegisterGalvanizedIron();
            RegisterCoal();

            // add recipe to make galvanized iron from zinc + iron combo
            CubeMerge.compoundablePairs.Add(new Il2CppSystem.ValueTuple<Substance, Substance>(zinc, Substance.Iron), new Il2CppSystem.ValueTuple<float, Substance, float>(0.25f, galvanizedIron, 1f));

            // Registering our substances in ore generation
            CustomOreManager.RegisterCustomOre(copperOre, new CustomOreManager.CustomOreParams
            {
                chance = 0.11f,
                substanceOverride = 0,
                maxSize = 0.4f,
                minSize = 0.1f,
                alpha = 1f
            });

            CustomOreManager.RegisterCustomOre(bauxiteOre, new CustomOreManager.CustomOreParams
            {
                chance = 0.15f,
                substanceOverride = 0,
                maxSize = 0.6f,
                minSize = 0.2f,
                alpha = 1f
            });

            CustomOreManager.RegisterCustomOre(sphaleriteOre, new CustomOreManager.CustomOreParams
            {
                chance = 0.09f,
                substanceOverride = 0,
                maxSize = 0.2f,
                minSize = 0.05f,
                alpha = 1f
            });

            CustomOreManager.RegisterCustomOre(coal, new CustomOreManager.CustomOreParams
            {
                chance = 0.15f,
                substanceOverride = 0,
                maxSize = 0.6f,
                minSize = 0.15f,
                alpha = 1f
            });


        }


        // 
        void RegisterCopperOre()
        {
            // Getting wood material and coloring it BLUE
            Material cmat = new(SubstanceManager.GetMaterial("Hematite"))
            {
                name = "CopperOre",
                color = new Color(0.486f, 0.824f, 0.725f),
            };
            // Registering material
            CustomMaterialManager.RegisterMaterial(cmat);

            // Getting substance params that our substance is based on and modifying them
            var param = SubstanceManager.GetParameter(Substance.Hematite).MemberwiseClone().Cast<SubstanceParameters.Param>();

            // Should be unique
            param.displayNameKey = "SUB_COPP_ORE";
            param.material = cmat.name;
            param.density = 8.5f;
            param.strength = 175;
            param.stiffness = 30;
            param.hardness = 4.0f;


            // Registering our substance as custom substance
            copperOre = CustomSubstanceManager.RegisterSubstance("copperOre", param, new CustomSubstanceParams
            {
                enName = "Copper Ore",
                jpName = "Copper Ore",
                behInit = (cb) =>
                {
                    // Adding test behavior
                    var beh = cb.gameObject.AddComponent<OreBeh>();
                    beh.refinedSubstance = copper;
                    return beh;
                }
            });
        }

        // 
        void RegisterCopper()
        {
            // Getting wood material and coloring it BLUE
            Material cmat = new(SubstanceManager.GetMaterial("AncientAlloy"))
            {
                name = "Copper",
                color = new Color(0.773f, 0.416f, 0.224f),
            };
            // Registering material
            CustomMaterialManager.RegisterMaterial(cmat);

            // Getting substance params that our substance is based on and modifying them
            var param = SubstanceManager.GetParameter(Substance.AncientAlloy).MemberwiseClone().Cast<SubstanceParameters.Param>();

            // Should be unique
            param.displayNameKey = "SUB_COPP";
            param.material = cmat.name;
            param.density = 8.5f;
            param.strength = 150;
            param.stiffness = 15;
            param.hardness = 4.5f;
            param.thermalConductivity = 750;


            // Registering our substance as custom substance
            copper = CustomSubstanceManager.RegisterSubstance("copper", param, new CustomSubstanceParams
            {
                enName = "Copper",
                jpName = "Copper",
                /*behInit = (cb) =>
                {
                    // Adding test behavior
                    var beh = cb.gameObject.AddComponent<CopperBeh>();
                    return beh;
                }*/
            });
        }

        //
        void RegisterBauxiteOre()
        {
            // Getting wood material and coloring it BLUE
            Material cmat = new(SubstanceManager.GetMaterial("Hematite"))
            {
                name = "Bauxite",
                //color = new Color(0.741f, 0.408f, 0.345f),
                color = new Color(1.0f, 0.714f, 0.653f),
            };
            // Registering material
            CustomMaterialManager.RegisterMaterial(cmat);

            // Getting substance params that our substance is based on and modifying them
            var param = SubstanceManager.GetParameter(Substance.Hematite).MemberwiseClone().Cast<SubstanceParameters.Param>();

            // Should be unique
            param.displayNameKey = "SUB_BAUX";
            param.material = cmat.name;
            param.density = 1.5f;
            param.strength = 200;
            param.stiffness = 50;
            param.hardness = 5.5f;


            // Registering our substance as custom substance
            bauxiteOre = CustomSubstanceManager.RegisterSubstance("bauxiteOre", param, new CustomSubstanceParams
            {
                enName = "Bauxite",
                jpName = "Bauxite",
                behInit = (cb) =>
                {
                    // Adding test behavior
                    var beh = cb.gameObject.AddComponent<OreBeh>();
                    beh.refinedSubstance = aluminum;
                    return beh;
                }
            });
        }

        //
        void RegisterAluminum()
        {
            // Getting wood material and coloring it BLUE
            Material cmat = new(SubstanceManager.GetMaterial("Iron"))
            {
                name = "Aluminum",
                color = new Color(0.875f, 0.875f, 0.882f),
            };
            // Registering material
            CustomMaterialManager.RegisterMaterial(cmat);

            // Getting substance params that our substance is based on and modifying them
            var param = SubstanceManager.GetParameter(Substance.Iron).MemberwiseClone().Cast<SubstanceParameters.Param>();

            // Should be unique
            param.displayNameKey = "SUB_ALUM";
            param.material = cmat.name;
            param.density = 0.3f;
            param.strength = 300;
            param.stiffness = 75;
            param.hardness = 5.0f;

            // Registering our substance as custom substance
            aluminum = CustomSubstanceManager.RegisterSubstance("aluminum", param, new CustomSubstanceParams
            {
                enName = "Aluminum",
                jpName = "Aluminum",
            });
        }

        //
        void RegisterSphalerite()
        {
            // Getting wood material and coloring it BLUE
            Material cmat = new(SubstanceManager.GetMaterial("Hematite"))
            {
                name = "Sphalerite",
                //color = new Color(0.8588f, 0.7529f, 0.4666f),
                color = new Color(0.137f, 0.11f, 0.075f),
            };
            // Registering material
            CustomMaterialManager.RegisterMaterial(cmat);

            // Getting substance params that our substance is based on and modifying them
            var param = SubstanceManager.GetParameter(Substance.Hematite).MemberwiseClone().Cast<SubstanceParameters.Param>();

            // Should be unique
            param.displayNameKey = "SUB_SPHAL";
            param.material = cmat.name;
            param.density = 7.0f;
            param.strength = 100;
            param.stiffness = 50;
            param.hardness = 5.5f;

            // Registering our substance as custom substance
            sphaleriteOre = CustomSubstanceManager.RegisterSubstance("sphaleriteOre", param, new CustomSubstanceParams
            {
                enName = "Sphalerite",
                jpName = "Sphalerite",
                behInit = (cb) =>
                {

                    // Adding test behavior
                    var beh = cb.gameObject.AddComponent<OreBeh>();
                    beh.refinedSubstance = zinc;
                    return beh;
                }
            });
        }

        //
        void RegisterZinc()
        {
            // Getting wood material and coloring it BLUE
            Material cmat = new(SubstanceManager.GetMaterial("AncientAlloy"))
            {
                name = "Zinc",
                color = new Color(0.7294f, 0.7686f, 0.7843f),
            };
            // Registering material
            CustomMaterialManager.RegisterMaterial(cmat);

            // Getting substance params that our substance is based on and modifying them
            var param = SubstanceManager.GetParameter(Substance.AncientAlloy).MemberwiseClone().Cast<SubstanceParameters.Param>();

            // Should be unique
            param.displayNameKey = "SUB_ZINC";
            param.material = cmat.name;
            param.density = 7.0f;
            param.strength = 75;
            param.stiffness = 30;
            param.hardness = 3.0f;

            // Registering our substance as custom substance
            zinc = CustomSubstanceManager.RegisterSubstance("zinc", param, new CustomSubstanceParams
            {
                enName = "Zinc",
                jpName = "Zinc",
                behInit = (cb) =>
                {

                    // Adding test behavior
                    var beh = cb.gameObject.AddComponent<SemiconductorBehavior>();
                    return beh;
                }
            });
        }

        // 
        void RegisterGalvanizedIron()
        {
            Material alloy = SubstanceManager.GetMaterial("AncientAlloy");
            Material iron = SubstanceManager.GetMaterial("Iron");
            Material cmat = new(alloy)
            {
                name = "GalvanizedIron",
                color = new Color(iron.color.r, iron.color.g, iron.color.b + 0.03f),
            };
            // Registering material
            CustomMaterialManager.RegisterMaterial(cmat);

            // Getting substance params that our substance is based on and modifying them
            var param = SubstanceManager.GetParameter(Substance.Iron).MemberwiseClone().Cast<SubstanceParameters.Param>();

            // Should be unique
            param.displayNameKey = "SUB_GALV_IRON";
            param.material = cmat.name;
            param.density = 7.9f;
            param.strength = 1000;
            param.stiffness = 100;
            param.hardness = 5.0f;
            param.softeningPoint = 873;


            // Registering our substance as custom substance
            galvanizedIron = CustomSubstanceManager.RegisterSubstance("galvanizedIron", param, new CustomSubstanceParams
            {
                enName = "Galvanized Iron",
                jpName = "Galvanized Iron",
                behInit = (cb) =>
                {
                    // Adding NoBeh behavior to override the iron behavior
                    var beh = cb.gameObject.AddComponent<NoBeh>();
                    return beh;
                }
            });
        }

        void RegisterCoal()
        {
            // Getting wood material and coloring it BLUE
            Material cmat = new(SubstanceManager.GetMaterial("Stone"))
            {
                name = "Coal",
                color = new Color(0.10f, 0.10f, 0.10f),
            };
            // Registering material
            CustomMaterialManager.RegisterMaterial(cmat);

            // Getting substance params that our substance is based on and modifying them
            var param = SubstanceManager.GetParameter(Substance.Wood).MemberwiseClone().Cast<SubstanceParameters.Param>();

            // Should be unique
            param.displayNameKey = "SUB_COAL";
            param.material = cmat.name;
            param.collisionSound = "stone1";
            param.density = 0.9f;
            param.combustionHeat = 80000;
            param.combustionSpeed = 0.00001f;
            param.defaultPitch = 0.8f;
            param.hasSectionTexture = false;
            param.strength = 150;
            param.stiffness = 30;
            param.hardness = 4.0f;

            // Registering our substance as custom substance
            coal = CustomSubstanceManager.RegisterSubstance("coal", param, new CustomSubstanceParams
            {
                enName = "Coal",
                jpName = "Coal",
            });
        }

        public override void OnUpdate()
        {
            // Spawning blud above our head
            /*if (Input.GetKeyDown(KeyCode.P))
            {
                // Getting player position
                var mv = GameObject.Find("XR Origin").GetComponent<PlayerMovement>();

                // Generating the cube
                CubeGenerator.GenerateCube(mv.cameraTransform.position + new Vector3(0f, 0.5f, 2f), new Vector3(0.2f, 0.2f, 0.2f), copper);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                // Getting player position
                var mv = GameObject.Find("XR Origin").GetComponent<PlayerMovement>();

                // Generating the cube
                CubeGenerator.GenerateBattery(mv.cameraTransform.position + new Vector3(0f, 0.5f, 2f), new Quaternion(), 1.0f);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                // Getting player position
                var mv = GameObject.Find("XR Origin").GetComponent<PlayerMovement>();

                // Generating the cube
                CubeGenerator.GenerateElectricMotor(mv.cameraTransform.position + new Vector3(0f, 0.5f, 2f), new Quaternion());
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                // Getting player position
                var mv = GameObject.Find("XR Origin").GetComponent<PlayerMovement>();

                // Generating the cube
                CubeGenerator.GenerateCube(mv.cameraTransform.position + new Vector3(0f, 0.5f, 2f), new Vector3(0.2f, 0.2f, 0.2f), bauxiteOre);
            }*/
        }
    }

    /*    public class CopperBeh : MonoBehaviour
        {
            public CopperBeh(IntPtr ptr) : base(ptr)
            {
                // Requesting load of cube save data
                *//*CustomSaveManager.RequestLoad(this);*//*
            }

            CubeBase cubeBase;
            ElectricPart electricProperties;
            float Resistance { set; get; }
            float ohmsPerKg = 10.0f;

            float InputPower { set; get; }
            float GeneratedEnergy { set; get; }

            void OnInitialize()
            {
                // Get the cube base
                cubeBase = GetComponent<CubeBase>();
                electricProperties = GetComponent<ElectricPart>();
                //cubeBase.UpdateSubstanceBehavior();
                //cubeBase.enabled = true;

                MelonLogger.Msg("CopperBeh has initialized");
            }


            void Start()
            {
                float mass = cubeBase.rigidbodyManager.rb.mass;
                //Rigidbody rb = cubeBase.GetComponent<Rigidbody>();
                MelonLogger.Msg("Mass = " + mass);
                Resistance = ohmsPerKg / mass;
                MelonLogger.Msg("CopperBeh started with "+ Resistance + " ohm device");
            }

            void Update()
            {
                MelonLogger.Msg("Energy " + electricProperties.energy + " Cap " + electricProperties.capacity);
            }

            void ConsumeEnergy()
            {
                MelonLogger.Msg("Got ConsumeEnergy hit");
            }


            ElectricPart.PartType GetPartType(CubeConnector conn)
            {
                MelonLogger.Msg("Got GetPartType hit");
                return ElectricPart.PartType.Receiver;
            }



            void OnElectricityUpdate()
            {
                MelonLogger.Msg("Got electricity update");
                MelonLogger.Msg("In:" + InputPower + " Out:" + GeneratedEnergy);
                Il2CppSystem.Collections.Generic.List<CubeConnector> conductors = null;
                Il2CppSystem.Collections.Generic.List<ElectricPart> receivers = null;
                cubeBase.electricPart.GetCircuit(conductors, receivers, false);

                MelonLogger.Msg(conductors);
                MelonLogger.Msg(receivers);

            }
        }*/

    /*public class ZincBeh : MonoBehaviour
    {
        public ZincBeh(IntPtr ptr) : base(ptr)
        {

        }

        CubeBase cubeBase;

        void OnInitialize()
        {
            // Get the cube base
            cubeBase = GetComponent<CubeBase>();
            //cubeBase.enabled = true;
            MelonLogger.Msg("ZincBeh has initialized");
        }

        void Start()
        {

        }
    }*/

    public class NoBeh : MonoBehaviour
    {
        public NoBeh(IntPtr ptr) : base(ptr)
        {

        }

        //CubeBase cubeBase;

        void OnInitialize()
        {
            // Get the cube base
            //cubeBase = GetComponent<CubeBase>();
            //cubeBase.enabled = true;
            MelonLogger.Msg("NoBeh has initialized");
        }

        void Start()
        {

        }
    }


    public class OreBeh : MonoBehaviour
    {
        public OreBeh(IntPtr ptr) : base(ptr)
        {

        }

        CubeBase cubeBase;
        public Substance refinedSubstance { get; set; }
        bool updated = false;

        void OnInitialize()
        {
            // Get the cube base
            cubeBase = GetComponent<CubeBase>();
            cubeBase.enabled = true;
            //MelonLogger.Msg("OreBeh has initialized");
        }

        void Start()
        {
            //MelonLogger.Msg("OreBeh has started");
        }



        void Update()
        {
            if (!updated)
            {
                // check if the cube is hot enough to be transformed
                if (cubeBase.heat.GetCelsiusTemperature() > 600.0)
                {
                    //MelonLogger.Msg("Hit temp target to turn into refined");
                    cubeBase.ChangeSubstance(refinedSubstance);
                    updated = true;
                }
            }
        }
    }

    /*public class SemiconductorBehavior : ElectricPart
    {
        //float maxPowerConsumptionPerVolume;
        ElectricPart.PartType partType;
        ElectricPart.Direction direction;
        float energy;
        float capacity;

        public SemiconductorBehavior(System.IntPtr ptr) : base(ptr)
        {

        }

        public override void Start()
        {
            // Get the cube base
            cubeBase = GetComponent<CubeBase>();
            //cubeBase.enabled = true;
            MelonLogger.Msg("Semiconductor has initialized");

            partType = ElectricPart.PartType.Insulator;
            direction = ElectricPart.Direction.Side;

            energy = 100;
        }

        public override void OnElectricityUpdate()
        {
            MelonLogger.Msg("Got OnElectricityUpdate hit");

            var conductors = new Il2CppSystem.Collections.Generic.List<CubeConnector>() { };
            var receivers = new Il2CppSystem.Collections.Generic.List<ElectricPart>() { };
            bool upwardOnly = true;
            GetCircuit(conductors, receivers, upwardOnly);

            MelonLogger.Msg($"Conductors:{conductors.Count}, Receivers: {receivers.Count}");

            energy = 100;
        }

        public override ElectricPart.PartType GetPartType(CubeConnector conn)
        {
            MelonLogger.Msg("Got GetPartType hit");
            return partType;
        }
    }*/
}