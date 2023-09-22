using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothingInventory : MonoBehaviour
{
    private List<ClothingItem> inventory = new List<ClothingItem>();
    [SerializeField] private int maxClothes = 5;
    [SerializeField] private Transform clothHolder;
    [SerializeField] private Image loadingImage;
    
    private static ClothingInventory instance; 
   
    public static ClothingInventory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ClothingInventory>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("ClothingInventory");
                    instance = obj.AddComponent<ClothingInventory>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        
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
        if (inventory.Count < maxClothes)
        {
            inventory.Add(clothingItem);
            Vector3 stackPosition = Vector3.up * inventory.Count * 0.5f; 
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
        foreach (ClothingItem item in inventory)
        {
            Debug.Log("- " + item.itemName);
        }

        return inventory.Contains(clothingItem);
    }


    public void RemoveClothingItem(ClothingItem clothingItem)
    {
        if (inventory.Contains(clothingItem))
        {
            inventory.Remove(clothingItem);
        }
    }

    private void DropAllClothes()
    {
        foreach (ClothingItem item in inventory)
        {
            if (item.itemPrefab != null)
            {
                Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
            }
        }
        inventory.Clear();
    }
}
