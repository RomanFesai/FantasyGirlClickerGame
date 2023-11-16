using UnityEngine;

public class DialogueOnStart : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    //private bool playerInRange;

    private void Update()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            //TimelinePlayer.GetInstance().PauseTimeline();
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            Destroy(gameObject);
        }
    }
}