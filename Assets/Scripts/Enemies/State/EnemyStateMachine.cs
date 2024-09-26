using System;
using System.Collections.Generic;
using System.Linq;
using Configs.Enemy;
using Enemies;
using Enemies.Abstractions;
using Enemies.State.States;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : IStateSwitcher
{
    private List<IEnemyState> _states;
    private IEnemyState _currentState;
    
    public EnemyStateMachine(NavMeshAgent agent, EnemyConfig config, Enemy enemy, LayerMask mask, EnemyView view)
    {
        _states = new List<IEnemyState>()
        {
            new MovementState(agent, config, enemy, this, view),
            new AttackState(agent, config, enemy, this, mask, view),
        };
        _currentState = _states[0];
    }

    public void Update()
    {
        if (_currentState is null)
            throw new ArgumentNullException($"{nameof(_currentState)} is not defined");
        _currentState.Update();
    }
    
    public void SwitchState<T>() where T : IEnemyState
    {
        Debug.Log("SwitchSState");
        _currentState.Exit();
        _currentState = _states.FirstOrDefault((state) => state is T);
        if (_currentState is null)
            throw new ArgumentNullException($"{nameof(T)} is not defined");
        _currentState.Enter();
    }
}
