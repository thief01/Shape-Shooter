using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTower : MonoBehaviour
{
    public GameObject bullet;
    public AudioClip aShot;
    public Transform shootingPosition;
    public float speedAttack;
    public int Damage;

    float cooldownAttack=0;
    void Start()
    {
        Destroy(this.gameObject, 15);
    }
    void Update()
    {
        cooldownAttack -= Time.deltaTime;
        physic();
        
    }
    void physic()
    {
        this.gameObject.transform.Rotate(new Vector3(0, 0, 500 * Time.deltaTime));
        if (cooldownAttack < 0)
        {
            AudioSource.PlayClipAtPoint(aShot, transform.position);
            GameObject a = Instantiate(bullet);
            a.SendMessage("setDmg", Damage);
            a.transform.position = shootingPosition.position;
            a.transform.eulerAngles = this.transform.eulerAngles;
            Destroy(a, 0.5f);
            cooldownAttack = 1 / speedAttack;
        }
    }
}
