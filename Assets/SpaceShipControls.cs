using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.InputSystem.Controls.AxisControl;


public class SpaceshipController : MonoBehaviour
{
    public float moveSpeed = 10f; //Dopredu dozadu speed
    public float rotationSpeed = 100f; //Rotace speed
    public float strafeSpeed = 10f; //do stran
    public float verticalSpeed = 10f; //vertical
    public float mouseSensitivity = 2f; //asi jasny ne mouse senska
    public float rotationSmoothness = 5f;  // Lower = slower rotation
    public float maxRotationSpeed = 90f;   // Maximum degrees per second
    public float deadZone = 0.1f;
    public float rotationCurve = 2f;



    public float pitch = 0f;
    public float yaw = 0f;
    public float roll = 0f;

    private Vector3 currentRotationVelocity;
    private Vector2 screenCenter;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    void Update()
    {
        UpdateMovement();
        UpdateRotation();
    }

    void UpdateMovement() {
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
    }

    float ApplyRotationCurve(float input)
    {
        float normalizedInput = Mathf.Clamp01(Mathf.Abs(input));
        float curvedValue = Mathf.Pow(normalizedInput, rotationCurve);
        return Mathf.Sign(input) * curvedValue;
    }

    void UpdateRotation() {
        Vector2 mouseOffset = ((Vector2)Input.mousePosition - screenCenter) / screenCenter;


        Vector3 targetRotationVelocity = new Vector3(
        -ApplyRotationCurve(mouseOffset.y) * maxRotationSpeed * mouseSensitivity,
        ApplyRotationCurve(mouseOffset.x) * maxRotationSpeed * mouseSensitivity,
        (Input.GetKey(KeyCode.Q) ? rotationSpeed : Input.GetKey(KeyCode.E) ? -rotationSpeed : 0)
    );


        if (mouseOffset.magnitude <= deadZone) {
            targetRotationVelocity.x = 0;
            targetRotationVelocity.y = 0;
        }

        currentRotationVelocity = Vector3.Lerp(
            currentRotationVelocity,
            targetRotationVelocity,
            Time.deltaTime * rotationSmoothness
        );

        // Apply rotations
        transform.Rotate(Vector3.left * currentRotationVelocity.x * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.up * currentRotationVelocity.y * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.forward * currentRotationVelocity.z * Time.deltaTime, Space.Self);

    }
}
