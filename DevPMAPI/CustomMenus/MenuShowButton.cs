/*using PMAPI.CustomMenus;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppSystem;
using Il2CppTMPro;
using UnityEngine;

namespace PMAPI.CustomMenus
{
    public class MenuShowButton : MonoBehaviour
    {
        public MenuShowButton(System.IntPtr ptr) : base(ptr) { }



        public void OnPress()
        {
            *//*if (MenuTool.IsShown)
            {
                MenuTool.Hide();
            }
            else
            {
                MenuTool.Show();
            }*//*
            MelonLogger.Msg("Button has been pressed");


        }

        public static void Create()
        {
            //For V0.2 or lower
            var textGameObjectTransform = PlayerStates.MenuWindowL.Find("Text");
            if (textGameObjectTransform != null)
            {
                return;
            }

            var buttonTransform = PlayerStates.MenuWindowL.Find("ShowDebugToolButton");
            if (buttonTransform != null)
            {
                return;
            }

            var button = GameObject.CreatePrimitive(PrimitiveType.Cube);
            button.name = "ShowMenuToolButton";
            button.transform.parent = PlayerStates.MenuWindowL;
            button.transform.localScale = new Vector3(0.3f, 0.05f, 0.01f);
            button.transform.localRotation = Quaternion.identity;
            button.transform.localPosition = new Vector3(0, -0.03f, 0);
            button.GetComponent<MeshRenderer>().material.color = Color.grey;
            button.AddComponent<MenuShowButton>();

            var textGameObject = new GameObject("ShowMenuToolButtonText");
            textGameObject.transform.parent = PlayerStates.MenuWindowL;
            var textMP = textGameObject.AddComponent<TMP_Text>();
            textMP.text = "Show Mod Menu";
            textMP.fontSize = 0.3f;
            textMP.color = Color.black;
            textMP.alignment = TextAlignmentOptions.Center;
            textGameObject.transform.localScale = new Vector3(1, 1, 1);
            textGameObject.transform.localPosition = new Vector3(0, -0.03f, -0.006f);
            textGameObject.transform.localRotation = Quaternion.identity;
        }


    }
}*/