namespace Enemies.Abstractions
{
    public interface IEnemyState
    {
        void Enter();
        void Exit();
        void Update();
    }
}