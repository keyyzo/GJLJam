using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Base Movement Settings")]

    [SerializeField] float movementSpeed = 10.0f;
    [SerializeField] float accelerationSpeed = 3.0f;
    [SerializeField] float decelerationSpeed = 5.0f;

    // Private movement variables

    float _currentSpeed = 0.0f;

    // Cached Components

    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void HandleMovement(Vector2 inputVec)
    {
        if (inputVec == Vector2.zero)
        {
            if (_currentSpeed > 0f)
            {
                _currentSpeed = Mathf.Lerp(_currentSpeed, 0f, Time.deltaTime * decelerationSpeed);
                _currentSpeed = Mathf.Max(_currentSpeed, 0f);
            }
        }

        else
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, movementSpeed, Time.deltaTime * accelerationSpeed);
        }

        Vector3 inputDirection = new Vector3(inputVec.x, 0.0f, inputVec.y).normalized;
        Vector3 movement = transform.right * inputDirection.x + transform.forward * inputDirection.z;

        characterController.Move(movement.normalized * (_currentSpeed * Time.deltaTime));
    }
    

    

}
