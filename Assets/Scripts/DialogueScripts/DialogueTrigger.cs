using Cinemachine;
using System.Collections;
using UnityEngine;
using YG;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TextAsset inkJSONEng;

    private bool playerInRange;

    private void Awake()
    {
        playerInRange = false;
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {

            if (YandexGame.EnvironmentData.language == "ru")
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
            else if (YandexGame.EnvironmentData.language == "en")
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSONEng);
            }
            Destroy(gameObject);
            
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