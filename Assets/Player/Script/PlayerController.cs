using UnityEngine;

[RequireComponent(typeof(PlayerMotor))] //  PlayerController can't work without PlayerMotors (it's required) - it is a good way to do to be sure that every componant that have their dependency
public class PlayerController : MonoBehaviour
{
    #region Properties
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _mouseSensitivityX = 6f;
    [SerializeField] private float _mouseSensitivityY = 5f;

    private PlayerMotor _motor;
    #endregion Properties

    #region Methods
    private void Start()
    {
        _motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        /* FOR THE PLAYER MOVEMENT */

        // Compute velocity (speed) of the player's movement
        float xMov = Input.GetAxisRaw("Horizontal"); // GetAxisRaw has no filter (it is the true data) and GetAxis apply a filter // We get -1 with the D key and +1 with the Q key
        float zMov = Input.GetAxisRaw("Vertical"); // We get -1 with the S key and +1 with the Z key

        Vector3 moveHorizontal = transform.right * xMov; // transform.right give the axes (transform.left work aswell)
        Vector3 moveVertical = transform.forward * zMov;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * _speed; // Direction (normalized) times the speed

        _motor.Move(velocity);

        /* FOR THE PLAYER ROTATION */

        float yRot = Input.GetAxisRaw("Mouse X"); // Rotation around the y rotation, it match with the X movement of the mouse

        Vector3 rotation = new Vector3(0, yRot, 0).normalized * _mouseSensitivityX;

        _motor.Rotate(rotation);

        /* FOR THE CAMERA ROTATION */

        float xRot = Input.GetAxisRaw("Mouse Y"); // Rotation around the x rotation, it match with the Y movement of the mouse

        Vector3 cameraRotation = new Vector3(xRot, 0, 0).normalized * _mouseSensitivityY;

        _motor.RotateCamera(cameraRotation);
    }
    #endregion Methods
}
