/*using MelonLoader;
using System;
using System.IO;
using UnityEngine;
using Unity;
using Il2CppTMPro;

namespace PMAPI.CustomMenus
{
    public class MenuTool : MonoBehaviour
    {
        public static MenuTool DebugTool = null;

        public MenuTool(System.IntPtr ptr) : base(ptr) { }


        private static Menu _CurrentMenu = null;
        private static Transform _Menus;

        public static Menu MainMenu = null;

        public static Menu ToolSettingsMenu = null;

        /// <summary>
        /// The MelonPreferences_Entry used to store if the tool should lock to your hand
        /// </summary>
        public static MelonPreferences_Entry<bool> LockToolToHandEntry;

        /// <summary>
        /// Is true when the setting lock to hand is on
        /// </summary>
        public static bool LockToolToHand { get { if (LockToolToHandEntry != null) return LockToolToHandEntry.Value; else return true; } }

        /// <summary>
        /// Is true when the debug tool is shown
        /// </summary>
        public static bool IsShown { get { return DebugTool != null && DebugTool.isActiveAndEnabled; } }

        public static void Hide()
        {
            if (DebugTool == null)
            {
                return;
            }

            DebugTool.gameObject.SetActive(false);

        }

        public static void Create()
        {
            var tool = GameObject.Find("MenuTool");
            if (tool != null)
            {
                DebugTool = tool.GetComponent<MenuTool>();
                return;
            }


            var gameObject = new GameObject();
            gameObject.transform.parent = PlayerStates.SystemTransform;
            gameObject.name = "MenuTool";
            gameObject.transform.localScale = new Vector3(1, 1, 1);

            var pannel = CreatePannel(gameObject.transform);

            var menusGameObject = new GameObject();
            menusGameObject.transform.parent = gameObject.transform;
            menusGameObject.transform.localPosition = new Vector3(0, 0, 0);
            menusGameObject.transform.localRotation = Quaternion.identity;
            menusGameObject.transform.localScale = new Vector3(1, 1, 1);
            _Menus = menusGameObject.transform;


            MainMenu = CreateMenu("MainMenu", null, "MOD SETTINGS");
            var closeButton = MainMenu.CreateButton("Close");
            closeButton.AttachOnPressListener(new System.Action(() =>
            {
                Hide();
            }));


            OpenMenu("MainMenu");

            DebugTool = gameObject.AddComponent<MenuTool>();

            DebugTool.gameObject.SetActive(false);


            var toolSettingsCategory = MelonPreferences.CreateCategory("Tool Settings");
            LockToolToHandEntry = toolSettingsCategory.CreateEntry("LockToHand", true);

            ToolSettingsMenu = CreateMenu("Settings", "MainMenu");

            ToolSettingsMenu.CreateToggleButton("Lock to hand", LockToolToHandEntry);

        }


        /// <summary>
        /// Moves the tool to the new position and rotation
        /// </summary>
        /// <param name="pos">new position</param>
        /// <param name="rot">new rotation</param>
        public static void UpdateToolPosRot(Vector3 pos, Quaternion rot)
        {
            if (DebugTool == null || !DebugTool.isActiveAndEnabled)
            {
                return;
            }

            DebugTool.transform.position = pos;
            DebugTool.transform.rotation = rot;
        }


        public static void Show()
        {
            if (DebugTool == null)
            {
                return;
            }

            DebugTool.gameObject.SetActive(true);
            UpdateToolPosRot(PlayerStates.MenuWindowL.position, PlayerStates.MenuWindowL.rotation);
        }



        public static Menu CreateMenu(string name, string parentMenuName, string title = null)
        {
            var existingMenu = GetMenu(name);
            if (existingMenu != null)
            {
                return existingMenu;
            }

            var menuGameObject = new GameObject();
            menuGameObject.name = name;
            menuGameObject.transform.parent = _Menus.transform;

            if (title == null)
            {
                title = name;
            }
            title = title.ToUpper();

            var menu = menuGameObject.AddComponent<Menu>();

            GameObject textGameObject = new GameObject("Title");
            textGameObject.transform.parent = menuGameObject.transform;
            var text = textGameObject.AddComponent<TMP_Text>();
            text.text = title;
            text.fontSize = 0.5f;
            text.color = Color.white;
            text.alignment = TextAlignmentOptions.Center;
            textGameObject.transform.localScale = new Vector3(1, 1, 1);
            textGameObject.transform.localPosition = new Vector3(0, 0.1f, -0.011f);

            if (parentMenuName != null)
            {
                menu.CreateButton("Back", new System.Action(() =>
                {
                    OpenMenu(parentMenuName);
                }));

                var parentMenu = GetMenu(parentMenuName);
                parentMenu.CreateButton(name, new System.Action(() =>
                {
                    OpenMenu(name);
                }));
            }

            menuGameObject.SetActive(false);
            return menu;
        }

        public static Menu GetMenu(string name)
        {
            return _Menus.Find(name)?.GetComponent<Menu>();
        }
        public static void OpenMenu(string name)
        {
            if (_CurrentMenu != null)
            {
                _CurrentMenu.gameObject.SetActive(false);
            }
            _CurrentMenu = GetMenu(name);
            if (_CurrentMenu != null)
            {
                _CurrentMenu.gameObject.SetActive(true);
            }
        }


        private static GameObject CreatePannel(Transform parent)
        {
            var Pannel = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Pannel.transform.parent = parent;
            Pannel.transform.localScale = new Vector3(0.4f, 0.3f, 0.02f);
            Pannel.transform.localPosition = new Vector3(0, 0, 0);
            Pannel.GetComponent<MeshRenderer>().material.color = Color.black;


            return Pannel;
        }



    }
}*/