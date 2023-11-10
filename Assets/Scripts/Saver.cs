using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Saver : MonoBehaviour
{
    [SerializeField] private LvLProgressBar lvlProgressBar;
    // Подписываемся на событие GetDataEvent в OnEnable
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

    // Ваш метод для загрузки, который будет запускаться в старте
    public void GetLoad()
    {
        // Получаем данные из плагина и делаем с ними что хотим
        // Например, мы хотил записать в компонент UI.Text сколько у игрока монет:
        lvlProgressBar.playerLvl.text = YandexGame.savesData.currentPlayerLvl.ToString();
        lvlProgressBar.fillBar.GetComponent<Image>().fillAmount = YandexGame.savesData.currentfillAmount;
    }

    // Допустим, это Ваш метод для сохранения
    public void MySave()
    {
        // Записываем данные в плагин
        // Например, мы хотил сохранить количество монет игрока:
        //YandexGame.savesData.money = money;

        // Теперь остаётся сохранить данные
        YandexGame.SaveProgress();
    }
}
