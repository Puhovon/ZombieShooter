using System;
using System.Collections.Generic;
using System.Linq;
using Enemies.Abstractions;

public class EnemyStateMachine : IStateSwitcher
{
    private List<IEnemyState> _states;
    private IEnemyState _currentState;
    
    public EnemyStateMachine()
    {
        
    }

    public void Update()
    {
        _currentState.Update();
    }
    
    public void SwitchState<T>() where T : IEnemyState
    {
        _currentState.Exit();
        _currentState = _states.FirstOrDefault((state) => state is T);
        if (_currentState is null)
            throw new ArgumentNullException($"{nameof(T)} is not defined");
        _currentState.Enter();
    }
}
