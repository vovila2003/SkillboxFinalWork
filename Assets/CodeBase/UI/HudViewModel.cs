using System.ComponentModel;
using System.Runtime.CompilerServices;
using CodeBase.Common;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using UnityWeld.Binding;

namespace CodeBase.UI
{
    [Binding]
    public class HudViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [Required, SerializeField] private Slider ExperienceSlider;

        private string _health = "";
        private string _armor = "";
        private string _currentBullets = "";
        private string _extraBullets = "";
        private string _level = "";
        private float _experience;

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
        public string CurrentBullets {
            get => _currentBullets;
            set {
                if (_currentBullets.Equals(value)) return;
                _currentBullets = value;
                OnPropertyChanged(nameof(CurrentBullets));
            }
        }
        
        [Binding]
        public string ExtraBullets {
            get => _extraBullets;
            set {
                if (_extraBullets.Equals(value)) return;
                _extraBullets = value;
                OnPropertyChanged(nameof(ExtraBullets));
            }
        }

        [Binding]
        public float Experience {
            get => _experience;
            set {
                if (Mathf.Abs(_experience - value) < Constants.Threshold) return;
                _experience = value;
                OnPropertyChanged(nameof(Experience));
            }
        }

        [Binding]
        public string Level {
            get => _level;
            set {
                if (_level.Equals(value)) return;
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        public void SetMaxExperience(float maxExperience) => 
            ExperienceSlider.maxValue = maxExperience;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}