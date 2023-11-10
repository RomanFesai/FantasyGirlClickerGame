using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Saver : MonoBehaviour
{
    [SerializeField] private LvLProgressBar lvlProgressBar;
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

    // ��� ����� ��� ��������, ������� ����� ����������� � ������
    public void GetLoad()
    {
        // �������� ������ �� ������� � ������ � ���� ��� �����
        // ��������, �� ����� �������� � ��������� UI.Text ������� � ������ �����:
        lvlProgressBar.playerLvl.text = YandexGame.savesData.currentPlayerLvl.ToString();
        lvlProgressBar.fillBar.GetComponent<Image>().fillAmount = YandexGame.savesData.currentfillAmount;
    }

    // ��������, ��� ��� ����� ��� ����������
    public void MySave()
    {
        // ���������� ������ � ������
        // ��������, �� ����� ��������� ���������� ����� ������:
        //YandexGame.savesData.money = money;

        // ������ ������� ��������� ������
        YandexGame.SaveProgress();
    }
}
