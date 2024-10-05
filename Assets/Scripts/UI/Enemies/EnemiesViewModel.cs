using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Enemies.EnemyWave;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class EnemiesViewModel : MonoBehaviour, INotifyPropertyChanged
{
    [SerializeField] private EnemyWaveController _wave;

    private string _enemyCount;
    private const string Text = "Enemies: ";
    
    [Binding]
    public string EnemyCount
    {
        get => _enemyCount;
        set
        {
            if(value == _enemyCount)
                return;
            _enemyCount = value;
            OnPropertyChanged(nameof(EnemyCount));
        }
    }

    private void OnEnable()
    {
        _wave.EnemyCountChanged += OnEnemyCountChanged;
    }


    private void OnDisable()
    {
        _wave.EnemyCountChanged -= OnEnemyCountChanged;
    }
    
    
    private void OnEnemyCountChanged(int count)
    {
        EnemyCount = $"{Text}{count}";
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
