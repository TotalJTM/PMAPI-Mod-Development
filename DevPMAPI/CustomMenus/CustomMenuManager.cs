using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PMAPI.CustomMenus
{
    public class CustomMenuManager : MonoBehaviour
    {

        //internal static MenuButton modMenuButton;
        //internal static Menu modMenu = null;

        internal static void Init()
        {
            // Warm up the manager so it loads everything properly
            //SubstanceManager.GetMaterial("Stone");
            //modMenu = MenuTool.CreateMenu("Mod Menu", "MainMenu");
            //MenuTool.Create();

        }

        public void OnSceneLoad()
        {
            //MenuTool.Create();
            //MenuShowButton.Create();
        }

        public void OnUpdate()
        {
            /*if (MenuTool.LockToolToHand && PlayerStates.MenuWindowL != null) //PlayerStates.MenuWindowL != null is to check if where not in the unity loading screen
            {
                MenuTool.UpdateToolPosRot(PlayerStates.MenuWindowL.position, PlayerStates.MenuWindowL.rotation);
            }*/
        }

        // mod page title, menu to open
        internal static Dictionary<string, Menu> modMenus = new();

        internal static void RegisterMenu(string name, Menu menu)
        {
            modMenus.Add(name, menu);
        }
    }
}
