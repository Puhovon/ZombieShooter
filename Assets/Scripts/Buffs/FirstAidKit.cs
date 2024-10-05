using UnityEngine;

namespace Buffs
{
    public class FirstAidKit : MonoBehaviour
    {
        [SerializeField] private FirstAidKitView _view;
        [SerializeField] private int _heal;
        
        private void Start()
        {
            _view.StartDefaultAnim();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IHealable healable) && other.CompareTag("Player"))
            {
                healable.Heal(_heal);
                _view.StartOnTakeAnim();
            }
        }
    }
}