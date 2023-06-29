using CodeBase.Hero.ComponentData;
using CodeBase.Infrastructure;
using CodeBase.UI;
using Unity.Entities;

namespace CodeBase.Common.Systems
{
    public partial class GamePauseSystem : SystemBase
    {
        private PauseMenu _pauseMenu;
        
        protected override void OnUpdate() {
            Entities
                .WithChangeFilter<InputData>()
                .ForEach(
                    (ref InputData inputData) => {
                        if (!CheckPauseMenu()) return;
                        if (inputData.Exit > Constants.Threshold)
                            _pauseMenu.Show();
                    }
                )
                .WithoutBurst()
                .Run();
        }

        private bool CheckPauseMenu() {
            if (_pauseMenu == null)
                _pauseMenu = Bootstrapper.Instance.Game.Pause;
            return _pauseMenu != null;
        }
    }
}