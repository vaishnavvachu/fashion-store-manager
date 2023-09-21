using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
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
}