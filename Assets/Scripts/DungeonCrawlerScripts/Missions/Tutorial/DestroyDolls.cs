using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDolls : MonoBehaviour
{
    [SerializeField] private int needToDestroyDolls;
    public static int dollsDestroyed;
    [SerializeField] private GameObject Exit;

    private void Start()
    {
        dollsDestroyed = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(dollsDestroyed == needToDestroyDolls)
        {
            Exit.SetActive(true);
        }
    }
}
