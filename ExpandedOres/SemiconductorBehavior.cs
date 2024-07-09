using Il2Cpp;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Il2CppSystem;

namespace ExpandedOres
{
    internal class SemiconductorBehavior : ElectricPart
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
    }
}
