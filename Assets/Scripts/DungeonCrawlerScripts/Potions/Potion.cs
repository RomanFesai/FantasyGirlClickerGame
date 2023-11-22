using Assets.Scripts;
using Assets.Scripts.DungeonCrawlerScripts;
using Assets.Scripts.LevelsList;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PotionType
{
    Health,
    Strength
}
public class Potion : MonoBehaviour
{
    [SerializeField] private PotionType potionType;
    [SerializeField] private int HealthValue;
    [SerializeField] private int damageValue;
    [SerializeField] private GameObject InfoText;
    [SerializeField] private Animator PickUpInfo;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (potionType)
            {
                case PotionType.Health:
                    PlayerStats.instance.HealthBar.value += HealthValue;
                    ShowInfo("Здоровье восстановлено на " + HealthValue + " ед.");
                    break;
                case PotionType.Strength:
                    PlayerStats.instance.damage += damageValue;
                    ShowInfo("Сила увеличина на " + damageValue + " ед.");
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }

    private void ShowInfo(string Info)
    {
        //InfoText.SetActive(true);
        InfoText.GetComponent<TextMeshProUGUI>().text = Info;
        PickUpInfo.Play("PickUpInfo_anim");
    }
}
