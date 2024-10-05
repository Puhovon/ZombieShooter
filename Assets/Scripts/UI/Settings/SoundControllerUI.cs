using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace UI.Settings
{
    [Binding]
    public class SoundControllerUI : MonoBehaviour, INotifyPropertyChanged
    {
        [SerializeField] private Slider _slider;
        
        private float _value;

        [Binding]
        public float Value
        {
            get => _value;
            set
            {
                if(value == _value)
                    return;
                _value = value;
                _slider.value = _value;
                OnPropertyChanged(nameof(Value));
            }
            
        }

        private void Start()
        {
            Value = _slider.value;
            AkSoundEngine.SetRTPCValue("New_Game_Parameter", Value);
        }

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(SetVolume);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(SetVolume);
        }

        private void SetVolume(float volume)
        {
            AkSoundEngine.SetRTPCValue("New_Game_Parameter", volume);
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
}