using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class saveData
{

    public static void savePercentage(shooter me)
    {
        BinaryFormatter papaya = new BinaryFormatter();
        FileStream applesauce = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        PlayerData data = new PlayerData(me);

        papaya.Serialize(applesauce, data);
        applesauce.Close();
    }

    public static void saveAttempts(shooter me)
    {
        BinaryFormatter papaya2 = new BinaryFormatter();
        FileStream applesauce2 = new FileStream(Application.persistentDataPath + "/playerA.sav", FileMode.Create);

        AttemptData data = new AttemptData(me);

        papaya2.Serialize(applesauce2, data);
        applesauce2.Close();
    }

    public static int[,] loadPercent()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter papaya = new BinaryFormatter();
            FileStream applesauce = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            PlayerData data = papaya.Deserialize(applesauce) as PlayerData;

            applesauce.Close();
            return data.percentages;
        }
        else
        {
            return new int[7, 8];
        }

    }

    public static int[,] loadAttempts()
    {
        if (File.Exists(Application.persistentDataPath + "/playerA.sav"))
        {
            BinaryFormatter papaya = new BinaryFormatter();
            FileStream applesauce = new FileStream(Application.persistentDataPath + "/playerA.sav", FileMode.Open);

            AttemptData data = papaya.Deserialize(applesauce) as AttemptData;

            applesauce.Close();
            return data.attempts;
        }
        else
        {
            return new int[7, 8];
        }

    }
}

[Serializable]
public class PlayerData
{
    public int[,] percentages = new int[7 , 8];

    public PlayerData(shooter k)
    {
        //Array.Copy(k.levelList, percentages, 7);
    }
}

[Serializable]
public class AttemptData
{
    public int[,] attempts = new int[7, 8];

    public AttemptData(shooter k)
    {
        //Array.Copy(k.attemptList, attempts, 7);
    }
}
