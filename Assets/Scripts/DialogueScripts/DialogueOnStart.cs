using UnityEngine;
using YG;
using Assets.Scripts.UI;

public class DialogueOnStart : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private TextAsset inkJSONEng;
    //private bool playerInRange;

    private void Update()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (YandexGame.EnvironmentData.language == "ru")
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
            else if (YandexGame.EnvironmentData.language == "en")
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSONEng);
            }
            //TimelinePlayer.GetInstance().PauseTimeline();
            Destroy(gameObject);
        }
    }
}