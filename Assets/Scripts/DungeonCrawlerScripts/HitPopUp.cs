using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DungeonCrawlerScripts
{
    public class HitPopUp : MonoBehaviour
    {
        private float _timer;
        [SerializeField] private float _timeBeforePool = .3f;
        private void OnEnable()
        {
            gameObject.transform.position = Input.mousePosition;
            _timer = 0f;
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _timeBeforePool)
            {
                ObjectPoolManager.ReturnObjectToPool(gameObject);
            }
        }

        public static IEnumerator Create()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 1.5f;
            //var obj = Instantiate(hitPopUpObj, mousePos, Quaternion.identity);
            var obj = ObjectPoolManager.SpawnObject(PlayerStats.instance.hitPopUpObj, mousePos, Quaternion.identity);
            obj.transform.SetParent(PlayerStats.instance._mainCamera.transform, true);
            obj.transform.position = PlayerStats.instance._mainCamera.GetComponent<Camera>().ScreenToWorldPoint(mousePos);
            yield return null;
        }
    }
}