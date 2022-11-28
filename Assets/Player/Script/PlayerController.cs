using UnityEngine;

[RequireComponent(typeof(PlayerMotor))] //  PlayerController can't work without PlayerMotors (it's required) - it is a good way to do to be sure that every componant that have their dependency
[RequireComponent(typeof(ConfigurableJoint))]
public class PlayerController : MonoBehaviour
{
    #region Properties
    [SerializeField] private float _speed = 12f;
    [SerializeField] private float _mouseSensitivityX = 6f;
    [SerializeField] private float _mouseSensitivityY = 5f;
    [SerializeField] private float _thrusterForce = 1000f;
    [Header("Joint Options")]
    [SerializeField] private float _jointSpring = 20f; // Spring and Max force for the ConfigurableJoint
    [SerializeField] private float _jointMaxForce = 40f;

    private PlayerMotor _motor;
    private ConfigurableJoint _joint;
    #endregion Properties

    #region Methods
    private void Start()
    {
        _motor = GetComponent<PlayerMotor>();
        _joint = GetComponent<ConfigurableJoint>();
        SetJointSettings(_jointSpring);
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

        float cameraRotationX = xRot * _mouseSensitivityY;

        _motor.RotateCamera(cameraRotationX);

        /* FOR THE THRUSTER */

        Vector3 thrusterVelocity = Vector3.zero;

        if (Input.GetButton("Jump")) // If the space key is press
        {
            thrusterVelocity = Vector3.up * _thrusterForce;
            SetJointSettings(0f); // We disabled the "gravity"
        }
        else
        {
            SetJointSettings(_jointSpring);
        }

        _motor.ApplyThruster(thrusterVelocity); // Apply the thruster force
    }

    private void SetJointSettings(float jointSpring) // The argument is use to make a dynamic joint spring. By passing 0 we disabled the gravity
    {
        _joint.yDrive = new JointDrive { positionSpring = jointSpring, maximumForce = _jointMaxForce };
    }
    #endregion Methods
}
