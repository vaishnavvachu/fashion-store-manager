using UnityEngine;

[System.Serializable]
public enum PlayerState
{
    Idle,
    ClothCollected,
    ClothDelivered,
    CashCollected
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    private CharacterController _characterController;
   
    private PlayerState _currentState;
    
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        
        _currentState = PlayerState.Idle;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        moveDirection.Normalize();
        moveDirection = transform.TransformDirection(moveDirection);
        
        Vector3 moveVelocity = moveDirection * moveSpeed;

        _characterController.Move(moveVelocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactable.OnInteracted();
        }
    }
    public void ChangeState(PlayerState newState)
    {
        _currentState = newState;
        switch (_currentState)
        {
            case PlayerState.Idle:
                OnIdle();
                break;
            case PlayerState.ClothCollected:
                OnClothCollectedState();
                break;
            case PlayerState.ClothDelivered:
                OnClothDelivered();
                break;
            case PlayerState.CashCollected:
                OnCashCollected();
                break;
        }
    }

    private void OnCashCollected()
    {
        UIManager.Instance.ShowCashCollectedUIFx();
    }

    private void OnClothDelivered()
    {
        UIManager.Instance.ShowPlayerSpeechBubble("Collect Cash");
    }

    private void OnIdle()
    {
        UIManager.Instance.ShowPlayerSpeechBubble("Goto Rack");
    }

    private void OnClothCollectedState()
    {
        UIManager.Instance.ShowPlayerSpeechBubble("Goto Customer");
    }
}