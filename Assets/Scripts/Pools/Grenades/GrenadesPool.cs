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

        [Inject]
        private void Construct(List<Grenade> grenades)
        {
            _grenades = grenades;
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
            var g = Instantiate(_grenadePrefab, transform, true);
            _grenades.Add(g);
            return g;
        }
    }
}