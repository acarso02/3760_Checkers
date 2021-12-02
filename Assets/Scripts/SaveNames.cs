using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public static class SaveNames
{

    public static void SaveIntoJson(pListWrapper plw)
    {
        foreach (PlayerData p in plw.pList)
        {
            Debug.Log(p.name);
        }

        string player = JsonUtility.ToJson(plw);
        System.IO.File.WriteAllText(Application.dataPath + "/PlayerData.json", player);
    }

    public static List<PlayerData> LoadFromJsonExternal()
    {
<<<<<<< Updated upstream
        string data = System.IO.File.ReadAllText(Application.persistentDataPath + "/PlayerData.json");
        pList = JsonUtility.FromJson<List>(data);
        Debug.Log(pList);
=======
        pListWrapper plw = new pListWrapper();
        string data = System.IO.File.ReadAllText(Application.dataPath + "/PlayerData.json");
        JsonUtility.FromJsonOverwrite(data, plw);
        //Debug.Log("loadfromjson");a
        return plw.pList;
>>>>>>> Stashed changes
    }

    public static pListWrapper LoadFromJson()
    {
        pListWrapper plw = new pListWrapper();
        string data = System.IO.File.ReadAllText(Application.dataPath + "/PlayerData.json");
        JsonUtility.FromJsonOverwrite(data, plw);
        //Debug.Log("loadfromjson");
        return plw;
    }

    public static void AddPlayer(string name, pListWrapper plw)
    {
        foreach(PlayerData pd in plw.pList){
            if(name == pd.name){
                return;
            }
        }
        if(name == ""){
            return;
        }

        PlayerData p = new PlayerData(name);
        plw.pList.Add(p);
        SaveIntoJson(plw);
    }
     
    public static void UpdateScores(string winner, string loser, pListWrapper plw)
    {
        if(winner == ""){
            winner = "Player 1";
        }
        if(loser == ""){
            loser = "Player 2";
        }
        foreach (PlayerData p in plw.pList)
        {
            if (p.name.Equals(winner))
            {
                p.wins++;
                SaveIntoJson(plw);
            }
            if (p.name.Equals(loser))
            {
                p.losses++;
                SaveIntoJson(plw);
            }
        }
    }

    public static void TestStartup()
    {
        pListWrapper plw = new pListWrapper();
        plw = LoadFromJson();
        //AddPlayer("Jordan", plw);
        //AddPlayer("Tyler", plw);
        //UpdateScores("Jordan", "Tyler", plw);
        //UpdateScores("Jordan", "Tyler", plw);

    }
}

[Serializable]
public class pListWrapper
{
    [SerializeField]
    public List<PlayerData> pList = new List<PlayerData>();
}

[Serializable]
public class PlayerData
{
    [SerializeField]
    public string name;
    [SerializeField]
    public int wins;
    [SerializeField]
    public int losses;

    public PlayerData(string playerName)
    {
        name = playerName;
        wins = 0;
        losses = 0;
    }

}

