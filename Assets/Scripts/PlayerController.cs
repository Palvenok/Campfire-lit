using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private AnimationController anim;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private Inventory inventory;

    private CharacterController _charController;
    private bool _isDrop;

    private void Awake()
    {
        _charController = GetComponent<CharacterController>();

        if (inputHandler == null) inputHandler = FindObjectOfType<InputHandler>();
        if (anim == null) anim = GetComponentInChildren<AnimationController>();
        if (inventory == null) inventory = GetComponentInChildren<Inventory>();
    }

    private void FixedUpdate()
    {
        if (inventory.IsEmpty)
            anim.Hold = false;
        else
            anim.Hold = true;

        if (inputHandler.MoveInput == Vector3.zero)
        {
            anim.Run = false;
            return;
        }

        anim.Run = true;

        Move();
        Rotate();
    }

    private void Move()
    {
        _charController.Move(inputHandler.MoveInput * moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        var rotInput = inputHandler.MoveInput;

        float angle = Mathf.Atan2(rotInput.x, rotInput.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isDrop) return;

        if(other.TryGetComponent(out Log log))
        {
            inventory.PushItem(log.gameObject);
        }

        if (other.TryGetComponent(out CampFire campFire))
        {
            var campFireInventory = campFire.GetComponent<Inventory>();
            _isDrop = true;

            while (!inventory.IsEmpty)
            {
                var item = inventory.TakeItem();
                campFireInventory.PushItem(item);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CampFire>())
        {
            _isDrop = false;
        }
    }
}
