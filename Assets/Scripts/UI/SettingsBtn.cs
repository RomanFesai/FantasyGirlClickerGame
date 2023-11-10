﻿using System.Collections;
using UnityEngine;
using YG;

namespace Assets.Scripts.UI
{
    public class SettingsBtn : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Animator saveNotification;
        [SerializeField] private bool isSettingsOn;

        private void Start()
        {
            isSettingsOn = false;
        }

        public void SettingsButton()
        {
            if (isSettingsOn == false)
            {
                isSettingsOn = true;
                _animator.SetBool("btnState", true);
            }
            else if (isSettingsOn == true)
            {
                isSettingsOn = false;
                _animator.SetBool("btnState", false);
            }

        }

        public void SaveGameBtn()
        {
            saveNotification.Play("GameSavedNotification");
            YandexGame.SaveProgress();
        }

        public void EraseGameBtn()
        {
            YandexGame.ResetSaveProgress();
        }
    }
}