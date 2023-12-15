using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DungeonCrawlerScripts
{
    public class TeleportArchEnter : MonoBehaviour
    {
        [SerializeField] private GameObject Exit;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                PlayerMovement.targetGridPos = Exit.transform.position;
                other.gameObject.transform.position = Exit.transform.position;
            }
        }


    }
}