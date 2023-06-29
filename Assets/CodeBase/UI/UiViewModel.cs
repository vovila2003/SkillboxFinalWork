using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace CodeBase.UI
{
    [Binding]
    public class UiViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _healthCount = "";
        private string _armorCount = "";
        private string _maxHealth = "";
        private string _maxArmor = "";
        private string _extraPistolBullets = "";
        private string _maxExtraPistolBullets = "";
        private string _extraGunBullets = "";
        private string _maxExtraGunBullets = "";
        private string _maxItemCount = "";
        private bool _showHealth;
        private bool _showArmor;
        private bool _showKnife;
        private bool _showPistol;
        private bool _showGun;

        [Binding]
        public string HealthCount {
            get => _healthCount;
            set {
                if (_healthCount.Equals(value)) return;
                _healthCount = value;
                OnPropertyChanged(nameof(HealthCount));
            }
        }

        [Binding]
        public string ArmorCount {
            get => _armorCount;
            set {
                if (_armorCount.Equals(value)) return;
                _armorCount = value;
                OnPropertyChanged(nameof(ArmorCount));
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
        public string ExtraPistolBullets {
            get => _extraPistolBullets;
            set {
                if (_extraPistolBullets.Equals(value)) return;
                _extraPistolBullets = value;
                OnPropertyChanged(nameof(ExtraPistolBullets));
            }
        }
        
        [Binding]
        public string MaxExtraPistolBullets {
            get => _maxExtraPistolBullets;
            set {
                if (_maxExtraPistolBullets.Equals(value)) return;
                _maxExtraPistolBullets = value;
                OnPropertyChanged(nameof(MaxExtraPistolBullets));
            }
        }

        [Binding]
        public string ExtraGunBullets {
            get => _extraGunBullets;
            set {
                if (_extraGunBullets.Equals(value)) return;
                _extraGunBullets = value;
                OnPropertyChanged(nameof(ExtraGunBullets));
            }
        }
        
        [Binding]
        public string MaxExtraGunBullets {
            get => _maxExtraGunBullets;
            set {
                if (_maxExtraGunBullets.Equals(value)) return;
                _maxExtraGunBullets = value;
                OnPropertyChanged(nameof(MaxExtraGunBullets));
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
        public bool ShowHealth {
            get => _showHealth;
            set {
                if (_showHealth == value)  return;
                _showHealth = value;
                OnPropertyChanged(nameof(ShowHealth));
            }
        }
        
        [Binding]
        public bool ShowArmor {
            get => _showArmor;
            set {
                if (_showArmor == value)  return;
                _showArmor = value;
                OnPropertyChanged(nameof(ShowArmor));
            }
        }
        
        [Binding]
        public bool ShowKnife {
            get => _showKnife;
            set {
                if (_showKnife == value)  return;
                _showKnife = value;
                OnPropertyChanged(nameof(ShowKnife));
            }
        }

        [Binding]
        public bool ShowPistol {
            get => _showPistol;
            set {
                if (_showPistol == value)  return;
                _showPistol = value;
                OnPropertyChanged(nameof(ShowPistol));
            }
        }

        [Binding]
        public bool ShowGun {
            get => _showGun;
            set {
                if (_showGun == value)  return;
                _showGun = value;
                OnPropertyChanged(nameof(ShowGun));
            }
        }

        
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}