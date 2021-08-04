using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle
{
    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        Server.clients[_fromClient].SendIntoGame(_username);
    }

    public static void PlayerMovement(int _fromClient, Packet _packet)
    {
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();
        Quaternion cameraRotation = _packet.ReadQuaternion();

        Server.clients[_fromClient].player.transform.position = _position;
        Server.clients[_fromClient].player.transform.rotation = _rotation;
        Server.clients[_fromClient].player.cameraRotation = cameraRotation;
    }


    public static void SelectWeapon(int _fromClient, Packet _packet)
    {
        int selectedWeapon = _packet.ReadInt();
        Server.clients[_fromClient].player.SelectWeapon(selectedWeapon);
    }

    public static void GetGunPositionAndRotation(int _fromClient, Packet _packet)
    {
        int gunId = _packet.ReadInt();
        Vector3 gunPosition = _packet.ReadVector3();
        Quaternion gunRotation = _packet.ReadQuaternion();
        Quaternion boneRotation = _packet.ReadQuaternion();

        Server.clients[_fromClient].player.SendRotationAndPosition(gunPosition, gunRotation, gunId, boneRotation);
    }

    public static void GetGunSounds(int _fromClient, Packet _packet)
    {
        int gunId = _packet.ReadInt();
        int soundEffectId = _packet.ReadInt();

        Server.clients[_fromClient].player.SendGunSounds(gunId, soundEffectId);
    }


    public static void GetBulletHitPoint(int _fromClient, Packet _packet)
    {
        int gunId = _packet.ReadInt();
        Vector3 bulletHitPoint = _packet.ReadVector3();
        float bulletForce = _packet.ReadFloat();
        Vector3 decalNormal = _packet.ReadVector3();

        Server.clients[_fromClient].player.SendBulletHitPoint(gunId, bulletHitPoint, bulletForce, decalNormal);
    }
}
