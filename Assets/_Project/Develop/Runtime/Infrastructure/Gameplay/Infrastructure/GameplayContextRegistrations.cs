using _Project.Develop.Runtime.Infrastructure.AssetManagment;
using _Project.Develop.Runtime.Infrastructure.ConfigsManagment;
using _Project.Develop.Runtime.Infrastructure.CoroutineManagment;
using _Project.Develop.Runtime.Infrastructure.DI;
using _Project.Develop.Runtime.Utilities.SceneManagment;
using Assets._Project.Develop.Runtime.Infrastructure.Gameplay;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Develop.Runtime.Infrastructure.Gameplay.Infrastructure
{
    public class GameplayContextRegistrations
    {
        private static GameplayInputArgs _args;

        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            _args = args;

            Debug.Log("Process of services registration in gameplay scene");

            container.RegisterAsSingle(CreateRandomSymbolsSequenceService);
            container.RegisterAsSingle(CreateGameCycle);
            container.RegisterAsSingle(CreateGameFinishStateHandler);
            container.RegisterAsSingle(CreateInputSequenceHandler);
        }

        private static RandomSymbolsSequenceService CreateRandomSymbolsSequenceService(DIContainer c)
        {
            return new RandomSymbolsSequenceService(_args.Length, _args.Symbols);
        }

        private static GameCycleHandler CreateGameCycle(DIContainer c)
        {
            return new GameCycleHandler(
                c.Resolve<RandomSymbolsSequenceService>(),
                c.Resolve<GameFinishStateHandler>(),
                c.Resolve<InputSequenceHandler>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<SceneSwitcherService>()
                );
        }

        private static GameFinishStateHandler CreateGameFinishStateHandler(DIContainer c)
        {
            return new GameFinishStateHandler();
        }

        private static InputSequenceHandler CreateInputSequenceHandler(DIContainer c)
        {
            return new InputSequenceHandler();
        }
    }
}