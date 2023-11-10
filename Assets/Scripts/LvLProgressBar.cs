using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LvLProgressBar : MonoBehaviour
{
    public int startLvl;
    public Image fillBar;
    public TextMeshProUGUI playerLvl;

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
        startLvl = YandexGame.savesData.currentPlayerLvl;
        fillBar.GetComponent<Image>().fillAmount = YandexGame.savesData.currentfillAmount;
        playerLvl.text = startLvl.ToString();
    }
}
