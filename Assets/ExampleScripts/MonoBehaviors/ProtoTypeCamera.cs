using UnityEngine;

// imperfect generated prototype camera movement 
public class ProtoTypeCamera : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float zoomSpeed = 10f;
    public float rotationSpeed = 10f;
    public float minZoomDistance = 1f;
    public float maxZoomDistance = 10f;

    private float currentZoomDistance = 5f;


    void Update()
    {
        // Move the camera using the WASD keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Zoom in and out using the mouse wheel
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoomDistance -= scrollInput * zoomSpeed;
        currentZoomDistance = Mathf.Clamp(currentZoomDistance, minZoomDistance, maxZoomDistance);

        // Rotate the camera to face downwards at a 45 degree angle
        transform.rotation = Quaternion.Euler(45f, 0f, 0f);

        // Rotate the camera using the mouse
        float rotateInput = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, rotateInput * rotationSpeed * Time.deltaTime);
    }

    void LateUpdate()
    {
        // Set the camera's distance from the ground
        Vector3 newPosition = transform.position;
        newPosition.y = currentZoomDistance;
        transform.position = newPosition;
    }
}
