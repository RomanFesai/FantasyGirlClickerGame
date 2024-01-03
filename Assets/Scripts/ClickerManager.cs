using Assets.Scripts;
using Assets.Scripts.LevelsList;
using Assets.Scripts.UI;
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
    [SerializeField] public float add_points;
    [SerializeField] private int add_Lvl;

    [Header("BonusButtonsSettings")]
    [SerializeField] private float addExtra_points;
    [SerializeField] private int default_timer = 60;
    [SerializeField] private TextMeshProUGUI addExtra_pointsText;

    [Header("PointsPopUp")]
    [SerializeField] public GameObject pointsPopUpObj;
    [SerializeField] private TextMeshProUGUI pointsPopUp;
    [SerializeField] public GameObject canvas;

    [Header("Timers")]
    [SerializeField] private TextMeshProUGUI timerText1;
    [SerializeField] private TextMeshProUGUI timerText2;

    [Header("VisualEffects")]
    [SerializeField] private GameObject LevelUpTextObj;
    [SerializeField] private GameObject LevelUpArrowsObj;

    public Vector3 mousePos;
    public static ClickerManager instance;

    // ������������� �� ������� GetDataEvent � OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // ������������ �� ������� GetDataEvent � OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this; 
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // ��������� ���������� �� ������
        if (YandexGame.SDKEnabled == true)
        {
            // ���� ����������, �� ��������� ��� ����� ��� ��������
            GetLoad();

            // ���� ������ ��� �� �����������, �� ����� �� ���������� � ������ Start,
            // �� �� ���������� ��� ������ ������� GetDataEvent, ����� ��������� �������
        }

        AudioManager.instance?.StopAll();
        AudioManager.instance?.Play("Clicker Theme");
    }

    private void Update()
    {
        lvlProgressBar.fillBar.fillAmount = lvlProgressBar.currentFill / lvlProgressBar.maxFillToLvlUp;
        if (lvlProgressBar.currentFill >= lvlProgressBar.maxFillToLvlUp)
        {
            lvlProgressBar.startLvl += add_Lvl;
            lvlProgressBar.playerLvl.text = lvlProgressBar.startLvl.ToString();
            lvlProgressBar.currentFill = 0;
            lvlProgressBar.maxFillToLvlUp += lvlProgressBar.addMaxPointsToLvlUp;
            PlayerData.GetInstance().maxHealht += 10;
            PlayerData.GetInstance().maxStamina += 10;
            PlayerData.GetInstance().damage += 1;
            PlayerData.GetInstance().MySave();
            StartCoroutine(LevelUp());

#if UNITY_WEBGL
            YandexGame.savesData.currentPlayerLvl = lvlProgressBar.startLvl;
            YandexGame.savesData.PointsToLvlUp = lvlProgressBar.maxFillToLvlUp;
#endif
        }

        if(YandexGame.EnvironmentData.language == "ru")
        {
            addExtra_pointsText.text = "���� +" + addExtra_points + " �� �����";
        }
        else if(YandexGame.EnvironmentData.language == "en")
        {
            addExtra_pointsText.text = "Click +" + addExtra_points + " for a while";
        }
    }

    public void ClickDown()
    {
        AudioManager.instance?.Play("Click");
        lvlProgressBar.currentFill += add_points;

#if UNITY_WEBGL
        YandexGame.savesData.currentfillAmount = lvlProgressBar.currentFill;
#endif

#if UNITY_WEBGL
        MySave();
        LevelsManager.GetInstance().MySave();
#endif
        StartCoroutine(PointsPopUp.Create());
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
            lvlProgressBar.currentFill += add_points;

#if UNITY_WEBGL
            YandexGame.savesData.currentfillAmount = lvlProgressBar.currentFill;
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
            YandexGame.savesData.currentfillAmount = lvlProgressBar.currentFill;
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
        lvlProgressBar.currentFill = 0;
        lvlProgressBar.startLvl = 0;
        lvlProgressBar.playerLvl.text = lvlProgressBar.startLvl.ToString();
        lvlProgressBar.maxFillToLvlUp = 50;

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
            AudioManager.instance?.Play("Level Up");
            AudioManager.instance?.Play("Level Up(2)");
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
        // �������� ������ �� ������� � ������ � ���� ��� �����
        // ��������, �� ����� �������� � ��������� UI.Text ������� � ������ �����:
        lvlProgressBar.playerLvl.text = YandexGame.savesData.currentPlayerLvl.ToString();
        lvlProgressBar.currentFill = YandexGame.savesData.currentfillAmount;
        lvlProgressBar.maxFillToLvlUp = YandexGame.savesData.PointsToLvlUp;

        add_points = PlayerData.GetInstance().add_points;
        addExtra_points = PlayerData.GetInstance().addExtra_points;
        default_timer = PlayerData.GetInstance().BonusBtnTimer;
    }

    public void MySave()
    {
        // ���������� ������ � ������
        // ��������, �� ����� ��������� ���������� ����� ������:
        YandexGame.savesData.currentfillAmount = lvlProgressBar.currentFill;
        YandexGame.savesData.currentPlayerLvl = lvlProgressBar.startLvl;
        YandexGame.savesData.PointsToLvlUp = lvlProgressBar.maxFillToLvlUp;

        // ������ ������� ��������� ������
        YandexGame.SaveProgress();
    }
}
