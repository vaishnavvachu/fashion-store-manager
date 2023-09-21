using UnityEngine;

[CreateAssetMenu(fileName = "New Clothing Item", menuName = "Clothing Item")]
public class ClothingItem : ScriptableObject
{
    public string itemName;
    public GameObject itemPrefab;
}