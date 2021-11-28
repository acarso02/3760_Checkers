using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


public class SaveLoadData : MonoBehaviour
{
    [SerializeField] private List<PlayerData> pList = new List<PlayerData>();

    public void SaveIntoJson()
    {
        string player = JsonUtility.ToJson(pList);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/PlayerData.json", player);
    }

    public void LoadFromJson()
    {
        string data = System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
        pList = JsonUtility.FromJson<List<PlayerData>>(data);
        Debug.Log(pList);
    }

    public void AddPlayer(string name)
    {
        PlayerData p = new PlayerData(name);
        pList.Add(p);
        SaveIntoJson();
    }

    public void UpdateScores(string winner, string loser)
    {
        foreach (PlayerData p in pList)
        {
            if (p.name.Equals(winner))
            {
                p.wins++;
            }
            if (p.name.Equals(loser))
            {
                p.losses--;
            }
        }
        SaveIntoJson();
    }
}



[System.Serializable]
public class PlayerData
{
    public string name;
    public int wins;
    public int losses;


    public PlayerData(string playerName)
    {
        name = playerName;
    }

}

