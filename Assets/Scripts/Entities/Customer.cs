using UnityEngine;

[System.Serializable]
public enum CustomerState
{
    WalkIn,
    ClothReceived,
    Happy,
    Leaving
}
public class Customer : MonoBehaviour, IInteractable
{
    [SerializeField] private ClothingItem requestedCloth; 
    private CustomerState _currentState = CustomerState.WalkIn;

    public void OnInteracted()
    {
        Debug.Log("OnInteracted");
        if(_currentState == CustomerState.WalkIn)
        {
            UpdateCustomerState(CustomerState.ClothReceived);
        }
    }

    public void UpdateCustomerState(CustomerState customerState)
    {
        Debug.Log("Customer CurrentState: "+customerState);
            switch (customerState)
            {
                case CustomerState.WalkIn:
                    _currentState = CustomerState.WalkIn;
                    break;
                case CustomerState.ClothReceived:
                    _currentState = CustomerState.ClothReceived;
                    OnClothReceived();
                    break;
                case CustomerState.Happy:
                    _currentState = CustomerState.Happy;
                    OnHappy();
                    break;
                case CustomerState.Leaving:
                    _currentState = CustomerState.Leaving;
                    OnLeave();
                    break;
            }
    }

    void OnClothReceived()
    {
        if (ClothingInventory.Instance.ClothingMatchesCustomerRequest(requestedCloth))
        {
            ClothingInventory.Instance.RemoveClothingItem(requestedCloth);
            Debug.Log("Customer received the requested cloth: " + requestedCloth.itemName);
            GameManager.Instance.UpdatePlayerState(PlayerState.ClothDelivered);
            UpdateCustomerState(CustomerState.Happy);
        }
        else
        {
            Debug.Log("Player does not have the requested cloth.");
            _currentState = CustomerState.WalkIn;
        }
    }
    void OnHappy()
    {
        UpdateCustomerState(CustomerState.Leaving);
        Debug.Log("Throw Cash");
    }

    void OnLeave()
    {
        Debug.Log("Customer Leaves");
    }
}
