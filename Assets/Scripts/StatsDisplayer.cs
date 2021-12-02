using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplayer : MonoBehaviour
{
    public Text textName;
    public Text textWins;
    public Text textLosses;

    public PlayerData pd;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerData p = new PlayerData("Harry");
        //Prime(p);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Prime(PlayerData pd){
        this.pd = pd;
        textName.text = pd.name;
        textWins.text = pd.wins.ToString();
        textLosses.text = pd.losses.ToString();
    }
}
