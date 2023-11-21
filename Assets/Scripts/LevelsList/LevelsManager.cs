using System.Collections;
using UnityEngine;
using YG;

namespace Assets.Scripts.LevelsList
{
    public class LevelsManager : MonoBehaviour
    {
        public Level[] level;

        private static LevelsManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public static LevelsManager GetInstance()
        {
            return instance;
        }

        private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

        // Отписываемся от события GetDataEvent в OnDisable
        private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

        private void Start()
        {
            // Проверяем запустился ли плагин
            if (YandexGame.SDKEnabled == true)
            {
                // Если запустился, то выполняем Ваш метод для загрузки
                GetLoad();

                // Если плагин еще не прогрузился, то метод не выполнится в методе Start,
                // но он запустится при вызове события GetDataEvent, после прогрузки плагина
            }
        }

        public void GetLoad()
        {
            // Получаем данные из плагина и делаем с ними что хотим
            // Например, мы хотил записать в компонент UI.Text сколько у игрока монет:
            for (int i = 0; i < level.Length; i++)
            {
                level[i].isCompleted = YandexGame.savesData.isCompleted[i];
                level[i].isUnlocked = YandexGame.savesData.isUnlocked[i];
                level[i].isRewarded = YandexGame.savesData.isRewarded[i];
            }
        }

        public void MySave()
        {
            // Записываем данные в плагин
            // Например, мы хотил сохранить количество монет игрока:
            for(int i = 0; i < level.Length; i++)
            {
                YandexGame.savesData.isCompleted[i] = level[i].isCompleted;
                YandexGame.savesData.isUnlocked[i] = level[i].isUnlocked;
                YandexGame.savesData.isRewarded[i] = level[i].isRewarded;
            }

            // Теперь остаётся сохранить данные
            YandexGame.SaveProgress();
        }
    }
}