using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayLocationTheme : MonoBehaviour
    {
        [SerializeField] private string theme_name;
        // Use this for initialization
        void Start()
        {
            AudioManager.instance?.StopAll();
            AudioManager.instance?.Play(theme_name);
        }
    }
}