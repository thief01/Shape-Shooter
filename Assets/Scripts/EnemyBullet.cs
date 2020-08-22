using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Vector2 targetPosition;
    int damage;
    float speed;
    void Update()
    {
        move();
    }
    private void move()
    {
        Vector2 t = this.transform.position;
        float distance = Vector2.Distance(t, targetPosition);
        Vector2 off = (targetPosition-t) / distance;
        this.transform.Translate(off * speed * Time.deltaTime);

        if (distance < 0.5f)
        {
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 13)
        {
            collision.gameObject.SendMessage("getDamage", damage);
        }
        else if( collision.gameObject.layer == 8 || collision.gameObject.layer == 11)
        {
            return;
        }
        Destroy(this.gameObject);
    }

    public void setTargetPosition(Vector3 value)
    {
        targetPosition = value;
    }
    public void setDamage(int value)
    {
        damage = value;
    }
    public void setSpeed(int value)
    {
        speed = value;
    }
}
