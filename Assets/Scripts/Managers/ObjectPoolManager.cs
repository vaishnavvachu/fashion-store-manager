using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private Dictionary<string, List<GameObject>> _objectPools = new Dictionary<string, List<GameObject>>();

    [SerializeField] private List<GameObject> objectPrefabsToPool = new List<GameObject>();
    [SerializeField] private int initialPoolSize = 10;

    private static ObjectPoolManager _instance;
    
    public static ObjectPoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjectPoolManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("ObjectPoolManager");
                    _instance = obj.AddComponent<ObjectPoolManager>();
                }
            }
            return _instance;
        }
    }
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        InitializeObjectPools();
    }

    private void InitializeObjectPools()
    {
        foreach (GameObject prefab in objectPrefabsToPool)
        {
            CreateObjectPool(prefab.name); 
        }
    }

    private void CreateObjectPool(string prefabName)
    {
        List<GameObject> objectPool = new List<GameObject>();

        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(GetPrefabByName(prefabName));
            obj.SetActive(false);
            objectPool.Add(obj);
        }

        _objectPools[prefabName] = objectPool;
    }

    public GameObject GetObjectFromPool(string prefabName)
    {
        if (_objectPools.ContainsKey(prefabName))
        {
            foreach (GameObject obj in _objectPools[prefabName])
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

           
            GameObject newObj = Instantiate(GetPrefabByName(prefabName));
            _objectPools[prefabName].Add(newObj);
            newObj.SetActive(true);
            return newObj;
        }
        else
        {
            Debug.LogWarning("Object pool not found for prefab: " + prefabName);
            return null;
        }
    }
    
    private GameObject GetPrefabByName(string prefabName)
    {
        foreach (GameObject prefab in objectPrefabsToPool)
        {
            if (prefab.name == prefabName)
            {
                return prefab;
            }
        }
        return null;
    }
}
