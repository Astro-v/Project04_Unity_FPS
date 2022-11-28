using UnityEngine;

[RequireComponent(typeof(Rigidbody))] //  PlayerMotor can't work without Rigidbody (it's required) - it is a good way to do to be sure that every componant that have their dependency
public class PlayerMotor : MonoBehaviour
{
    #region Properties
    [SerializeField] private Camera _camera;
    [SerializeField] private float _cameraRotationLimit = 85f;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private float _cameraRotationX = 0f;
    private float _currentCameraRotationX = 0f;
    private Vector3 _thrusterVelocity = Vector3.zero;

    private Rigidbody _rb = null;
    #endregion Properties

    #region Methods
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 velocity)
    {
        _velocity = velocity;
    }

    public void Rotate(Vector3 rotation)
    {
        _rotation = rotation;
    }

    public void RotateCamera(float cameraRotationX)
    {
        _cameraRotationX = cameraRotationX;
    }

    public void ApplyThruster(Vector3 thrusterVelocity)
    {
        _thrusterVelocity = thrusterVelocity;
    }

    private void FixedUpdate() // More acces on physics than Update. Do not depend on the framerate
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement()
    {
        if (_velocity != Vector3.zero)
        {
            _rb.MovePosition(_rb.position + _velocity * Time.fixedDeltaTime); // _rb.position or transform.position are exactly the same in that precise case
        }

        if (_thrusterVelocity != Vector3.zero)
        {
            _rb.AddForce(_thrusterVelocity * Time.fixedDeltaTime, ForceMode.Acceleration); // Add a force to the rigid body
        }
    }

    private void PerformRotation()
    {
        _rb.MoveRotation(_rb.rotation * Quaternion.Euler(_rotation)); // MoveRotation work with Quaternion and not Vector.

        _currentCameraRotationX += _cameraRotationX;
        _currentCameraRotationX = Mathf.Clamp(_currentCameraRotationX, -_cameraRotationLimit, _cameraRotationLimit); // We clam the value of currentCameraRotation to fit in the defined domain
        _camera.transform.localEulerAngles = new Vector3(-_currentCameraRotationX, 0f, 0f);
    }
    #endregion Methods
}
