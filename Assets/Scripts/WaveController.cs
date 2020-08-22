using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    public AudioClip bossAlarm;
    public Transform[] spawnSpots = new Transform[8];
    public Text timeDisplayer;
    public GameObject[] Monsters;
    public GameObject[] Bosses;
    int timeToNextBoss = 60;
    int wave = 0;
    float second=0;
    float miniWaveRespawn = 10;
    void Update()
    {
        second+= Time.deltaTime;
        if(second>1)
        {
            miniWaveRespawn++;
            second--;
            timeToNextBoss--;
        }
        if(miniWaveRespawn > 10-wave/2)
        {
            miniWaveRespawn -= 10;
            respawnEnemy();
        }
        if(timeToNextBoss < 0)
        {
            respawnBoss();
        }
        timeDisplayer.text = (timeToNextBoss / 60).ToString() + ":" + (timeToNextBoss % 60 < 10 ? "0" : "")
            + (timeToNextBoss % 60).ToString();
    }
    void respawnEnemy()
    {
        for(int i=0; i<spawnSpots.Length; i++)
        {
            GameObject enemy = Instantiate(Monsters[Random.Range(0, wave+3)]);
            enemy.transform.position = spawnSpots[i].position;
        }
    }
    void respawnBoss()
    {
        Transform p = GameObject.Find("Player").transform;
        AudioSource.PlayClipAtPoint(bossAlarm, p.position, 5);
        if (wave > 7*2)
            wave = 7*2;
        timeToNextBoss = 60;
        GameObject boss = Instantiate(Bosses[wave+Random.Range(0,1)]);
        boss.transform.position = spawnSpots[Random.Range(0, 7)].position;
        wave+=2;
    }
}
