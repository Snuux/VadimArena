using Arena.Scripts.Game;

namespace Arena.Scripts.Infrastructure.GameCycle
{
    public class EnemiesCountHandler
    {
        public int AliveCount { get; private set; }
        public int DeadCount { get; private set; }

        public void Add(Enemy enemy)
        {
            AliveCount++;
            
            enemy.Died += OnEnemyDeath;
        }

        private void OnEnemyDeath()
        {
            DeadCount++;
            AliveCount--;
        }
    }
}