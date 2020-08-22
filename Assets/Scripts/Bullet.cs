using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform bul;
    float speed = 35;
    int damage = 500;
    void Update()
    {
        bul.Translate(Vector3.right * speed * Time.deltaTime);  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 11 || collision.gameObject.layer == 14)
            Destroy(this.gameObject);
        else if (collision.gameObject.layer == 8)
        {
            collision.gameObject.SendMessage("getDamage", damage);
            Destroy(this.gameObject);
        }
            
    }
    public void setDmg(int value)
    {
        damage = value;
    }
}
