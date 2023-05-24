using UnityEngine;
using UnityEngine.InputSystem;

public class cameraControl : MonoBehaviour
{
    private CameraControlActions cameraActions;
    private InputAction movement;
    private Transform cameraTransform;

    // horizontal
    [SerializeField]
    private float maxSpeed = 5f;
    private float speed;
    [SerializeField]
    private float acceleration = 10f;
    [SerializeField]
    private float damping = 15f;

    // zoom
    [SerializeField]
    private float stepSize = 2f;
    [SerializeField]
    private float zoomDampening = 7.5f;
    [SerializeField]
    private float minHeight = 5f;
    [SerializeField]
    private float maxHeight = 50f;
    [SerializeField]
    private float zoomSpeed = 2f;

    private Vector3 targetPosition;

    private float zoomHeight;

    private Vector3 horizontalVelosity;
    private Vector3 lastPosition;

    Vector3 startDrag;


    private void Awake() {
        cameraActions = new CameraControlActions();
        cameraTransform = this.GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable() {
        zoomHeight = cameraTransform.localPosition.y;
        cameraTransform.LookAt(this.transform);

        lastPosition = this.transform.position;
        movement = cameraActions.Camera.Movement;
        cameraActions.Camera.ZoomCamera.performed += ZoomCamera;

        cameraActions.Camera.Enable();
        
    }

    private void OnDisable() {
        cameraActions.Camera.ZoomCamera.performed -= ZoomCamera;
        cameraActions.Disable();
    }

    private void Update() {

        if (PlayerStats.Lose == true || Time.timeScale == 0f)
        {
            return;
        }
        GetKeyboardMovement();
        DragCamera();
        UpdateVelocity();
        UpdateCameraPosition();
        UpdateBasePosition();
    }

    private void UpdateVelocity() {
        horizontalVelosity = (this.transform.position  - lastPosition) / Time.deltaTime;
        horizontalVelosity.y = 0;
        lastPosition = this.transform.position;
    }

    private void GetKeyboardMovement() {
        Vector3 inputValue = movement.ReadValue<Vector2>().x * GetCameraRight()
                                + movement.ReadValue<Vector2>().y * GetCameraForward();
        inputValue = inputValue.normalized;

        if(inputValue.sqrMagnitude > 0.1f){
            targetPosition += inputValue;
        }
    }

    private Vector3 GetCameraRight() {
        Vector3 right = cameraTransform.right;
        right.y = 0;
        return right;
    }

    private Vector3 GetCameraForward() {
        Vector3 forward = cameraTransform.forward;
        forward.y = 0;
        return forward;
    }

    private void UpdateBasePosition()
    {

        if(targetPosition.sqrMagnitude > 0.1f){
            speed = Mathf.Lerp(speed,maxSpeed, Time.deltaTime * acceleration);
            transform.position += targetPosition * speed * Time.deltaTime;
        }
        else {
            horizontalVelosity = Vector3.Lerp(horizontalVelosity,Vector3.zero, Time.deltaTime * damping);
            /*transform.position += horizontalVelosity * Time.deltaTime;*/
        }

        targetPosition = Vector3.zero;
    }

    private void ZoomCamera(InputAction.CallbackContext inputValue) {
        float value = -inputValue.ReadValue<Vector2>().y / 50f;

        if(Mathf.Abs(value) > 0.1f) {
            zoomHeight = cameraTransform.localPosition.y + value * stepSize;
            if (zoomHeight < minHeight){
                zoomHeight = minHeight;
            }
            else if (zoomHeight > maxHeight) {
                zoomHeight = maxHeight;
            }
        }
    }

    private void UpdateCameraPosition() {
        Vector3 zoomTarget = new Vector3(cameraTransform.localPosition.x, zoomHeight, cameraTransform.localPosition.z);
        zoomTarget -= zoomSpeed * (zoomHeight - cameraTransform.localPosition.y) * Vector3.forward;

        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, zoomTarget, Time.deltaTime * zoomDampening);
        cameraTransform.LookAt(this.transform);
    }

    private void DragCamera() {
        if (!Mouse.current.rightButton.isPressed) {
            return;
        }
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (plane.Raycast(ray, out float distance)) {
            if(Mouse.current.rightButton.wasPressedThisFrame) {
                startDrag = ray.GetPoint(distance);
            }
            else {
                targetPosition += startDrag - ray.GetPoint(distance);
            }
        }
    }
    
}
