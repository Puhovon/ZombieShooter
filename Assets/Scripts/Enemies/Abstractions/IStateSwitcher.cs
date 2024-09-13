namespace Enemies.Abstractions
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IEnemyState;
    }
}