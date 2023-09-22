using UnityEngine;

public class ClothRack : MonoBehaviour, IInteractable
{
    public ClothingItem clothingItem;
    public void OnInteracted()
    {
        ClothingInventory.Instance.AddClothingItem(clothingItem);
        GameManager.Instance.UpdatePlayerState(PlayerState.ClothCollected);
    }
}
