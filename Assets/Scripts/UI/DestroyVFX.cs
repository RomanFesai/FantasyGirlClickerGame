using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class DestroyVFX : MonoBehaviour
    {
        [SerializeField] private float time = 1f;
        // Update is called once per frame
        void Update()
        {
            Destroy(gameObject, time);
        }
    }
}