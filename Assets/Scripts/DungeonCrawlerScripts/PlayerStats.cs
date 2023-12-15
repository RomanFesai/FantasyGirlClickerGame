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

        public CanvasGroup DamageInformation;

        [Header("StatusIcon")]
        public GameObject defaultStatusIcon;
        public Sprite[] statusIcons;

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

            maxHealht = PlayerData.GetInstance().maxHealht;
            maxStamina = PlayerData.GetInstance().maxStamina;
            damage = PlayerData.GetInstance().damage;

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

            if(StaminaBar.value == maxStamina && !GameOverWindow.gameOver)
                canAttack = true;
        }

        private void Update()
        {
            StatusIconChange();
            if (!isBattle)
            {
                GetComponentInChildren<CinemachineVirtualCamera>().transform.localRotation = Quaternion.Slerp(GetComponentInChildren<CinemachineVirtualCamera>().transform.localRotation,
                    Quaternion.Euler(Vector3.zero), 5f * Time.deltaTime);
            }

            if(!isBattle && DamageInformation.alpha > 0)
            {
                StartCoroutine(DamageInfoFadeOut());
            }
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

        public IEnumerator DamageInfoFadeOut()
        {
            if (DamageInformation.alpha > 0)
            {
                while (DamageInformation.alpha > 0)
                {
                    yield return new WaitForSeconds(0.01f);
                    DamageInformation.alpha -= 0.002f;
                }
            }
        }

        private void StatusIconChange()
        {
            switch (HealthBar.value)
            {
                case 0:
                    defaultStatusIcon.GetComponent<Image>().sprite = statusIcons[1];
                    break;
                default:
                    defaultStatusIcon.GetComponent<Image>().sprite = statusIcons[0];
                    break;
            }
        }
    }
}