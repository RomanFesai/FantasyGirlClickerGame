using Assets.Scripts;
using Assets.Scripts.LevelsList;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ClickerManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private LvLProgressBar lvlProgressBar;
    [SerializeField] private float add_points;
    [SerializeField] private int add_Lvl;
    [SerializeField] private float points_divider;

    [Header("BonusButtonsSettings")]
    [SerializeField] private float addExtra_points;
    [SerializeField] private int default_timer = 60;
    [SerializeField] private TextMeshProUGUI addExtra_pointsText;

    [Header("PointsPopUp")]
    [SerializeField] private GameObject pointsPopUpObj;
    [SerializeField] private TextMeshProUGUI pointsPopUp;
    [SerializeField] private GameObject canvas;

    [Header("Timers")]
    [SerializeField] private TextMeshProUGUI timerText1;
    [SerializeField] private TextMeshProUGUI timerText2;

    [Header("VisualEffects")]
    [SerializeField] private GameObject LevelUpTextObj;
    [SerializeField] private GameObject LevelUpArrowsObj;

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

    private void Update()
    {
        if (lvlProgressBar.fillBar.fillAmount == 1)
        {
            lvlProgressBar.startLvl += add_Lvl;
            lvlProgressBar.playerLvl.text = lvlProgressBar.startLvl.ToString();
            lvlProgressBar.fillBar.fillAmount = 0;
            points_divider = points_divider * 2;
            PlayerData.GetInstance().maxHealht += 10;
            PlayerData.GetInstance().maxStamina += 10;
            PlayerData.GetInstance().damage += 1;
            PlayerData.GetInstance().MySave();
            StartCoroutine(LevelUp());

#if UNITY_WEBGL
            YandexGame.savesData.currentPlayerLvl = lvlProgressBar.startLvl;
            YandexGame.savesData.currentPointsDivider = points_divider;
#endif
        }

        addExtra_pointsText.text = "Клик +" + addExtra_points + " на время";
    }

    public void ClickDown()
    {
        lvlProgressBar.fillBar.fillAmount += add_points / points_divider;

#if UNITY_WEBGL
        YandexGame.savesData.currentfillAmount = lvlProgressBar.fillBar.fillAmount;
#endif

#if UNITY_WEBGL
        MySave();
        LevelsManager.GetInstance().MySave();
#endif
        StartCoroutine(pointsPopUpAnim());
    }

    private IEnumerator pointsPopUpAnim()
    {
        Vector3 mousePos = Input.mousePosition;
        var obj = Instantiate(pointsPopUpObj, mousePos, transform.rotation);
        obj.transform.SetParent(canvas.transform, true);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = "+" + add_points.ToString();
        yield return new WaitForSeconds(2f);
        Destroy(obj);
    }

    public void TempAutoClick(Button button)
    {
        StartCoroutine(AutoClick(button, timerText1));
    }

    public void TempPlusClick(Button button)
    {
        StartCoroutine(PlusClick(button, timerText2));
    }
    private IEnumerator AutoClick(Button btn, TextMeshProUGUI timerText)
    {
        int timer = default_timer;
        timerText.enabled = true;
        while(timer > 0)
        {
            timerText.text = timer.ToString();
            btn.interactable = false;
            lvlProgressBar.fillBar.fillAmount += add_points / points_divider;

#if UNITY_WEBGL
            YandexGame.savesData.currentfillAmount = lvlProgressBar.fillBar.fillAmount;
#endif

/*#if UNITY_WEBGL
            DataPersistenceManager.instance.SaveGame();
#endif*/
            timer--;
            yield return new WaitForSeconds(1f);
        }
        timerText.enabled = false;
        btn.interactable = true;
    }

    private IEnumerator PlusClick(Button btn, TextMeshProUGUI timerText)
    {
        int timer = default_timer;
        timerText.enabled = true;
        float default_add_points = add_points;
        add_points += addExtra_points;
        while (timer > 0)
        {
            timerText.text = timer.ToString();
            btn.interactable = false;
            //add_points = addExtra_points;

#if UNITY_WEBGL
            YandexGame.savesData.currentfillAmount = lvlProgressBar.fillBar.fillAmount;
#endif

/*#if UNITY_WEBGL
            DataPersistenceManager.instance.SaveGame();
#endif*/
            timer--;
            yield return new WaitForSeconds(1f);
        }
        add_points = default_add_points;
        timerText.enabled = false;
        btn.interactable = true;
    }

    public void EraseGameBtn()
    {
        lvlProgressBar.fillBar.fillAmount = 0;
        lvlProgressBar.startLvl = 0;
        lvlProgressBar.playerLvl.text = lvlProgressBar.startLvl.ToString();
        points_divider = 10;

        for (int i = 1; i < LevelsManager.GetInstance().level.Length; i++)
        {
            LevelsManager.GetInstance().level[i].isCompleted = false;
            LevelsManager.GetInstance().level[i].isUnlocked = false;
        }

        YandexGame.ResetSaveProgress();
    }

    private IEnumerator LevelUp()
    {
        if (LevelUpArrowsObj != null && LevelUpTextObj != null)
        {
            LevelUpArrowsObj.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            LevelUpArrowsObj.SetActive(false);
            LevelUpTextObj.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            LevelUpTextObj.SetActive(false);
        }
    }

    /* private IEnumerator loading()
     {
         yield return new WaitForSeconds(1f);
         if (YandexGame.savesData.currentPointsDivider != 0)
             points_divider = YandexGame.savesData.currentPointsDivider;
     }*/
    public void GetLoad()
    {
        // Получаем данные из плагина и делаем с ними что хотим
        // Например, мы хотил записать в компонент UI.Text сколько у игрока монет:
        lvlProgressBar.playerLvl.text = YandexGame.savesData.currentPlayerLvl.ToString();
        lvlProgressBar.fillBar.GetComponent<Image>().fillAmount = YandexGame.savesData.currentfillAmount;
        if (YandexGame.savesData.currentPointsDivider != 0)
            points_divider = YandexGame.savesData.currentPointsDivider;

        add_points = PlayerData.GetInstance().add_points;
        addExtra_points = PlayerData.GetInstance().addExtra_points;
        default_timer = PlayerData.GetInstance().BonusBtnTimer;
    }

    public void MySave()
    {
        // Записываем данные в плагин
        // Например, мы хотил сохранить количество монет игрока:
        YandexGame.savesData.currentfillAmount = lvlProgressBar.fillBar.fillAmount;
        YandexGame.savesData.currentPlayerLvl = lvlProgressBar.startLvl;
        YandexGame.savesData.currentPointsDivider = points_divider;

        // Теперь остаётся сохранить данные
        YandexGame.SaveProgress();
    }
}
