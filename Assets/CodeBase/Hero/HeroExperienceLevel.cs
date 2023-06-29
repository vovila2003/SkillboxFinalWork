using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Common;
using CodeBase.Hero.Interfaces;
using CodeBase.Infrastructure.GameData;
using CodeBase.Infrastructure.GameData.Signals;
using CodeBase.UI;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace CodeBase.Hero
{
    [RequireComponent(typeof(HeroSound))]
    public class HeroExperienceLevel : MonoBehaviour
    {
        [Required, SerializeField] private GameObject ChangeLevelEffect;
        [SerializeField] private List<MonoBehaviour> LevelUpActions = new List<MonoBehaviour>();

        private float _experience;
        private int _level;
        [TabGroup("Debug"), ShowInInspector] private float _experienceToNextLevel;
        private float _firstLevelExperience;
        private float _multiplier;
        private Settings _settings;
        private HudViewModel _hudViewModel;
        private float _pickUpItemBonus; 
        private float _applyItemBonus; 
        private float _shotBonus; 
        private float _killEnemyBonus;
        private Transform _transform;
        private GameObject _effect;
        private float _effectTime;
        private SignalBus _signalBus;
        private HeroLevel _heroLevel;
        private HeroSound _heroSound;
        private ParticleSystem _particleSystem;

        [ShowInInspector]
        public float CurrentExperience {
            get => _experience;
            set {
                if (Mathf.Abs(_experience - value) < Constants.Threshold) return;
                _experience = value;
                if (_experience < 0) 
                    _experience = 0;
                if (_experience >= _experienceToNextLevel) 
                    LevelUp();
                UpdateViewModelExperience();
            }
        }

        [ShowInInspector]
        public int CurrentLevel {
            get => _level;
            set {
                if (_level == value) return;
                _level = value;
                if (_level < 1)
                    _level = 1;
                _heroLevel.Level = _level;
                CalculateToNextLevelExperience();
                UpdateViewModelLevel();
            }
        }

        [Inject]
        private void Construct(Settings settings, 
                               SignalBus signalBus,
                               HeroLevel heroLevel) {
            _settings = settings;
            _signalBus = signalBus;
            _heroLevel = heroLevel;
        }

        private void Awake() => 
            _heroSound = GetComponent<HeroSound>();

        private void OnDestroy() {
            _signalBus.TryUnsubscribe<EnemyKilledSignal>(OnEnemyKilled);
        }
        
        public void RegisterModel(HudViewModel hudViewModel) {
            _transform = transform;
            _firstLevelExperience = _settings.HeroExperienceFirstLevelUp;
            _multiplier = _settings.HeroExperienceMultiplier;
            _pickUpItemBonus = _settings.HeroPickUpItemExperience;
            _applyItemBonus = _settings.HeroApplyItemExperience;
            _shotBonus = _settings.HeroShotExperience;
            _killEnemyBonus = _settings.HeroKillEnemyExperience;
            _signalBus.Subscribe<EnemyKilledSignal>(OnEnemyKilled);
            _hudViewModel = hudViewModel;
            CurrentLevel = _heroLevel.Level;
            ApplyLevelUpActions();
            CalculateToNextLevelExperience();
            _hudViewModel.SetMaxExperience(_experienceToNextLevel);
            _experience = 1;
            CurrentExperience = 0;
        }

        public void PickUpItemBonus() => 
            CurrentExperience += _pickUpItemBonus;

        public void ApplyItemBonus() => 
            CurrentExperience += _applyItemBonus;

        public void ShotBonus() => 
            CurrentExperience += _shotBonus;

        private void OnEnemyKilled(EnemyKilledSignal obj) {
            CurrentExperience += _killEnemyBonus;
        }

        [Button]
        private void IncreaseExperience(float value = 20f) => 
            CurrentExperience += value;

        private void UpdateViewModelExperience() {
            if (_hudViewModel == null) return;
            _hudViewModel.Experience = _experience;
        }

        private async void LevelUp() {
            CurrentLevel++;
            CurrentExperience = 0;
            _heroSound.PlayLevelUp();
            ShowEffect();
            await UniTask.Delay(Constants.ChangeLevelDelayMs);
            ApplyLevelUpActions();
        }

        private void ApplyLevelUpActions() {
            foreach (var levelUpAction in LevelUpActions) {
                if (!(levelUpAction is ILevelUp action)) return;
                action.LevelUp(CurrentLevel);
            }
        }

        private async void ShowEffect() {
            if (_effect == null) {
                await InstantiateNewEffect();
            }
            else {
                await EnableEffect();
            }
        }

        private async Task EnableEffect() {
            _effect.transform.position = _transform.position;
            _particleSystem.Play();
            await UniTask.Delay(TimeSpan.FromSeconds(_effectTime));
            _particleSystem.Stop();
        }

        private async Task InstantiateNewEffect() {
            _effect = Instantiate(ChangeLevelEffect, _transform.position, Quaternion.Euler(-90, 0, 0), _transform);
            _particleSystem = _effect.GetComponent<ParticleSystem>();
            _effectTime = _particleSystem.main.duration;
            await UniTask.Delay(TimeSpan.FromSeconds(_effectTime));
            _particleSystem.Stop();
        }

        private void UpdateViewModelLevel() {
            if (_hudViewModel == null) return;
            _hudViewModel.Level = _level.ToString();
            _hudViewModel.SetMaxExperience(_experienceToNextLevel);
        }

        private void CalculateToNextLevelExperience() {
            _experienceToNextLevel = _firstLevelExperience;
            for (var i = 0; i < _level - 1; ++i)
                _experienceToNextLevel *= _multiplier;
        }
    }
}