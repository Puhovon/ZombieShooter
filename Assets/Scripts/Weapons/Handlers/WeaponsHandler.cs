using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons.Abstraction;
using Zenject;

namespace Weapons
{
    public class WeaponsHandler : MonoBehaviour
    {
        [SerializeField] private Weapon[] _weapons;
        [SerializeField] private int length;
        [SerializeField] private int _currentWeaponIndex;
        private InputSystem_Actions _input;
        
        [Inject]
        private void Constuct(InputSystem_Actions input)
        {
            _input = input;
        }

        private void OnEnable()
        {
            _input.Player.SelectGun.performed += SelectGun;
            _weapons[0].gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _input.Player.SelectGun.performed -= SelectGun;
        }

        private void SelectGun(InputAction.CallbackContext cc)
        {
            var value = cc.ReadValue<float>();
            if(value == 0) 
                return;
            if (value == 1)
                GetNextGun();
            else if (value == -1)
                GetPreviousGun();
        }

        private void GetPreviousGun()
        {
            CalculateGunIndex(false);
        }

        private void CalculateGunIndex(bool b)
        {
            _weapons[_currentWeaponIndex].gameObject.SetActive(false);
            if (_currentWeaponIndex + 1 > _weapons.Length - 1)
                _currentWeaponIndex = 0;
            else if (_currentWeaponIndex == 0)
                _currentWeaponIndex = _weapons.Length - 1;
            else
                _currentWeaponIndex = b ? _currentWeaponIndex++ : _currentWeaponIndex--;

            _weapons[_currentWeaponIndex].gameObject.SetActive(true);
        }

        private void GetNextGun()
        {
            CalculateGunIndex(true);
        }
    }
}