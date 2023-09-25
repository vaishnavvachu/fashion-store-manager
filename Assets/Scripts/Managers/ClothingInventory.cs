using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothingInventory : MonoBehaviour
{
    private List<ClothingItem> _inventory = new List<ClothingItem>();
    [SerializeField] private int maxClothes = 5;
    [SerializeField] private Transform clothHolder;
    [SerializeField] private Image loadingImage;
    
    private static ClothingInventory _instance; 
   
    public static ClothingInventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ClothingInventory>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("ClothingInventory");
                    _instance = obj.AddComponent<ClothingInventory>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        
        loadingImage.gameObject.SetActive(false);
        
        DontDestroyOnLoad(this.gameObject); 
    }
    
    public void AddClothingItem(ClothingItem clothingItem)
    {
        StartCoroutine(AddClothingItemWithDelay(clothingItem));
    }

    private IEnumerator AddClothingItemWithDelay(ClothingItem clothingItem)
    {
        loadingImage.fillAmount = 0;
        loadingImage.gameObject.SetActive(true);
        
        float elapsedTime = 0;
        while (elapsedTime < 2)
        {
            elapsedTime += Time.deltaTime;
            loadingImage.fillAmount = elapsedTime / 2;
            yield return null;
        }
        if (_inventory.Count < maxClothes)
        {
            _inventory.Add(clothingItem);
            Vector3 stackPosition = Vector3.up * _inventory.Count * 0.5f; 
            GameObject clothingObject = Instantiate(clothingItem.itemPrefab, clothHolder);
            clothingObject.transform.localPosition = stackPosition; 
        }
        else
        {
            UIManager.Instance.ShowPlayerSpeechBubble("Inventory Full");
        }
        loadingImage.gameObject.SetActive(false);
    }

    
    public bool ClothingMatchesCustomerRequest(ClothingItem clothingItem)
    {
        foreach (ClothingItem item in _inventory)
        {
            Debug.Log("- " + item.itemName);
        }

        return _inventory.Contains(clothingItem);
    }


    public void RemoveClothingItem(ClothingItem clothingItem)
    {
        if (_inventory.Contains(clothingItem))
        {
            _inventory.Remove(clothingItem);
        }
    }

    private void DropAllClothes()
    {
        foreach (ClothingItem item in _inventory)
        {
            if (item.itemPrefab != null)
            {
                Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
            }
        }
        _inventory.Clear();
    }
}
