using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    #region Properties
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _mask;

    public PlayerWeapon _weapon;
    #endregion Properties

    #region Methods
    private void Start()
    {
        if (_camera == null)
        {
            Debug.LogError("No camera selected in the PlayerShoot class");
            this.enabled = false;
        }
    }

    private void Update() // For the Input
    {
        if (Input.GetButtonDown("Fire1")) // Fire1 is the default button for left clic
        { // Here, the GetButtonDown mean that the weapon is semi-automatic
            Shoot();
        }
    }

    [Client]
    private void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, _weapon._range, _mask)) // Here we shoot the raycast and it return true if the raycast touch something
        {
            if (hit.collider.tag == "Player")
            {
                CmdPlayerShot(hit.collider.name);
            }
        }
    }

    [Command] // Send to the server and all the instance will receive data
    private void CmdPlayerShot(string playerName) // Cmd to notify that we are in a command
    {
        Debug.Log(playerName + " has been touched.");
    }
    #endregion Methods
}
