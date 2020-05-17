using UnityEngine;

public class WeaponControllerObject
{
    public BoltEntity weapon;
    public BoltConnection connection;

    public bool IsServer
    {
        get { return connection == null; }
    }

    public bool IsClient
    {
        get { return connection != null; }
    }

    public void Spawn()
    {
        if (!weapon)
        {
            weapon = BoltNetwork.Instantiate(BoltPrefabs.Weapon_Blaster, new Vector3(), Quaternion.identity);

            if (IsServer)
            {
                weapon.TakeControl();
            }
            else
            {
                weapon.AssignControl(connection);
            }
        }

        // teleport entity to a random spawn position
        weapon.transform.position = new Vector3();
    }
}