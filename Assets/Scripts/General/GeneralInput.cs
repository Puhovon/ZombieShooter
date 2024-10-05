using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace General
{
    public class GeneralInput : MonoBehaviour
    {
        [SerializeField] private GameObject _menu;
        private InputSystem_Actions _actions;
        private bool _gameStopped;
        
        [Inject]
        private void Construct(InputSystem_Actions actions)
        {
            _actions = actions;
        }

        private void OnEnable()
        {
            _actions.General.OpenMenu.performed += SetPause;    
            _actions.General.Enable();
        }

        private void SetPause(InputAction.CallbackContext obj)
        {
            if (!_gameStopped)
            {
                Cursor.lockState = CursorLockMode.Confined;
                _actions.Player.Disable();
                _menu.SetActive(true);
                Time.timeScale = 0;
                _gameStopped = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                _actions.Player.Enable();
                _menu.SetActive(false);
                Time.timeScale = 1;
                _gameStopped = false;
            }
        }

        private void OnDisable()
        {
            _actions.General.OpenMenu.performed -= SetPause;
            _actions.General.Disable();
        }
    }
}