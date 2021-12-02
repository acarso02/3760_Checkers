using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    public static string activeRedPlayer = "Player 1";
    public static string activeBlackPlayer = "Player 2";
    public float startGameDelayInSeconds;

    public void StartGame() {
        StartCoroutine(StartGameAfterDelay());
    }

    void Start(){
        Dropdown redDropDown = GameObject.Find("DropdownRed").GetComponent<Dropdown>();
        Dropdown blackDropDown = GameObject.Find("DropdownBlack").GetComponent<Dropdown>();

        redDropDown.options.Clear();
        blackDropDown.options.Clear();
        
        pListWrapper plw = SaveNames.LoadFromJson();
        foreach(PlayerData p in plw.pList){
            redDropDown.options.Add(new Dropdown.OptionData() {text = p.name});
            blackDropDown.options.Add(new Dropdown.OptionData() {text = p.name});
        }

        redDropDown.onValueChanged.AddListener(delegate { DropdownItemSelected(redDropDown, "Red");} );
        blackDropDown.onValueChanged.AddListener(delegate { DropdownItemSelected(blackDropDown, "Black");} );
        
    }

    void DropdownItemSelected(Dropdown dropdown, string color){
        int index = dropdown.value;
        if(color == "Red"){
            activeRedPlayer = dropdown.options[index].text;
        }
        if(color == "Black"){
            activeBlackPlayer = dropdown.options[index].text;
        }
    }

    private IEnumerator StartGameAfterDelay() {

        GameObject redTextInput = GameObject.Find("Red Name");
        InputField redInputField = redTextInput.GetComponent<InputField>();
        if (!string.IsNullOrEmpty(redInputField.text))
        {
            Debug.Log(redInputField.text);
            activeRedPlayer = redInputField.text;
        }
        pListWrapper plw = SaveNames.LoadFromJson();
        SaveNames.AddPlayer(activeRedPlayer, plw);

        GameObject blackTextInput = GameObject.Find("Black Name");
        InputField blackInputField = blackTextInput.GetComponent<InputField>();
        if (!string.IsNullOrEmpty(blackInputField.text))
        {
            Debug.Log(blackInputField.text);
            activeBlackPlayer = blackInputField.text;
        }
        SaveNames.AddPlayer(activeBlackPlayer, plw);

        yield return new WaitForSeconds(startGameDelayInSeconds);
        //Set teams, probably by assigning a static variable?
        //Or should this happen when the game scene loads in? Bring it up next meeting
        SceneManager.LoadScene("Scenes/GameBoard");
    }

    public static void UpdatePlayers(string winner){
        pListWrapper plw = SaveNames.LoadFromJson();
        if(winner == "Red"){
            SaveNames.UpdateScores(activeRedPlayer, activeBlackPlayer, plw);
        }
        else{
            SaveNames.UpdateScores(activeBlackPlayer, activeRedPlayer, plw);
        }
    }
}