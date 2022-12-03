using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    #region Properties
    [SerializeField] private Behaviour[] _componentsToDisable;
    [SerializeField] private string _remoteLayerName = "RemotePlayer";
    
    private Camera _sceneCamera = null;
    #endregion Properties

    #region Methods
    private void Start()
    {  
        if (!isLocalPlayer) // If the player is not the local player, it is the remote player
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            _sceneCamera = Camera.main;
            if (_sceneCamera != null)
            {
                _sceneCamera.gameObject.SetActive(false);
            }
        }
        RegisterPlayer();
    }

    private void RegisterPlayer()
    {
        // Change the name of the player by "Player" + unique Id
        string playerName = "Player" + GetComponent<NetworkIdentity>().netId;
        transform.name = playerName; // Rename the gameObject
    }

    private void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(_remoteLayerName);
    }

    private void DisableComponents()
    {
        // We loop over the differents componant to disable it if the player correspond to the remote player
        for (int i = 0; i < _componentsToDisable.Length; i++)
        {
            _componentsToDisable[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        if (_sceneCamera != null)
        {
            _sceneCamera.gameObject.SetActive(true);
        }
    }
    #endregion Methods
}
