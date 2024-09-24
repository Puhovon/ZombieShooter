using Configs;
using Pools.Grenades;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Weapons
{
    public class GrenadeHandler : MonoBehaviour
    {
        [SerializeField] private GrenadeConfig _config;

        private int _grenadesHave;
        private InputSystem_Actions _input;
        private GrenadesPool _grenadesPool;
        
        [Inject]
        private void Construct(InputSystem_Actions actions, GrenadesPool pool)
        {
            _input = actions;
            _grenadesPool = pool;
        }

        private void Start()
        {
            _grenadesHave = _config.maxGrenade;
        }

        private void OnEnable()
        {
            _input.Player.Grenade.performed += TryThrowGrenade;
        }

        
        private void OnDisable()
        {
            _input.Player.Grenade.performed -= TryThrowGrenade;
        }
        
        private void TryThrowGrenade(InputAction.CallbackContext obj)
        {
            if (_grenadesHave == 0)
                return;
            var grenade = _grenadesPool.GetGrenade();
            grenade.Throw();
        }
    }
}