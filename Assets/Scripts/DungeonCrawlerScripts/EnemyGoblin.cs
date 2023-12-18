using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DungeonCrawlerScripts
{
    public class EnemyGoblin : EnemyBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _attackPeriodicity = 5f;
        private float _attackTimer = 0f;

        private void FixedUpdate()
        {
            Attack();
        }

        private void Attack()
        {
            if (isBattle)
            {
                _attackTimer += Time.deltaTime;
                if (_attackTimer > _attackPeriodicity)
                {
                    PlayerStats.instance.HealthBar.value -= _damage;
                    PlayerStats.instance.DamageInformation.alpha = 0.4f;
                    CameraShake.instance.Play(.15f, .4f);
                    AudioManager.instance.Play("Hurt");
                    StartCoroutine(PlayerStats.instance.DamageInfoFadeOut());
                    _attackTimer = 0f;
                }
            }
        }
    }
}