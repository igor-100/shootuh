using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool = 20;
    private List<GameObject> pooledObjects;

    private void Awake()
    {
        SharedInstance = this;
    }

    // TODO: projectile: dictionary: type - list<gameObject>

    void Start()
    {
        // delete all or warm
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        // TODO: if no object available, then instantiate new
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
