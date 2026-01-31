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
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Process of services registration in gameplay scene");

            container.RegisterAsSingle(c => CreateRandomSymbolsSequenceService(c, args));
            container.RegisterAsSingle(CreateGameCycle);
            container.RegisterAsSingle(CreateGameFinishStateHandler);
            container.RegisterAsSingle(CreateInputSequenceHandler);
            container.RegisterAsSingle(CreateSequenceCheckService);
        }

        private static RandomSymbolsSequenceGenerationService CreateRandomSymbolsSequenceService(DIContainer c, GameplayInputArgs args)
        {
            return new RandomSymbolsSequenceGenerationService(args.Length, args.Symbols);
        }

        private static GameCycleHandler CreateGameCycle(DIContainer c)
        {
            return new GameCycleHandler(
                c.Resolve<RandomSymbolsSequenceGenerationService>(),
                c.Resolve<GameFinishStateHandler>(),
                c.Resolve<InputSequenceHandler>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<SequenceCheckService>()
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
        
        private static SequenceCheckService CreateSequenceCheckService(DIContainer c)
        {
            return new SequenceCheckService();
        }
    }
}