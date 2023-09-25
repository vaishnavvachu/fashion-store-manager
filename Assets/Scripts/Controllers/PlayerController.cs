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
    public bool isKeyboard;
    
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private Animator playerAnimator;

    private CharacterController _characterController;
    private PlayerState _currentState;
    private Vector3 _velocity = Vector3.zero; 
 

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _currentState = PlayerState.Idle;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _velocity = Vector3.zero; 
                    playerAnimator.SetBool("isWalking",false);
                    break;

                case TouchPhase.Moved:
                    Vector2 swipeDirection = touch.deltaPosition.normalized;
                    playerAnimator.SetBool("isWalking",true);
                    if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
                    {
                        if (swipeDirection.x > 0)
                        {
                            _velocity = Vector3.right * moveSpeed;
                        }
                        else
                        {
                            _velocity = Vector3.left * moveSpeed;
                        }
                    }
                    else
                    {
                        if (swipeDirection.y > 0)
                        {
                            _velocity = Vector3.forward * moveSpeed;
                        }
                        else
                        {
                            _velocity = Vector3.back * moveSpeed;
                        }
                    }
                    break;
            }
        }
        else
        {
            _velocity = Vector3.zero;
            playerAnimator.SetBool("isWalking",false);
        }
        
        _characterController.SimpleMove(_velocity);
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