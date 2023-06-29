using CodeBase.Hero;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.GameData.Installers
{
    [CreateAssetMenu(fileName = "HeroLevelInstaller", menuName = "Installers/HeroLevelInstaller")]
    public class HeroLevelInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private HeroLevel Level;

        public override void InstallBindings() => 
            Container.BindInstance(Level).AsSingle();
    }
}