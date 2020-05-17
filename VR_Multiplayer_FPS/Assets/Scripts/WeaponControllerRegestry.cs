using System.Collections.Generic;

public static class WeaponControllerObjectRegestry
{
    // keeps a list of all the weapons
    static List<WeaponControllerObject> weapons = new List<WeaponControllerObject>();

    // create a weapon for a connection
    // note: connection can be null
    static WeaponControllerObject CreateWeapon(BoltConnection connection)
    {
        WeaponControllerObject weapon;

        // create a new weapon object, assign the connection property
        // of the object to the connection was passed in
        weapon = new WeaponControllerObject();
        weapon.connection = connection;

        // if we have a connection, assign this weapon
        // as the user data for the connection so that we
        // always have an easy way to get the weapon object
        // for a connection
        if (weapon.connection != null)
        {
            weapon.connection.UserData = weapon;
        }

        // add to list of all weapons
        weapons.Add(weapon);

        return weapon;
    }

    // this simply returns the 'weapons' list cast to
    // an IEnumerable<T> so that we hide the ability
    // to modify the weapon list from the outside.
    public static IEnumerable<WeaponControllerObject> AllWeapons
    {
        get { return weapons; }
    }

    // finds the server weapon by checking the
    // .IsServer property for every weapon object.
    public static WeaponControllerObject ServerWeapon
    {
        get { return weapons.Find(weapon => weapon.IsServer); }
    }

    // utility function which creates a server weapon
    public static WeaponControllerObject CreateServerWeapon()
    {
        return CreateWeapon(null);
    }

    // utility that creates a client weapon object.
    public static WeaponControllerObject CreateClientWeapon(BoltConnection connection)
    {
        return CreateWeapon(connection);
    }

    // utility function which lets us pass in a
    // BoltConnection object (even a null) and have
    // it return the proper weapon object for it.
    public static WeaponControllerObject GetWeapon(BoltConnection connection)
    {
        if (connection == null)
        {
            return ServerWeapon;
        }

        return (WeaponControllerObject) connection.UserData;
    }
}