using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

[System.Serializable]
public struct sWeapon
{
    public bool avaible;
    public int ammo;
    // stats
    public float delay;
    public float angle;
    public bool burstMode;
    public int damage;
}
public class Player : MonoBehaviour
{
    public Canvas DeathScreen;
    public AudioClip aShot;
    [HideInInspector]
    public int health;
    public long scores;
    public UnityEngine.Experimental.Rendering.Universal.Light2D flashPoint;
    public GameObject bullet;
    public GameObject rpgBullet;
    public GameObject tower;
    public Transform shootingPosition;
    public Transform player;
    [HideInInspector]
    public int usingWeapon=0;
    float shotsDelay = 0f;
    public sWeapon[] weapons = new sWeapon[9];

    void Start()
    {
        health = 100;
        scores = 0;
    }

    void renderBullets()
    {
        sWeapon mW = weapons[usingWeapon];

        if (shotsDelay > 0 || mW.ammo < 1)
            return;
        if(usingWeapon == 7)
        {
            GameObject t = Instantiate(tower);
            t.transform.position = this.transform.position;
            t.gameObject.GetComponent<ShootingTower>().Damage = mW.damage;
            weapons[usingWeapon].ammo--;
            shotsDelay = mW.delay;
            return;
        }
        if(usingWeapon == 5)
        {
            AudioSource.PlayClipAtPoint(aShot, transform.position);
            GameObject t = Instantiate(rpgBullet);
            t.transform.position = this.transform.position;
            t.transform.rotation = player.rotation;
            t.gameObject.GetComponent<BulletRpg>().setDmg(mW.damage);
            weapons[usingWeapon].ammo--;
            shotsDelay = mW.delay;
            return;
        }

        flashPoint.enabled = true;
        GameObject lp = Instantiate(flashPoint.gameObject);
        lp.transform.position = shootingPosition.position;
        Destroy(lp, 0.04f);
        flashPoint.enabled = false;

        

        for (int i= mW.burstMode ? 5 : 1; i > 0; i--)
        {
            AudioSource.PlayClipAtPoint(aShot, transform.position);
            Vector3 angle = new Vector3(0, 0, Random.Range(mW.angle / 2 * -1, mW.angle / 2));
            GameObject a = Instantiate(bullet);
            a.SendMessage("setDmg", mW.damage);
            a.transform.position = shootingPosition.position;
            a.transform.eulerAngles = player.eulerAngles + angle;
            Destroy(a, 30);
        }
        if(usingWeapon != 0)
             weapons[usingWeapon].ammo--;
        shotsDelay = mW.delay;
    }
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            renderBullets();
        }
        for(int i=0; i<8; i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1+i))
            {
                if(weapons[i].avaible)
                {
                    usingWeapon = i;
                }
            }
        }
        shotsDelay -= Time.deltaTime;
    }
    public void unlockWeapon(int type , int value)
    {
        weapons[type].avaible = true;
        weapons[type].ammo += value;
    }

    public void addAmmo(int type , int ammo)
    {
        weapons[type].ammo += ammo;
    }
    public void getHeal(int value)
    {
        if( health < value)
        {
            health = value;
        }
    }
    public void getDamage(int value)
    {
        health -= value;
        if(health < 0)
        {
            Time.timeScale = 0;
            DeathScreen.gameObject.SetActive(true);
        }
    }
    public void addScores(int value)
    {
        scores += value;
    }
}
