using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMouseAndPhysics : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1.0f; // Controls how fast the player moves
    [SerializeField] private float RotationSpeed = 1.0f; // Controls how fast the player rotates

    [SerializeField] private Transform PlayerHead;

    private Rigidbody physicsBody;
    private float horizontalFacing = 0f; // Horizontal rotation for left and right movement
    private float verticalFacing = 0f; // Vertical rotation for looking up and down
    private float desiredHeight;

    void Start()
    {

        PlayerHead.localPosition = new Vector3(PlayerHead.localPosition.x, -0.8f, PlayerHead.localPosition.z);


        physicsBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        physicsBody.freezeRotation = true;
    }

    void Update()
    {
        // Allow the player to look around with the mouse
        MoveCameraWithMouse();
    }

    private void FixedUpdate()
    {

        MoveWithPhysics();
    }

    private void MoveCameraWithMouse()
    {

        float mouseX = Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime;


        verticalFacing -= mouseY;
        verticalFacing = Mathf.Clamp(verticalFacing, -90f, 90f);


        horizontalFacing += mouseX;

        // Apply the vertical rotation to the player's head
        PlayerHead.localRotation = Quaternion.Euler(verticalFacing, 0f, 0f);
        physicsBody.rotation = Quaternion.Euler(0f, horizontalFacing, 0f);
    }

    private void MoveWithPhysics()
    {

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");


        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        // Move the player based on input direction and speed
        transform.Translate(moveDirection * MoveSpeed * Time.deltaTime, Space.World);
    }
}
