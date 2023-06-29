using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace CodeBase.UI
{
    [Binding]
    public class InitialViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        private string _level = "0";
        private string _health = "0";
        private string _armor = "0";
        private string _maxHealth = "0";
        private string _maxArmor = "0";
        private string _maxItemCount = "0";
        private string _maxPistolBullets = "0";
        private string _maxGunBullets = "0";

        [Binding]
        public string Level {
            get => _level;
            set {
                if (_level.Equals(value)) return;
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }
        
        [Binding]
        public string Health {
            get => _health;
            set {
                if (_health.Equals(value)) return;
                _health = value;
                OnPropertyChanged(nameof(Health));
            }
        }
        
        [Binding]
        public string Armor {
            get => _armor;
            set {
                if (_armor.Equals(value)) return;
                _armor = value;
                OnPropertyChanged(nameof(Armor));
            }
        }
        
        [Binding]
        public string MaxHealth {
            get => _maxHealth;
            set {
                if (_maxHealth.Equals(value)) return;
                _maxHealth = value;
                OnPropertyChanged(nameof(MaxHealth));
            }
        }
        
        [Binding]
        public string MaxArmor {
            get => _maxArmor;
            set {
                if (_maxArmor.Equals(value)) return;
                _maxArmor = value;
                OnPropertyChanged(nameof(MaxArmor));
            }
        }
        
        [Binding]
        public string MaxItemCount {
            get => _maxItemCount;
            set {
                if (_maxItemCount.Equals(value)) return;
                _maxItemCount = value;
                OnPropertyChanged(nameof(MaxItemCount));
            }
        }
        
        [Binding]
        public string MaxPistolBullets {
            get => _maxPistolBullets;
            set {
                if (_maxPistolBullets.Equals(value)) return;
                _maxPistolBullets = value;
                OnPropertyChanged(nameof(MaxPistolBullets));
            }
        }
        
        [Binding]
        public string MaxGunBullets {
            get => _maxGunBullets;
            set {
                if (_maxGunBullets.Equals(value)) return;
                _maxGunBullets = value;
                OnPropertyChanged(nameof(MaxGunBullets));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}