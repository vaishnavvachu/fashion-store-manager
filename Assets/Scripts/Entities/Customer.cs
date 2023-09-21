using UnityEngine;

public class Customer : MonoBehaviour, IInteractable
{
    [SerializeField] private ClothingItem requestedCloth; 
    
    public void OnInteracted()
    {
        if (ClothingInventory.Instance.ClothingMatchesCustomerRequest(requestedCloth))
        {
            ClothingInventory.Instance.RemoveClothingItem(requestedCloth);
            Debug.Log("Customer received the requested cloth: " + requestedCloth.itemName);
        }
        else
        {
            Debug.Log("Player does not have the requested cloth.");
        }
    }
}
