using UnityEngine;

public class ClothRack : MonoBehaviour, IInteractable
{
    public ClothingItem clothingItem;
    public void OnInteracted()
    {
        Debug.Log("Cloth Rack Hit");
        ClothingInventory.Instance.AddClothingItem(clothingItem);
        GameManager.Instance.UpdatePlayerState(PlayerState.ClothCollected);
    }
}
