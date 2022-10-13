using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMD
{
    public class InteractablePopup : MonoBehaviour
    {
        public Text alertText;

        private void Start()
        {
            alertText = GetComponentInChildren<Text>();
            Hide();
        }

        public void Show(string text)
        {
            alertText.text = text;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
