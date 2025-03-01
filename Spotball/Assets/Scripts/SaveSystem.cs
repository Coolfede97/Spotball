using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveSystem
{
    public static void SavePlayerData(DataManager manager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+"/playerData.crh";
        FileStream stream = new FileStream(path, FileMode.Create);
        PlayerData data = new PlayerData(manager);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData.crh";
        if (!File.Exists(path)) 
        {
            List<string> emptyList = new List<string>();
            DataManager manager = new DataManager(0,0,"1","1","1", emptyList, emptyList, emptyList);

            SavePlayerData(manager);
        }
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);
        PlayerData data = formatter.Deserialize(stream) as PlayerData;
        stream.Close();
        return data;
    }
}
