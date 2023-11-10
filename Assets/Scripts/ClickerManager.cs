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

    // ������������� �� ������� GetDataEvent � OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // ������������ �� ������� GetDataEvent � OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

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
    }

    private void Update()
    {
        if (lvlProgressBar.fillBar.fillAmount == 1)
        {
            lvlProgressBar.startLvl += add_Lvl;
            lvlProgressBar.playerLvl.text = lvlProgressBar.startLvl.ToString();
            lvlProgressBar.fillBar.fillAmount = 0;
            points_divider = points_divider * 2;
            StartCoroutine(LevelUp());

#if UNITY_WEBGL
            YandexGame.savesData.currentPlayerLvl = lvlProgressBar.startLvl;
            YandexGame.savesData.currentPointsDivider = points_divider;
#endif
        }
    }

    public void ClickDown()
    {
        lvlProgressBar.fillBar.fillAmount += add_points / points_divider;

#if UNITY_WEBGL
        YandexGame.savesData.currentfillAmount = lvlProgressBar.fillBar.fillAmount;
#endif

#if UNITY_WEBGL
        MySave();
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
        int timer = 60;
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
        int timer = 60;
        timerText.enabled = true;
        while (timer > 0)
        {
            timerText.text = timer.ToString();
            btn.interactable = false;
            add_points = 5;

#if UNITY_WEBGL
            YandexGame.savesData.currentfillAmount = lvlProgressBar.fillBar.fillAmount;
#endif

/*#if UNITY_WEBGL
            DataPersistenceManager.instance.SaveGame();
#endif*/
            timer--;
            yield return new WaitForSeconds(1f);
        }
        add_points = 1;
        timerText.enabled = false;
        btn.interactable = true;
    }

    public void EraseGameBtn()
    {
        lvlProgressBar.fillBar.fillAmount = 0;
        lvlProgressBar.startLvl = 0;
        lvlProgressBar.playerLvl.text = lvlProgressBar.startLvl.ToString();
        points_divider = 10;
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
        // �������� ������ �� ������� � ������ � ���� ��� �����
        // ��������, �� ����� �������� � ��������� UI.Text ������� � ������ �����:
        lvlProgressBar.playerLvl.text = YandexGame.savesData.currentPlayerLvl.ToString();
        lvlProgressBar.fillBar.GetComponent<Image>().fillAmount = YandexGame.savesData.currentfillAmount;
        if (YandexGame.savesData.currentPointsDivider != 0)
            points_divider = YandexGame.savesData.currentPointsDivider;
    }

    public void MySave()
    {
        // ���������� ������ � ������
        // ��������, �� ����� ��������� ���������� ����� ������:
        YandexGame.savesData.currentfillAmount = lvlProgressBar.fillBar.fillAmount;
        YandexGame.savesData.currentPlayerLvl = lvlProgressBar.startLvl;
        YandexGame.savesData.currentPointsDivider = points_divider;

        // ������ ������� ��������� ������
        YandexGame.SaveProgress();
    }
}
