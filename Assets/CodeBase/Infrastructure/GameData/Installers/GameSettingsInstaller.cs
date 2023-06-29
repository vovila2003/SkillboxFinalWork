using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.GameData.Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private Settings _settings;

        public override void InstallBindings() => 
            Container.BindInstance(_settings).AsSingle();
    }
}