using UnityEngine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    #region Properties
    [SerializeField] private Behaviour[] _componentsToDisable;
    
    private Camera _sceneCamera = null;
    #endregion Properties

    #region Methods
    private void Start()
    {  
        if (!isLocalPlayer) // If the player is not the local player, it is the remote player
        {
            // We loop over the differents componant to disable it if the player correspond to the remote player
            for (int i = 0; i < _componentsToDisable.Length; i++)
            {
                _componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            _sceneCamera = Camera.main;
            if (_sceneCamera != null)
            {
                _sceneCamera.gameObject.SetActive(false);
            }
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
