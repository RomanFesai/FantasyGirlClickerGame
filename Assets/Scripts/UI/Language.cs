using System.Collections;
using TMPro;
using UnityEngine;
using YG;

namespace Assets.Scripts.UI
{
    //Класс-костыль, либо переделать, либо НИКОМУ не показывать
    public class Language : MonoBehaviour
    {
        public static string currentLanguage;
        public static Language instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                //DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            currentLanguage = YandexGame.EnvironmentData.language;
        }

        public void setLanguage(string lang)
        {
            currentLanguage = lang;
            YandexGame.EnvironmentData.language = lang;
        }

        /* [SerializeField] private string textRU;
         [SerializeField] private string textEN;

         private void Update()
         {
             if(YandexGame.EnvironmentData.language == "ru")
             {
                 gameObject.GetComponent<TextMeshProUGUI>().text = textRU;
             }
             else if (YandexGame.EnvironmentData.language == "en")
             {
                 gameObject.GetComponent<TextMeshProUGUI>().text = textEN;
             }
         }*/
    }
}