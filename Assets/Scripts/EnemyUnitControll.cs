using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
public class EnemyUnitControll : MonoBehaviour
{
    public Item[] dropItems;
    public AudioClip aDash;
    public AudioClip aShot;

    public GameObject bullet;
    public UnityEngine.Experimental.Rendering.Universal.Light2D lightObject;

    public int hp = 300;
    public int dmg = 300;
    public int points = 500;
    public float speed = 300;

    public int bulletSpeed = 20;
    public bool shoots = false;
    public float speedAttack = 1;
    public float range = 0;

    public bool Light = true;
    public bool blinkLight = false;
    public bool boss = false;

    Vector2 off;
    float speedBoostFromDash = 1;
    float cooldownAttack=0;
    PolygonCollider2D m;
    void Start()
    {
        m = GetComponent<PolygonCollider2D>();
    }
    void Update()
    {
        if(1 < speedBoostFromDash)
        {
            speedBoostFromDash -= 10 * Time.deltaTime;
        }
        else
        {
            m.isTrigger = false;
            speedBoostFromDash = 1;
        }
        
        cooldownAttack -= Time.deltaTime;
        AI();
    }

    void AI()
    {
        if (blinkLight)
            lightControl();
        Transform t = GameObject.Find("Player").transform;
        Transform t2 = this.gameObject.transform;
        float distance = Vector2.Distance(t2.position, t.position);
 
        if (range > distance && speedBoostFromDash == 1)
        {
            if (cooldownAttack < 0)
            {
                if (shoots)
                {
                    shot();
                }
                else
                {
                    dash();
                    off = (t.position - t2.position) / distance;
                }
            }
        }
        else
        {
            
            move((t.position-t2.position)/distance);
        }
        
    }
    void move(Vector2 off)
    {
        if(speedBoostFromDash > 1)
        {
            this.gameObject.transform.Translate(this.off * Time.deltaTime * speed * speedBoostFromDash);
            return;
        }
        this.gameObject.transform.Translate(off*Time.deltaTime*speed*speedBoostFromDash);
    }
    void shot()
    {
        GameObject r = Instantiate(bullet);
        r.transform.position = this.gameObject.transform.position;
        r.SendMessage("setTargetPosition", GameObject.Find("Player").transform.position);
        r.SendMessage("setDamage", dmg);
        r.SendMessage("setSpeed", bulletSpeed);
        cooldownAttack = 1f / speedAttack;
 
        AudioSource.PlayClipAtPoint(aShot, transform.position);
    }
    void dash()
    {
        m.isTrigger = true;
        AudioSource.PlayClipAtPoint(aDash, transform.position);
        cooldownAttack = 1f / speedAttack;
        speedBoostFromDash = 10;
    }
    bool increasing;
    float intesiveValue;
    void lightControl()
    {
        if (intesiveValue < 0)
        {
            increasing = true;
        }
        else if (intesiveValue > 1)
        {
            increasing = false;
        }
        intesiveValue += (increasing ? 1  : -1 ) *Time.deltaTime ;
        lightObject.intensity = intesiveValue;
    }
    public void getDamage(int value)
    {
        hp -= value;
        checkDead();
    }
    void checkDead()
    {
        if (hp < 1)
        {
            Player p = GameObject.Find("Player").GetComponent<Player>();
            p.addScores(points);
            dropItem();
            Destroy(this.gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 13)
        {
            collision.gameObject.SendMessage("getDamage", dmg);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 13)
        {
            m.isTrigger = false;
        }
    }
    void dropItem()
    {
        int items = dropItems.Length;
        if (items == 0)
        {
            return;
        }
        if(boss == true)
        {
            for(int i=0; i<items; i++)
            {
                dropItems[i].transform.position = this.transform.position;
                Item a = Instantiate(dropItems[i]);
                Destroy(a.gameObject, 30);
            }
        }
        int r = Random.Range(0, 100);
        if ( r < 50 )
        {
            items = Random.Range(0, items);
            dropItems[items].transform.position = this.transform.position;
            Item a = Instantiate(dropItems[items]);
            Destroy(a.gameObject, 30);
        }
    }
}
