using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    healBox,
    weapon,
    ammo
}
public class Item : MonoBehaviour
{
    public ItemType type;
    public int value1; // Type of gun
    public int value2; // Ammout of heal or ammo
    public AudioClip pickUpSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();   
        if(collision.gameObject.layer == 13)
        {
            AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
            switch(type)
            {
                case ItemType.healBox:
                    p.getHeal(value2);
                    break;
                case ItemType.weapon:
                    p.unlockWeapon(value1, value2);
                    break;
                case ItemType.ammo:
                    p.addAmmo(value1, value2);
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
