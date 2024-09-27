using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class AmmoViewModel : MonoBehaviour, INotifyPropertyChanged
{
    private string _ammoText;
    private int _ammo;
    [Binding]
    public string AmmoText
    {
        get => _ammoText;
        set
        {
            if (_ammoText == value)
                return;
            _ammoText = value;
            OnPropertyChanged(nameof(AmmoText));
        }
    }

    public void SetNewAmmo(int ammo)
    {
        _ammo = ammo;
        AmmoText = $"Ammo: {_ammo}";
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
