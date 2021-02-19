using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    const int maxPlayers = 2;
    List<Player> players = new List<Player>(maxPlayers);
    public List<Vector3> SpawnLocations = new List<Vector3>();

    public GameObject playerPrefab;

    private void OnEnable()
    {
        InputManager.OnDeviceDetached += OnDeviceDetached;
    }

    void Update()
    {
        var inputDevice = InputManager.ActiveDevice;

        if (JoinButtonWasPressedOnDevice(inputDevice))
        {
            if (ThereIsNoPlayerUsingDevice(inputDevice))
            {
                CreatePlayer(inputDevice);
            }
        }
    }


    bool JoinButtonWasPressedOnDevice(InputDevice inputDevice)
    {
        return inputDevice.AnyButtonWasPressed || inputDevice.Action1.WasPressed || inputDevice.Action2.WasPressed || inputDevice.Action3.WasPressed || inputDevice.Action4.WasPressed;
    }


    Player FindPlayerUsingDevice(InputDevice inputDevice)
    {
        var playerCount = players.Count;
        for (var i = 0; i < playerCount; i++)
        {
            var player = players[i];
            if (player.Device == inputDevice)
            {
                return player;
            }
        }

        return null;
    }


    bool ThereIsNoPlayerUsingDevice(InputDevice inputDevice)
    {
        return FindPlayerUsingDevice(inputDevice) == null;
    }


    void OnDeviceDetached(InputDevice inputDevice)
    {
        var player = FindPlayerUsingDevice(inputDevice);
        if (player != null)
        {
            RemovePlayer(player);
        }
    }

    Player CreatePlayer(InputDevice inputDevice)
    {
        if (players.Count < maxPlayers)
        {
            var pos = SpawnLocations[players.Count];
            var gameObject = (GameObject)Instantiate(playerPrefab, pos, Quaternion.identity);
            var player = gameObject.GetComponent<Player>();
            player.Device = inputDevice;
            players.Add(player);

            return player;
        }

        return null;
    }


    void RemovePlayer(Player player)
    {
        players.Remove(player);
        player.Device = null;
        Destroy(player.gameObject);
    }
}
