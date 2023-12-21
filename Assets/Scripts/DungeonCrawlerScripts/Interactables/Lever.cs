using Assets.Scripts.DungeonCrawlerScripts.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Lever : InteractableObjBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject[] activateObj;

    private void Start()
    {
        animator.SetBool("isOn", activateObj[0].activeInHierarchy);
    }
    public void PullLever()
    {
        foreach (var obj in activateObj)
        {

            if (obj.activeInHierarchy == false)
            {
                AudioManager.instance.Play("Lever");
                obj.SetActive(true);
                animator.SetBool("isOn", activateObj[0].activeInHierarchy);
            }
            else
            {
                AudioManager.instance.Play("Lever");
                obj.SetActive(false);
                animator.SetBool("isOn", activateObj[0].activeInHierarchy);
            }
        }
    }
}
