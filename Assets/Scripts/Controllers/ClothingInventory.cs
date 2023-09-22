using System.Collections.Generic;
using UnityEngine;
public class ClothingInventory : MonoBehaviour
{
    private List<ClothingItem> inventory = new List<ClothingItem>();
    public int maxClothes = 5;
    public Transform clothHolder;
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
        DontDestroyOnLoad(this.gameObject); 
    }
    
    public void AddClothingItem(ClothingItem clothingItem)
    {
        if (inventory.Count < maxClothes)
        {
            inventory.Add(clothingItem);
            Vector3 stackPosition = Vector3.up * inventory.Count * 0.5f; 
            GameObject clothingObject = Instantiate(clothingItem.itemPrefab, clothHolder);
            clothingObject.transform.localPosition = stackPosition; 

            Debug.Log("Collected clothing: " + clothingItem.itemName);
        }
        else
        {
            Debug.Log("Inventory is full");
        }
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
