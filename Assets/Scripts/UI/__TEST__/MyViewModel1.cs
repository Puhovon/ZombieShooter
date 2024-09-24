using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityWeld.Binding;

namespace UI.__TEST__
{
    [Binding]
    public class MyViewModel1 : MonoBehaviour, INotifyPropertyChanged
    {
        private float _timer = 0;
        private string _text = "text";
        
        [Binding]
        public string Text
        {
            get => _text;
            set
            {
                if (_text == value)
                {
                    return;
                }

                _text = value;
                OnPropertyChanged("Text");
            }
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

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > 10f)
            {
                Text = "New text";
            }
        }
    }
}