using UnityEngine;

namespace Arena.Scripts.Infrastructure.GameCycle
{
    public class GameCycleUI
    {
        public void OnGUI(float currentAliveTime, int aliveEnemiesCount, int deadEnemiesCount,
            int targetDeadEnemiesCount, float health)
        {
            const float scale = 2f;

            var oldMatrix = GUI.matrix;
            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(scale, scale, 1f));

            float x = 10f;
            float y = 10f;
            float lineH = 22f;

            Color oldColor = GUI.color;
            GUI.color = Color.red;
            GUI.Label(new Rect(x, y + lineH * 0, 600f, lineH), $"Your Health: {health:N0}");
            GUI.color = oldColor;

            GUI.Label(new Rect(x, y + lineH * 1, 600f, lineH), $"Alive Time: {currentAliveTime:0.00}");
            GUI.Label(new Rect(x, y + lineH * 2, 600f, lineH), $"Enemies Count: {aliveEnemiesCount}");
            GUI.Label(new Rect(x, y + lineH * 3, 600f, lineH),
                $"Dead Enemies: {deadEnemiesCount} / {targetDeadEnemiesCount}");

            GUI.matrix = oldMatrix;
        }
    }
}