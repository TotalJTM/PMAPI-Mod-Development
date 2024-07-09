using Il2Cpp;
using Il2CppSystem.ComponentModel;
using Il2CppTMPro;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace PMAPI
{
    public class PlayerHelper : MonoBehaviour
    {
        public static Transform LHandTransform;
        public static Transform RHandTransform;
        public static Transform CameraRigTransform;
        public static Transform PlayerTransform;
        public static PlayerMovement PlayerMovementRef;
        public static PlayerLife PlayerLifeRef;

        public static Transform LeftMenuTransform;

        public Vector2Int WorldCenter;

        /// <summary>
		/// The system transform is used to store gameobjects that don't require saving (Custom objects that are not Cubes)
		/// </summary>
		public static Transform SystemTransform;

        public static TMP_FontAsset PrimitierDefaultFont = null;

        public void Start()
        {
            var playerGameObj = GameObject.Find("Player");

            PlayerMovementRef = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
            PlayerLifeRef = GameObject.FindWithTag("Player").GetComponent<PlayerLife>();
            //SystemTransform = GetComponent<VRM>();
            PlayerTransform = GetComponent<TerrainMeshGenerator>().player;

            WorldCenter = CubeGenerator.worldCenter;

            CameraRigTransform = PlayerLifeRef.mainCamera;

            LHandTransform = PlayerLifeRef.grabberL.realHand;
            RHandTransform = PlayerLifeRef.grabberR.realHand;

            LeftMenuTransform = GetComponent<MenuWindowL>().window.transform;


            /*private static TMP_FontAsset FindPrimitierDefaultFont()
            {

                var fonts = GameObject.FindObjectsOfTypeIncludingAssets(type(TMP_FontAsset));
                foreach (var font in fonts)
                {
                    if (font.name == "mplus-1p-black SDF")
                    {
                        var castedFont = font.TryCast<TMP_FontAsset>();
                        if (castedFont == null)
                            continue;

                        return castedFont;
                    }

                }

                return null;
            }*/
        }

        public void Reload()
        {
            /*PlayerMovementRef = GetComponent<PlayerMovement>();
            PlayerLifeRef = GetComponent<PlayerLife>();
            //SystemTransform = GetComponent<VRM>();
            PlayerTransform = GetComponent<TerrainMeshGenerator>().player;

            WorldCenter = CubeGenerator.worldCenter;

            CameraRigTransform = PlayerLifeRef.mainCamera;

            LHandTransform = PlayerLifeRef.grabberL.realHand;
            RHandTransform = PlayerLifeRef.grabberR.realHand;

            LeftMenuTransform = GetComponent<MenuWindowL>().window.transform;*/


            /*private static TMP_FontAsset FindPrimitierDefaultFont()
            {

                var fonts = GameObject.FindObjectsOfTypeIncludingAssets(type(TMP_FontAsset));
                foreach (var font in fonts)
                {
                    if (font.name == "mplus-1p-black SDF")
                    {
                        var castedFont = font.TryCast<TMP_FontAsset>();
                        if (castedFont == null)
                            continue;

                        return castedFont;
                    }

                }

                return null;
            }*/

        }
    }
}