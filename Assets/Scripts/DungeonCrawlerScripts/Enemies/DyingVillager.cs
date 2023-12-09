using Assets.Scripts.DungeonCrawlerScripts;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingVillager : EnemyBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        currentHealth = 10;
        base.playerCam = PlayerStats.instance.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    public override void Die()
    {
        _healthBar.SetActive(false);
        StartCoroutine(DeathAnimation());
    }

    public override void getDamage(int playerDamage)
    {
        if(!DialogueManager.GetInstance().dialogueIsPlaying)
            base.getDamage(playerDamage);
    }

    private IEnumerator DeathAnimation()
    {
        animator.Play("VillagerDeath");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
