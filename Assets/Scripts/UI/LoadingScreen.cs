using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class LoadingScreen : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            StartCoroutine(WaitForDataLoad());
        }

        private IEnumerator WaitForDataLoad()
        {
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
    }
}