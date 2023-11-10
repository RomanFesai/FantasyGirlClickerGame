
namespace Assets.Scripts.DungeonCrawlerScripts
{
    interface IDamagable
    {
        int maxHealth { get; set; }
        int currentHealth { get; set; }
        void getDamage(int playerDamage);
        void Die();
    }
}
