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

    private void Start()
    {
        UpdateCustomerState(_currentState);
    }

    public void OnInteracted()
    {
        if(_currentState == CustomerState.WalkIn)
        {
            UpdateCustomerState(CustomerState.ClothReceived);
        }
    }

    public void UpdateCustomerState(CustomerState customerState)
    {
            switch (customerState)
            {
                case CustomerState.WalkIn:
                    _currentState = CustomerState.WalkIn;
                    OnCustomerWalkin();
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

    private void OnCustomerWalkin()
    {
       UIManager.Instance.ShowCustomerSpeechBubble("Red");
    }

    void OnClothReceived()
    {
        if (ClothingInventory.Instance.ClothingMatchesCustomerRequest(requestedCloth))
        {
            ClothingInventory.Instance.RemoveClothingItem(requestedCloth);
            UIManager.Instance.ShowCustomerSpeechBubble("Cloth Received");
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
        UIManager.Instance.ShowCustomerSpeechBubble("Happy");
        UpdateCustomerState(CustomerState.Leaving);
    }

    void OnLeave()
    {
        UIManager.Instance.ShowCustomerSpeechBubble("Thank you");
    }
}
