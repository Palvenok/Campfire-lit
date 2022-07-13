using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private const string horizontalMoveAxis = "Horizontal";
    [SerializeField] private const string verticalMoveAxis = "Vertical";

    private Vector3 _moveInput;

    public Vector3 MoveInput => _moveInput;

    private void Update()
    {
        _moveInput.x = SimpleInput.GetAxis(horizontalMoveAxis);
        _moveInput.z = SimpleInput.GetAxis(verticalMoveAxis);
    }

}
