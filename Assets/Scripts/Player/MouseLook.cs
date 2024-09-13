using System;
using Configs;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class MouseLook : MonoBehaviour
    {
        private InputSystem_Actions _input;
        private PlayerConfig _config;
        
        [Inject]
        private void Construct(InputSystem_Actions input, PlayerConfig config)
        {
            _config = config;
            _input = input;
            _input.Player.Look.performed += GetLook;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void GetLook(InputAction.CallbackContext callbackContext)
        {
            var _currentLookDelta = callbackContext.ReadValue<Vector2>(); 
            float horizontalRotationDelta = _currentLookDelta.x * _config.RotateSpeedX * Time.deltaTime;
            transform.rotation *= Quaternion.AngleAxis(horizontalRotationDelta, Vector3.up);

            float verticalRotationDelta = -_currentLookDelta.y * _config.RotateSpeedY * Time.deltaTime;
            transform.rotation *= Quaternion.AngleAxis(verticalRotationDelta, Vector3.right);

            var eulerAngles = transform.localEulerAngles;
            eulerAngles.z = 0;

            var verticalAngle = transform.localEulerAngles.x;

            if (verticalAngle > 180 && verticalAngle < 360 - _config.VerticalRotationHigherBound)
            {
                eulerAngles.x = 360 - _config.VerticalRotationHigherBound;
            }
            else if(verticalAngle < 180 && verticalAngle > - _config.VerticalRotationLowerBound)
            {
                eulerAngles.x = -_config.VerticalRotationLowerBound;
            }
            transform.localEulerAngles = eulerAngles;
        }
    }
}