using Assets.Scripts.DungeonCrawlerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private int _damage = 40;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerStats.instance.HealthBar.value -= _damage;
            PlayerStats.instance.DamageInformation.alpha = 0.4f;
            CameraShake.instance.Play(.15f, .4f);
            StartCoroutine(PlayerStats.instance.DamageInfoFadeOut());
        }
    }
}
