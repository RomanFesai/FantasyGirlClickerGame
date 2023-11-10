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
                    _attackTimer = 0f;
                }
            }
        }
    }
}