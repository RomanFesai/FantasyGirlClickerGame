using Assets.Scripts.UI;
using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DungeonCrawlerScripts
{
    public class PlayerStats : MonoBehaviour
    {
        public int maxHealht;
        public int currentHealht;
        public int damage;
        public int maxStamina;
        public int currentStamina;

        public float staminaRegenTimer = 0;

        public bool isBattle;
        public bool canAttack;

        public Slider HealthBar;
        public Slider StaminaBar;

        public static PlayerStats instance { get; private set; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogWarning("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
                Destroy(this.gameObject);
            }

            GameOverWindow.gameOver = false;
            canAttack = true;

            currentHealht = maxHealht;
            currentStamina = maxStamina;

            HealthBar.maxValue = maxHealht;
            HealthBar.value = currentHealht;

            StaminaBar.maxValue = maxStamina;
            StaminaBar.value = currentStamina;
        }

        private void FixedUpdate()
        {
            StaminaRefill();

            if (!isBattle)
            {
                StaminaBar.value += 5;
            }

            /*if(StaminaBar.value <= 0)
                canAttack = false;
            else if(!GameOverWindow.gameOver)
                canAttack = true;*/
            if(StaminaBar.value == maxStamina && !GameOverWindow.gameOver)
                canAttack = true;
        }

        private void Update()
        {
            if(!isBattle)
                GetComponentInChildren<CinemachineVirtualCamera>().transform.localRotation = Quaternion.Slerp(GetComponentInChildren<CinemachineVirtualCamera>().transform.localRotation, 
                    Quaternion.Euler(Vector3.zero), 5f * Time.deltaTime);
        }

       public void StaminaRefill()
        {
            staminaRegenTimer += Time.deltaTime;
            if (StaminaBar.value != maxStamina && staminaRegenTimer > .5f)
            {
                if(StaminaBar.value <= 0)
                {
                    canAttack = false;
                    if(StaminaBar.value != maxStamina)
                    {
                        StaminaBar.value += 1;
                    }
                }
                StaminaBar.value += 1;
            }
        }
    }
}