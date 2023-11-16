using System.Collections;
using UnityEngine;
using YG;
using UnityEngine.UI;

namespace Assets.Scripts.LevelsList
{
    public class UpdateLevelMap : MonoBehaviour
    {
        [SerializeField] private GameObject[] LevelIcons;
        [SerializeField] private int playerLvl;
        int i = 0;

        private void Update()
        {
            UpdateLvlMap();
        }
        public void UpdateLvlMap()
        {
            if(LevelsManager.GetInstance() == null)
                return;

            if (LevelsManager.GetInstance().level[i].isCompleted == true)
            {
                if (i == LevelsManager.GetInstance().level.Length - 1) return;
                i++;
                if (LevelsManager.GetInstance().level[i].requiredPlayerLevel <= playerLvl)
                {
                    LevelIcons[i].GetComponent<Button>().interactable = true;
                    LevelsManager.GetInstance().level[i].isUnlocked = true;
                    LevelsManager.GetInstance().MySave();
                }/*
                else if (i != LevelsManager.GetInstance().level.Length - 1)
                    i--;*/
            }
        }


        // Подписываемся на событие GetDataEvent в OnEnable
        private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

        // Отписываемся от события GetDataEvent в OnDisable
        private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;
        void Start()
        {
            if (YandexGame.SDKEnabled == true)
            {
                // Если запустился, то выполняем Ваш метод для загрузки
                GetLoad();

                // Если плагин еще не прогрузился, то метод не выполнится в методе Start,
                // но он запустится при вызове события GetDataEvent, после прогрузки плагина
            }
        }

        private void GetLoad()
        {
            playerLvl = YandexGame.savesData.currentPlayerLvl;
        }
    }
}