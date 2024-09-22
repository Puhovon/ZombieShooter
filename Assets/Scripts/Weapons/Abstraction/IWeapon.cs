using System.Collections;

namespace Weapons.Abstraction
{
    public interface IWeapon
    {
        void Shoot();
        IEnumerator TimerReload();
        IEnumerator TimerToNextShoot();
    }
}