using CodeBase.Hero;
using CodeBase.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class StartSceneConstructor : MonoBehaviour
    {
        [Required, SerializeField] private HeroModel HeroModel;

        private void Awake() {
            var model = Instantiate(Bootstrapper.Instance.Game.Prefabs.StartSceneCanvas).GetComponent<InitialViewModel>();
            HeroModel.Register(model);
        }
    }
}