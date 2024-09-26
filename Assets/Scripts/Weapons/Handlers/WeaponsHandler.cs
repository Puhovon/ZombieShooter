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
        private AmmoViewModel _ammoView;
        private Weapon _currentWeapon;
        private InputSystem_Actions _input;
        protected Weapon CurrentWeapon => _currentWeapon;
        
        [Inject]
        private void Constuct(InputSystem_Actions input, AmmoViewModel view)
        {
            _input = input;
            _ammoView = view;
        }

        private void OnEnable()
        {
            _input.Player.SelectGun.performed += SelectGun;
            _currentWeapon = _weapons[0];
            _currentWeapon.OnAmmoChanged += _ammoView.SetNewAmmo;
            _currentWeapon.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _input.Player.SelectGun.performed -= SelectGun;
            _currentWeapon.OnAmmoChanged -= _ammoView.SetNewAmmo;
        }

        private void SelectGun(InputAction.CallbackContext cc)
        {
            var value = cc.ReadValue<float>();
            if(value == 0) 
                return;
            if (value == 1)
                CalculateGunIndex(true);
            else if (value == -1)
                CalculateGunIndex(false);
        }

        private void CalculateGunIndex(bool b)
        {
            if (_currentWeaponIndex + 1 > _weapons.Length - 1)
                _currentWeaponIndex = 0;
            else if (_currentWeaponIndex == 0)
                _currentWeaponIndex = _weapons.Length - 1;
            else
                _currentWeaponIndex = b ? _currentWeaponIndex++ : _currentWeaponIndex--;
            SetGun();
        }

        private void SetGun()
        {
            _currentWeapon.OnAmmoChanged -= _ammoView.SetNewAmmo;
            _currentWeapon.gameObject.SetActive(false);
            _currentWeapon = _weapons[_currentWeaponIndex];
            _currentWeapon.OnAmmoChanged += _ammoView.SetNewAmmo;
            _currentWeapon.gameObject.SetActive(true);
        }
        
    }
}