using Assets.Scripts;
using Assets.Scripts.LevelsList;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private bool playerInRange;
    [SerializeField] private int levelCompleteNumber;
    // Start is called before the first frame update
    private void Awake()
    {
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            LevelsManager.GetInstance().level[levelCompleteNumber].isCompleted = true;
            LevelsManager.GetInstance().MySave();
            LevelLoader.GetInstance().LoadLevelByName("LevelSelect");
        }
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
