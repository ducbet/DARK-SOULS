using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TMD
{
    public class PopUpWindows : MonoBehaviour
    {
        public GameObject selectMenuWindow;
        private InputManager inputManager;

        private void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
        }
        // Start is called before the first frame update
        void Start()
        {
            inputManager.playerControls.SystemAction.ToggleSelectMeunu.performed += HandleToggleSelectMenuInput;
        }
        private void OnDestroy()
        {
            inputManager.playerControls.SystemAction.ToggleSelectMeunu.performed -= HandleToggleSelectMenuInput;
        }

        private void HandleToggleSelectMenuInput(InputAction.CallbackContext context)
        {
            ToggleWindow(selectMenuWindow);
        }

        private void ToggleWindow(GameObject window)
        {
            window.SetActive(!window.activeSelf);
        }
    }
}
