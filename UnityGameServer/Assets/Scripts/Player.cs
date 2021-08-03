using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int id;
    public string username;
    public Quaternion cameraRotation;

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
    }

    /// <summary>Processes player input and moves the player.</summary>
    public void FixedUpdate()
    {
        Move();
    }

    /// <summary>Calculates the player's desired movement direction and moves him.</summary>
    /// <param name="_inputDirection"></param>
    private void Move()
    {
        ServerSend.PlayerMovement(this);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);

        ServerSend.PlayerRespawned(this);
    }

    public void SelectWeapon(int selectedWeapon)
    {
        ServerSend.SendSelectedWeaponNumber(this, selectedWeapon);
    }

    public void SendRotationAndPosition(Vector3 position, Quaternion rotation, int gunId, Quaternion boneRotation)
    {
        ServerSend.SendGunRotationAndPosition(this, position, rotation, gunId, boneRotation);
    }

    public void SendGunSounds(int gunId, int soundEffectId)
    {
        ServerSend.SendGunSounds(this, gunId, soundEffectId);
    }
}
