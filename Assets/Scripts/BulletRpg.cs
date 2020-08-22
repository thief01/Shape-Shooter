using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletRpg : MonoBehaviour
{
    public ParticleSystem boom;
    public Transform bul;
    float speed = 50;
    int damage = 500;
    Vector2 target;
    private void Start()
    {
        boom.Pause();
    }
    void Update()
    {
        bul.Translate(Vector3.right * speed * Time.deltaTime);  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 13 || collision.gameObject.layer == 9)
        {
            return;
        }
        SpriteRenderer  s = this.gameObject.GetComponent<SpriteRenderer>();
        s.sprite = null;
        speed = 0;
        explode();
        Destroy(this.gameObject , 1); 
    }
    void explode()
    {
        boom.Play();
        RaycastHit2D[] q = Physics2D.CircleCastAll(this.transform.position, 2, new Vector2(0, 0),0,LayerMask.GetMask("Monsters") );
        for(int i=0; i<q.Length; i++)
        {
            EnemyUnitControll EUC = q[i].transform.GetComponent<EnemyUnitControll>();
            EUC.getDamage(damage);                
        }
        
    }
    public void setDmg(int value)
    {
        damage = value;
    }
}
