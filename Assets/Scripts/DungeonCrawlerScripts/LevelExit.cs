using Assets.Scripts;
using Assets.Scripts.LevelsList;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private bool playerInRange;
    [SerializeField] private bool Rewardable;
    [SerializeField] private int levelCompleteNumber;
    [SerializeField] private TextMeshProUGUI LevelCompleteWindowText;
    [SerializeField] private TextMeshProUGUI LevelCompleteWindowButtonText;

    [SerializeField] private GameObject player;

    [Header("UI")]
    [SerializeField] private GameObject[] gameplayUI;

    [Header("Reward")]
    [SerializeField] private GameObject LevelCompleteWindow;
    [SerializeField] private float extra_points;
    [SerializeField] private int bonusBtnTimer;
    [SerializeField] private int add_points;
    // Start is called before the first frame update
    private void Awake()
    {
        playerInRange = false;
    }

    private void Start()
    {
        if (Rewardable && LevelsManager.GetInstance().level[levelCompleteNumber].isRewarded == false)
        {
            LevelCompleteWindowText.text = "Level Complete! You Got A Reward";
            LevelCompleteWindowButtonText.text = "Get Reward and Exit";
        }
        else if (Rewardable == false || LevelsManager.GetInstance().level[levelCompleteNumber].isRewarded == true)
        {
            LevelCompleteWindowText.text = "Level Complete!";
            LevelCompleteWindowButtonText.text = "Exit";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            LevelCompleteWindow.SetActive(true);

           /* if (Rewardable && LevelsManager.GetInstance().level[levelCompleteNumber].isRewarded == false)
            {
                LevelCompleteWindowText.text = "Level Complete! You Got A Reward";
                LevelCompleteWindowButtonText.text = "Get Reward and Exit";
            }
            else if(Rewardable == false || LevelsManager.GetInstance().level[levelCompleteNumber].isRewarded == true)
            {
                LevelCompleteWindowText.text = "Level Complete!";
                LevelCompleteWindowButtonText.text = "Exit";
            }*/

            if (gameplayUI != null)
                foreach (GameObject obj in gameplayUI)
                    obj.SetActive(false);

            if (player != null)
            {
                player.GetComponent<PlayerMovement>().enabled = false;
                //animator.SetBool("PlayerMoving", false);
            }
        }
    }

    public void LevelCompleted()
    {
        if (LevelsManager.GetInstance().level[levelCompleteNumber].isRewarded == false)
        {
            LevelsManager.GetInstance().level[levelCompleteNumber].isCompleted = true;
            GetReward();
        }
        LevelsManager.GetInstance().MySave();
        LevelLoader.GetInstance().LoadLevelByName("LevelSelect");
    }

    private void GetReward()
    {
        PlayerData.GetInstance().add_points += add_points;
        PlayerData.GetInstance().addExtra_points += extra_points;
        PlayerData.GetInstance().BonusBtnTimer += bonusBtnTimer;
        PlayerData.GetInstance().MySave();
        LevelsManager.GetInstance().level[levelCompleteNumber].isRewarded = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
