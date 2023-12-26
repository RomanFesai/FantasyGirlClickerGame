using Assets.Scripts.DungeonCrawlerScripts;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private int _damage = 40;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !GameOverWindow.gameOver)
        {
            PlayerStats.instance.HealthBar.value -= _damage;
            PlayerStats.instance.DamageInformation.alpha = 0.4f;
            CameraShake.instance.Play(.15f, .4f);
            AudioManager.instance.Play("Hurt");
            StartCoroutine(PlayerStats.instance.DamageInfoFadeOut());
        }
    }
}
