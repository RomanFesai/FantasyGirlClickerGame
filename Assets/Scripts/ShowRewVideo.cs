using System.Collections;
using UnityEngine;
using YG;

namespace Assets.Scripts
{
    public class ShowRewVideo : MonoBehaviour
    {
        private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;
        private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;
        public void Rewarded(int id)
        {

        }

        public void ShowAd(int id)
        {
            YandexGame.RewVideoShow(id);
        }
    }
}