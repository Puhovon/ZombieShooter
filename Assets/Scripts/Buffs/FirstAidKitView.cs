using DG.Tweening;
using UnityEngine;

namespace Buffs
{
    public class FirstAidKitView : MonoBehaviour
    {
        [SerializeField] private Transform _mesh;

        public void StartDefaultAnim()
        {
            _mesh.DORotate(_mesh.position, 1f, RotateMode.FastBeyond360).SetLoops(100);
        }

        public void StartOnTakeAnim()
        {
            _mesh.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 1f).onComplete += () => Destroy(gameObject);
        }
    }
}