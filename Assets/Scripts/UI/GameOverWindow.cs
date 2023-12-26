using Assets.Scripts.DungeonCrawlerScripts;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class GameOverWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverWindow;
        [SerializeField] private PlayerMovement playerMovement;
        public static bool gameOver = false;

        [Header("UI")]
        [SerializeField] private GameObject[] gameplayUI;

        private void Update()
        {
            if (PlayerStats.instance.HealthBar.value <= 0)
            {
                gameOver = true;
                PlayerStats.instance.canAttack = false;

                if (gameplayUI != null)
                    foreach (GameObject obj in gameplayUI)
                        obj.SetActive(false);

                playerMovement.enabled = false;
                _gameOverWindow.SetActive(true); 
            }
        }  
    }
}