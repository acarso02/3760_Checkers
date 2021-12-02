using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStats : MonoBehaviour
{
    public InventoryDisplay inventoryDisplayPrefab;
    public void Start(){
        List<PlayerData> playerData = new List<PlayerData>();
        playerData = SaveNames.LoadFromJsonExternal();
        InventoryDisplay inventory = (InventoryDisplay)Instantiate(inventoryDisplayPrefab);
        inventory.Prime(playerData);
    }
}
