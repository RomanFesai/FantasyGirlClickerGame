using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class PointsPopUp : MonoBehaviour
    {
        private void OnEnable()
        {
            gameObject.transform.position = Input.mousePosition;
        }

        public static IEnumerator Create()
        {
            Vector3 mousePos = Input.mousePosition;
            var obj = ObjectPoolManager.SpawnObject(ClickerManager.instance.pointsPopUpObj, mousePos, Quaternion.identity);
            obj.transform.SetParent(ClickerManager.instance.canvas.transform, true);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = "+" + ClickerManager.instance.add_points.ToString();
            yield return new WaitForSeconds(2f);
            //Destroy(obj);
            ObjectPoolManager.ReturnObjectToPool(obj);
        }
    }
}