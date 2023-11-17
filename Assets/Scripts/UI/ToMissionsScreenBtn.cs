using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMissionsScreenBtn : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void LoadMissionScreenBtn()
    {
        if (animator != null && LevelLoader.GetInstance() != null)
        {
            animator.Play("ToMissionBtn");
            LevelLoader.GetInstance().LoadLevelByName("LevelSelect");
        }
    }

    public void ReturnToMainScreen()
    {
        LevelLoader.GetInstance().LoadLevelByName("ClickerScene");
    }
}
