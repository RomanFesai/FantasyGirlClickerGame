using Assets.Scripts.LevelsList;
using System.Collections;
using UnityEngine;
using YG;

namespace Assets.Scripts
{
    public class PlayerData : MonoBehaviour
    {
        [Header("Dungeon Crawler Data")]
        public int maxHealht;
        public int damage;
        public int maxStamina;

        [Header("Clicker Data")]
        public float add_points;
        public float addExtra_points;
        public int BonusBtnTimer;

        private static PlayerData instance;

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

        public static PlayerData GetInstance()
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

        public void MySave()
        {
            YandexGame.savesData.maxHealht = maxHealht;
            YandexGame.savesData.maxStamina = maxStamina;
            YandexGame.savesData.damage = damage;

            YandexGame.savesData.add_points = add_points;
            YandexGame.savesData.addExtra_points = addExtra_points;
            YandexGame.savesData.BonusBtnTimer = BonusBtnTimer;

            YandexGame.SaveProgress();
        }

        public void GetLoad()
        {
            maxHealht = YandexGame.savesData.maxHealht;
            maxStamina = YandexGame.savesData.maxStamina;
            damage = YandexGame.savesData.damage;

            add_points = YandexGame.savesData.add_points;
            addExtra_points = YandexGame.savesData.addExtra_points;
            BonusBtnTimer = YandexGame.savesData.BonusBtnTimer;
        }
    }
}