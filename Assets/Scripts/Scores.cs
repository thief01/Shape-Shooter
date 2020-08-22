using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public struct Score
{
    public long points;
    public string nickname;
}

public static class Scores
{
    public static Score[] scoresData = new Score[10];
    
    public static void Start()
    {
        for(int i=0; i<10; i++)
        { scoresData[i].nickname = "null"; scoresData[i].points = 0; }

        loadScores();
    }

    static void loadScores()
    {
        string path = Application.dataPath + "scores.s";

        if(File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);
            scoresData = bf.Deserialize(fs) as Score[];
            fs.Close();
        }
    }
    static void saveScores()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.dataPath + "scores.s";
        FileStream fs = new FileStream(path, FileMode.Create);

        bf.Serialize(fs, scoresData);
        fs.Close();
    }
    public static Score[] getScores()
    {
        return scoresData;
    }
    public static void addNewScore(string nickname, long points)
    {
        Debug.Log(points);
        for(int i=0; i<10; i++)
        {
            if(points > scoresData[i].points)
            {
                sort(i);
                scoresData[i].points = points;
                scoresData[i].nickname = nickname;
                break;
            }
        }
        Scores.saveScores();
    }
    static void sort(int i)
    {
        for(int j=8; j>i-1; j--)
        {
            scoresData[j + 1].points = scoresData[j].points;
            scoresData[j + 1].nickname = scoresData[j].nickname;
        }
    }
}
