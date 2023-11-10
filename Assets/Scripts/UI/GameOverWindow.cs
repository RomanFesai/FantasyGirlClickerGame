using Assets.Scripts.DungeonCrawlerScripts;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverWindow;
        public static bool gameOver = false;

        private void Update()
        {
            if (PlayerStats.instance.HealthBar.value <= 0)
            {
                gameOver = true;
                PlayerStats.instance.canAttack = false;
                _gameOverWindow.SetActive(true); 
            }
        }       
    }
}