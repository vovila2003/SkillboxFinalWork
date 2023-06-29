using FMODUnity;
using CodeBase.Infrastructure.Factories.Interfaces;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class ResultScreen : MonoBehaviour
    {
        [Required, SerializeField] private Text WinText;
        [Required, SerializeField] private Text LoseText;
        [Required, SerializeField] private StudioEventEmitter WinEmitter;
        [Required, SerializeField] private StudioEventEmitter LoseEmitter;

        private CanvasGroup _canvasGroup;
        private Canvas _canvas;
        private IHeroFactory _heroFactory;
        private IEnemyFactory _enemyFactory;

        private void Awake() {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvas = GetComponent<Canvas>();
        }

        public void Register(IHeroFactory heroFactory) =>
            _heroFactory = heroFactory;
        
        public void Register(IEnemyFactory enemyFactory) =>
            _enemyFactory = enemyFactory;

        [Button]
        public void WinScreen() => Win(true);

        [Button]
        public void LoseScreen() => Win(false);

        private void Win(bool win) {
            WinText.enabled = win;
            LoseText.enabled = !win;
            PlaySound(win);
            DestroyAll();
            Show();
        }

        private void PlaySound(bool win) {
            if (win)
                WinEmitter.Play();
            else
                LoseEmitter.Play();
        }

        private void Show() {
            _canvasGroup.alpha = 0;
            _canvas.enabled = true;
            _canvasGroup.DOFade(1f, 1f)
                        .SetEase(Ease.InSine)
                        .SetLink(gameObject)
                        .SetRelative(true);

        }

        private void DestroyAll() {
            DestroyAllEntities();
            DestroyHero();
            DestroyAllEnemies();
        }

        private void DestroyHero() => 
            _heroFactory.DestroyHero();

        private void DestroyAllEnemies() => 
            _enemyFactory.DestroyAllEnemies();

        private static void DestroyAllEntities() {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            entityManager.DestroyEntity(entityManager.UniversalQuery);
        }
    }
}