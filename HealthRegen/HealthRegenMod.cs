using MelonLoader;
using Il2Cpp;
using PMAPI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.Json;

namespace HealthRegenMod
{
    public class HealthRegenMod : MelonMod
    {

        GameObject playerRef;
        PlayerLife playerLifeRef;

        public override void OnInitializeMelon()
        {
            // Init PMAPI
            PMAPIModRegistry.InitPMAPI(this);

            playerRef = GameObject.Find("Player");
            playerLifeRef = playerRef.GetComponent<PlayerLife>();
        }

        DateTime sysTime;
        public float lastRegenHealth = 1000f;
        public float regenDelay = 1.0f;
        public float regenIncrease = 10.0f;
        public float currTime = 0.0f;
        public float lastTime = 0.0f;
        public byte regenCooldownCounter = 0;
        public byte regenCooldownCounterResetVal = 7;


        public override void OnUpdate()
        {
            base.OnUpdate();
            sysTime = DateTime.Now;

            float currTime = Time.fixedUnscaledTime;
            //PMFLog.Message("CurrTime: " + currTime.ToString() + " LastRecord: " + lastTime.ToString() + " Delta: " + (currTime - lastTime).ToString());

            /*if (PlayerLife.Life < 600)
            {
                playerLifeRef.Die();
            }*/


            if (currTime - lastTime > regenDelay)
            {
                if (lastRegenHealth != PlayerLife.Life)
                {
                    regenCooldownCounter = regenCooldownCounterResetVal;
                }
                if (regenCooldownCounter == 0)
                {
                    if (PlayerLife.Life + regenIncrease > 1000f)
                    {
                        PlayerLife.Life = 1000f;
                    }
                    else
                    {
                        PlayerLife.Life += regenIncrease;
                    }
                }
                if (regenCooldownCounter > 0)
                {
                    //PMFLog.Message("regencooldown subtraction: " + regenCooldownCounter.ToString());
                    regenCooldownCounter--;
                }
                lastRegenHealth = PlayerLife.Life;
                lastTime = currTime;
            }
        }
    }
}