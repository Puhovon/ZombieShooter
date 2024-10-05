using System;
using UnityEngine;
using UnityWeld.Binding;

namespace UI.Settings
{
    [Binding]
    public class ButtonViewModel : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;

        private void Start()
        {
        }

        [Binding]
        public void OpenMenu()
        {
            print("OpenMenu");
            _menu.SetActive(true);
        }
        
        [Binding]
        public void CloseMenu()
        {
            print("CloseMenu");
            _menu.SetActive(false);
        }
    }
}