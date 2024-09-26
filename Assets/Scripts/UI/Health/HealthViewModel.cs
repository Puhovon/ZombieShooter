using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Global;
using UnityEngine;
using UnityWeld.Binding;
using Zenject;

[Binding]
public class HealthViewModel : MonoBehaviour, INotifyPropertyChanged
{
    private Health _playerHealth;
    private int _healthPoints;

    private string _healthText = "";
    
    [Binding]
    public string HealthPoints
    {
        get => _healthText;
        set
        {
            if(_healthText == value)
                return;
            _healthText = value;
            OnPropertyChanged("HealthPoints");
        }
    }
    
    [Inject]
    private void Construct(Health playerHealth)
    {
        _playerHealth = playerHealth;
    }

    private void OnEnable()
    {
        _playerHealth.OnHealthChanged += SetNewHealthPoints;
    }
    
    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= SetNewHealthPoints;
    }
    
    private void SetNewHealthPoints(int health)
    {
        _healthPoints = health;
        HealthPoints = $"Health: {_healthPoints}";
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
