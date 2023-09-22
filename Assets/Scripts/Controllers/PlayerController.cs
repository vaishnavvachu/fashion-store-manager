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
        Debug.Log("Player CurrentState: "+_currentState);
        switch (_currentState)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.ClothCollected:
                ClothCollectedState();
                break;
            case PlayerState.ClothDelivered:
                break;
            case PlayerState.CashCollected:
                break;
        }
    }

    private void ClothCollectedState()
    {
        throw new System.NotImplementedException();
    }
}