using Ink.Parsed;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static List<PooledObjectInfo> objectPools = new List<PooledObjectInfo>();

        private void Awake()
        {
            objectPools.Clear();
        }

        public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 mousePos, Quaternion rotation)
        {
            PooledObjectInfo pool = objectPools.Find(p => p.lookupString == objectToSpawn.name);

            if (pool == null)
            {
                pool = new PooledObjectInfo
                {
                    lookupString = objectToSpawn.name,
                };

                objectPools.Add(pool);
            }

            GameObject go = pool.inactiveObjects.FirstOrDefault();
            if (go == null)
            {
                go = Instantiate(objectToSpawn, mousePos, rotation);
            }
            else
            {
                pool.inactiveObjects.Remove(go);
                go.SetActive(true);
            }

            return go;
        }

        public static void ReturnObjectToPool(GameObject obj)
        {
            string goName = obj.name.Substring(0, obj.name.Length - 7);

            PooledObjectInfo pool = objectPools.Find(p => p.lookupString == goName);

            if (pool != null)
            {
                obj.SetActive(false);
                pool.inactiveObjects.Add(obj);
            }
        }
    }
}

public class PooledObjectInfo
{
    public string lookupString;
    public List<GameObject> inactiveObjects = new List<GameObject>();
}