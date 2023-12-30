using Assets.Scripts;
using Assets.Scripts.DungeonCrawlerScripts;
using Assets.Scripts.LevelsList;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

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
            AudioManager.instance?.PlayOneShot("Pick Up");
            switch (potionType)
            {
                case PotionType.Health:
                    PlayerStats.instance.HealthBar.value += HealthValue;
                    if(YandexGame.EnvironmentData.language == "ru")
                        ShowInfo("Здоровье восстановлено на " + HealthValue + " ед.");
                    else if(YandexGame.EnvironmentData.language == "en")
                        ShowInfo("Health restored by " + HealthValue);
                    break;
                case PotionType.Strength:
                    PlayerStats.instance.damage += damageValue;
                    if (YandexGame.EnvironmentData.language == "ru")
                        ShowInfo("Сила увеличина на " + damageValue + " ед.");
                    else if(YandexGame.EnvironmentData.language == "en")
                        ShowInfo("Strenght increased by " + damageValue);
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
