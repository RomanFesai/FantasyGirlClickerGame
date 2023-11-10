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

    // ������������� �� ������� GetDataEvent � OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // ������������ �� ������� GetDataEvent � OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;
    void Start()
    {
        if (YandexGame.SDKEnabled == true)
        {
            // ���� ����������, �� ��������� ��� ����� ��� ��������
            GetLoad();

            // ���� ������ ��� �� �����������, �� ����� �� ���������� � ������ Start,
            // �� �� ���������� ��� ������ ������� GetDataEvent, ����� ��������� �������
        }
    }

    private void GetLoad()
    {
        startLvl = YandexGame.savesData.currentPlayerLvl;
        fillBar.GetComponent<Image>().fillAmount = YandexGame.savesData.currentfillAmount;
        playerLvl.text = startLvl.ToString();
    }
}
