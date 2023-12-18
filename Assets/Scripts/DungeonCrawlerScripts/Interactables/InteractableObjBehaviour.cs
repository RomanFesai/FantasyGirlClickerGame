using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.DungeonCrawlerScripts.Interactables
{
    public class InteractableObjBehaviour : MonoBehaviour
    {
        [SerializeField] protected GameObject InteractionButton;
        [SerializeField] private UnityEvent btnAction;
        protected bool playerInRange = false;

        private void Start()
        {
            playerInRange = false;
            InteractionButton.SetActive(false);
        }

        private void Update()
        {
            if (playerInRange)
            {
                Interaction();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                InteractionButton.SetActive(true);
                InteractionButton.GetComponent<Button>().onClick.AddListener(btnAction.Invoke);
                playerInRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                InteractionButton.SetActive(false);
                InteractionButton.GetComponent<Button>().onClick.RemoveListener(btnAction.Invoke);
                playerInRange = false;
            }
        }

        protected virtual void Interaction()
        {

        }
    }
}