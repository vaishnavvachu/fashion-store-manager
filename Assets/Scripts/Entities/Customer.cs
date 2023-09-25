using DG.Tweening;
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
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float duration = 1.0f;
    [SerializeField] private Animator animator;

    private Vector3 _initialPos;
    private CustomerState _currentState = CustomerState.WalkIn;

    private void Start()
    {
        UpdateCustomerState(_currentState);
        // GameObject cash = ObjectPoolManager.Instance.GetObjectFromPool("Cash");
        // cash.transform.position = transform.position;
        _initialPos = transform.position;
        
        MoveObject(targetPosition.position);
    }
    
    private void MoveObject(Vector3 pos)
    {
        
        transform.DOMove(pos, duration)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                animator.Play("Idle");
            });
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
       UIManager.Instance.ShowCustomerSpeechBubble("Want Red Dress");
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
        GameObject cash = ObjectPoolManager.Instance.GetObjectFromPool("Cash");
        cash.transform.position = transform.position;
        UpdateCustomerState(CustomerState.Leaving);
    }

    void OnLeave()
    {
        MoveObject(_initialPos);
        UIManager.Instance.ShowCustomerSpeechBubble("Thank you");
    }
}
