using Assets.Scripts.DungeonCrawlerScripts.Interactables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Lever : InteractableObjBehaviour
{
    private bool objIsActive = false;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject activateObj;

    private void Start()
    {
        objIsActive = false;
    }

    public void PullLever()
    {
        if (!objIsActive)
        {
            AudioManager.instance.Play("Lever");
            activateObj.SetActive(true);
            objIsActive = true;
            animator.SetBool("isOn",objIsActive);
        }
        else
        {
            AudioManager.instance.Play("Lever");
            activateObj.SetActive(false);
            objIsActive = false;
            animator.SetBool("isOn", objIsActive);
        }
    }
}
