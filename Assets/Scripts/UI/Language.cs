using System.Collections;
using TMPro;
using UnityEngine;
using YG;

namespace Assets.Scripts.UI
{
    //Класс-костыль, либо переделать, либо НИКОМУ не показывать
    public class Language : MonoBehaviour
    {
        /* public static string currentLanguage;
         public static Language instance;

         [SerializeField] private TextMeshProUGUI LoadingText;
         [SerializeField] private TextMeshProUGUI AutoClickText;
         [SerializeField] private TextMeshProUGUI BackButtonText;
         [SerializeField] private TextMeshProUGUI TutorialText;
         [SerializeField] private TextMeshProUGUI SkipDialogueBtnText;
         [SerializeField] private TextMeshProUGUI GameOverWindowText1;
         [SerializeField] private TextMeshProUGUI GameOverWindowText2;
         [SerializeField] private TextMeshProUGUI GameOverWindowText3;
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

         private void Update()
         {
             if (LoadingText != null)
             {
                 if (currentLanguage == "ru")
                 {
                     LoadingText.text = "ЗАГРУЗКА...";
                 }
                 else if (currentLanguage == "en")
                 {
                     LoadingText.text = "LOADING...";
                 }
             }

             if (AutoClickText != null)
             {
                 if (currentLanguage == "ru")
                 {
                     AutoClickText.text = "Авто-клик на время";
                 }
                 else if (currentLanguage == "en")
                 {
                     AutoClickText.text = "Auto-Click for a while";
                 }
             }

             if(BackButtonText != null)
             {
                 if (currentLanguage == "ru")
                 {
                     BackButtonText.text = "НАЗАД";
                 }
                 else if (currentLanguage == "en")
                 {
                     BackButtonText.text = "BACK";
                 }
             }

             if (TutorialText != null)
             {
                 if (currentLanguage == "ru")
                 {
                     TutorialText.text = "ОБУЧЕНИЕ";
                 }
                 else if (currentLanguage == "en")
                 {
                     TutorialText.text = "TUTORIAL";
                 }
             }
         }

         public void setLanguage(string lang)
         {
             currentLanguage = lang;
         }*/

        [SerializeField] private string textRU;
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
        }
    }
}