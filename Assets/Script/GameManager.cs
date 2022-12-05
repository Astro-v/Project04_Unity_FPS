using System.Collections.Generic;
using UnityEngine;

// Class that list all the player
public class GameManager : MonoBehaviour
{
    #region Properties
    private const string _playerIdPrefix = "Player";

    private static Dictionary<string, Player> _player = new Dictionary<string, Player>();
    #endregion Properties

    #region Methods
    public void RegisterPlayer(string netID, Player player) // Put the player in the dictionnary in order to acces it fast and easy
    {
        string playeId = _playerIdPrefix + netID;
    }
    #endregion Methods
}
