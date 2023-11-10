using Assets.Scripts.DungeonCrawlerScripts;
using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class EnemyBehaviour : MonoBehaviour, IDamagable
{
    private protected bool isBattle;
    [SerializeField] private bool isPlayerInRange;
    [SerializeField] private GameObject _healthBar;

    [field: SerializeField] public int maxHealth { get; set; }
    [field: SerializeField] public int currentHealth { get; set; }

    [Header("HitPopUp")]
    [SerializeField] private GameObject hitPopUpObj;
    [SerializeField] private GameObject _mainCamera;

    private Vector3 _transformY;
    private CinemachineVirtualCamera playerCam;
    private Collider playerCollider;

    void Start()
    {
        currentHealth = maxHealth;
        playerCam = PlayerStats.instance.GetComponentInChildren<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        LookAtPlayer();

        if (isPlayerInRange)
        {
            StartBattle(playerCollider);
        }
    }

    public void Die()
    {
        _healthBar.SetActive(false);
        Destroy(gameObject);
    }

    public void getDamage(int playerDamage)
    {
        if (isBattle)
        {
            StartCoroutine(getDamageVisuals());
            currentHealth -= playerDamage;
            if (currentHealth <= 0)
            {
                isBattle = false;
                isPlayerInRange = false;
                PlayerStats.instance.isBattle = false;
                gameObject.GetComponent<SphereCollider>().enabled = false;
                Invoke("Die", 0.2f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInRange = true;
            playerCollider = other;
        }
    }

    private void OnMouseDown()
    {
        if(PlayerStats.instance.StaminaBar.value > 0 && isBattle && PlayerStats.instance.canAttack)
        {
            getDamage(PlayerStats.instance.damage);
            PlayerStats.instance.StaminaBar.value -= 30;
            PlayerStats.instance.staminaRegenTimer = 0f;

        }
    }

    private void LookAtPlayer()
    {
        gameObject.transform.LookAt(PlayerStats.instance.transform.position);
    }

    private void StartBattle(Collider other)
    {
        isBattle = true;
        _transformY = new Vector3(transform.position.x - playerCam.transform.position.x, 0f, transform.position.z - playerCam.transform.position.z);
        _transformY.Normalize();
        playerCam.transform.rotation = Quaternion.Slerp(playerCam.transform.rotation, Quaternion.LookRotation(_transformY), 10f * Time.deltaTime);
        PlayerStats.instance.isBattle = true;

        _healthBar.SetActive(true);
        _healthBar.GetComponent<Slider>().maxValue = maxHealth;
        _healthBar.GetComponent<Slider>().value = currentHealth;
    }
    IEnumerator getDamageVisuals()
    {
        transform.localScale = transform.localScale / 1.2f;
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(0.100317448f, 0.100317448f, 0.100317448f);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 2.0f;
        var obj = Instantiate(hitPopUpObj, mousePos, Quaternion.identity);
        obj.transform.SetParent(_mainCamera.transform, true);
        obj.transform.position = _mainCamera.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
        yield return new WaitForSeconds(.3f);
        Destroy(obj);
    }
}
