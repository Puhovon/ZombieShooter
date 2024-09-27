using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Weapons;
using Zenject;

namespace Pools.Grenades
{
    public class GrenadesPool : MonoBehaviour
    {
        [SerializeField] private Grenade _grenadePrefab;

        private List<Grenade> _grenades;
        private IInstantiator _instantiator;
        
        [Inject]
        private void Construct(List<Grenade> grenades, IInstantiator instantiator)
        {
            _grenades = grenades;
            _instantiator = instantiator;
        }

        public Grenade GetGrenade()
        {
            var grenade = _grenades.FirstOrDefault(g => !g.gameObject.activeSelf);
            if(grenade is null)
                grenade = CreateNewGrenade();
            grenade.gameObject.SetActive(true);
            return grenade;
        }

        private Grenade CreateNewGrenade()
        {
            var g = _instantiator.InstantiatePrefab(_grenadePrefab, transform.position, Quaternion.identity, transform).GetComponent<Grenade>();
            _grenades.Add(g);
            return g;
        }
    }
}