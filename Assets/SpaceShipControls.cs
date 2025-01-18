using UnityEngine;


public class SpaceshipController : MonoBehaviour
{
    public float moveSpeed = 10f; //Dopredu dozadu speed
    public float rotationSpeed = 100f; //Rotace speed
    public float strafeSpeed = 10f; //do stran
    public float verticalSpeed = 10f; //vertical
    public float mouseSensitivity = 2f; //asi jasny ne mouse senska

    
    public float pitch = 0f;
    public float yaw = 0f;
    public float roll = 0f;

    private Vector3 currentRotation;

    void Update()
    {
        // Move forward and backward
        float move = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * move);

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * strafeSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * strafeSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.up * verticalSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime);
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(-mouseY, mouseX, 0, Space.Self);

        pitch -= mouseY; // Invert mouse Y for natural airplane control
        yaw += mouseX;


        // Adjust pitch (up/down) and yaw (left/right) in WORLD SPACE
        Vector3 pitchYawAdjustment = new Vector3(-mouseY, mouseX, 0f);
        currentRotation += pitchYawAdjustment;

        // Apply rotation in local space relative to the world
        Quaternion targetRotation = Quaternion.Euler(currentRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);


        if (Input.GetKey(KeyCode.Q))
        {
            roll += rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            roll -= rotationSpeed * Time.deltaTime;
        }

        transform.Rotate(0, 0, roll, Space.Self);
    }
}
