﻿
using MelonLoader;
using UnityEngine;
using Il2CppTMPro;

namespace PMAPI.CustomMenus
{
    public class Menu : MonoBehaviour
    {
        public Menu(System.IntPtr ptr) : base(ptr) { }

        private const float s_buttonZSize = 0.02f;
        private const float s_buttonHeight = 0.05f;
        private const float s_buttonWidth = 0.1f;
        private const float s_buttonSpaceing = 0.01f;
        private const int s_maxButtonsPerLine = 3;

        private int _buttonsOnCurrentLine = 0;
        private Vector2 _nextButtonPos = new Vector2(-0.1f, 0.05f);


        public MenuButton CreateButton(string text, Il2CppSystem.Action opPress)
        {
            var button = CreateButton(text);
            button.AttachOnPressListener(opPress);
            return button;
        }


        /// <summary>
        /// Creates a toggle button and adds it to this menu.
        /// The value gets automatically synced with the MelonPreferences_Entry provided.
        /// </summary>
        /// <param name="text">text to show on the button</param>
        /// <param name="entry">The entry to sync to</param>
        /// <returns>The created button</returns>
        /// <exception cref="PMFSystemNotEnabledException"></exception>
        public MenuToggleButton CreateToggleButton(string text, MelonPreferences_Entry<bool> entry)
        {
            var button = CreateToggleButton(text);
            button.Value = entry.Value;
            button.AttachOnValueChanged(new System.Action(() =>
            {
                entry.Value = button.Value;
            }));

            return button;
        }


        /// <summary>
        /// Creates a toggle button and adds it to this menu
        /// </summary>
        /// <param name="text">text to show on the button</param>
        /// <returns>The created button</returns>
        /// <exception cref="PMFSystemNotEnabledException"></exception>
        public MenuToggleButton CreateToggleButton(string text)
        {
            var buttonGameObject = new GameObject();
            buttonGameObject.transform.parent = transform;
            buttonGameObject.name = "ToggleButton";
            buttonGameObject.transform.localScale = new Vector3(s_buttonWidth, s_buttonHeight, s_buttonZSize);
            buttonGameObject.transform.localPosition = new Vector3(_nextButtonPos.x, _nextButtonPos.y, -s_buttonZSize);

            buttonGameObject.AddComponent<BoxCollider>();
            var button = buttonGameObject.AddComponent<MenuToggleButton>();


            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = buttonGameObject.transform;
            cube.transform.localPosition = Vector3.zero;
            cube.transform.localScale = new Vector3(1, 1, 1);
            cube.GetComponent<MeshRenderer>().material.color = Color.grey;
            Destroy(cube.GetComponent<BoxCollider>());


            GameObject textGameObject = new GameObject("Text");
            textGameObject.transform.parent = transform;

            var textMP = textGameObject.AddComponent<TMP_Text>();
            textMP.text = text;
            textMP.fontSize = 0.1f;
            textMP.color = Color.black;
            textMP.alignment = TextAlignmentOptions.Center;
            textGameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            textGameObject.transform.localPosition = new Vector3(_nextButtonPos.x, _nextButtonPos.y, -0.031f);


            button.CubeTransform = cube.transform;

            _buttonsOnCurrentLine++;
            _nextButtonPos += new Vector2(s_buttonWidth + s_buttonSpaceing, 0);
            if (_buttonsOnCurrentLine >= s_maxButtonsPerLine)
            {
                _buttonsOnCurrentLine = 0;
                _nextButtonPos.x = -0.1f;
                _nextButtonPos.y -= s_buttonHeight + s_buttonSpaceing;
            }


            return button;
        }


        /// <summary>
        /// Creates a button and adds it to this menu
        /// </summary>
        /// <param name="text">The text on the button</param>
        /// <returns>The created button</returns>
        /// <exception cref="PMFSystemNotEnabledException"></exception>
        public MenuButton CreateButton(string text)
        {
            var buttonGameObject = new GameObject();
            buttonGameObject.transform.parent = transform;
            buttonGameObject.name = "Button";
            buttonGameObject.transform.localScale = new Vector3(s_buttonWidth, s_buttonHeight, s_buttonZSize);
            buttonGameObject.transform.localPosition = new Vector3(_nextButtonPos.x, _nextButtonPos.y, -s_buttonZSize);

            buttonGameObject.AddComponent<BoxCollider>();
            var button = buttonGameObject.AddComponent<MenuButton>();


            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.parent = buttonGameObject.transform;
            cube.transform.localPosition = Vector3.zero;
            cube.transform.localScale = new Vector3(1, 1, 1);
            cube.GetComponent<MeshRenderer>().material.color = Color.grey;
            Destroy(cube.GetComponent<BoxCollider>());


            GameObject textGameObject = new GameObject("Text");
            textGameObject.transform.parent = transform;

            var textMP = textGameObject.AddComponent<TMP_Text>();
            textMP.text = text;
            textMP.fontSize = 0.1f;
            textMP.color = Color.black;
            textMP.alignment = TextAlignmentOptions.Center;
            textGameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            textGameObject.transform.localPosition = new Vector3(_nextButtonPos.x, _nextButtonPos.y, -0.031f);


            button.CubeTransform = cube.transform;

            _buttonsOnCurrentLine++;
            _nextButtonPos += new Vector2(s_buttonWidth + s_buttonSpaceing, 0);
            if (_buttonsOnCurrentLine >= s_maxButtonsPerLine)
            {
                _buttonsOnCurrentLine = 0;
                _nextButtonPos.x = -0.1f;
                _nextButtonPos.y -= s_buttonHeight + s_buttonSpaceing;
            }


            return button;
        }



    }
}