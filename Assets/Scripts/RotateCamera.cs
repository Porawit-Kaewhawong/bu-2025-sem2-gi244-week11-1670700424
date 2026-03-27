using UnityEngine;
using UnityEngine.InputSystem;

// 1.1 START HERE
public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;

    private InputAction moveAction;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void Update()
    {
        // Create a variable to read value of "moveAction" in Vector2 and store in "move"
        var move = moveAction.ReadValue<Vector2>();

        // Create a variable to store "move"'s x axis
        var horizontalInput = move.x;

        transform.Rotate(Vector3.up, rotationSpeed * horizontalInput * Time.deltaTime);
    }
}
