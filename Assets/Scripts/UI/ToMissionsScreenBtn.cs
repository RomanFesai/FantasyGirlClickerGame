using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMissionsScreenBtn : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void LoadMissionScreenBtn()
    {
        StartCoroutine(LoadMissionScreen());
    }

    public void ReturnToMainScreen()
    {
        SceneManager.LoadScene("ClickerScene");
    } 

    private IEnumerator LoadMissionScreen()
    {
        if (animator != null)
        {
            animator.Play("ToMissionBtn");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("LevelSelect");
        }
    } 
}
