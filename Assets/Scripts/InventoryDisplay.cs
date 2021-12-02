using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{

    public Transform targetTransform;
    public StatsDisplayer statsDisplayerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        List<PlayerData> pdl = new List<PlayerData>();
        pdl = SaveNames.LoadFromJsonExternal();
        this.Prime(pdl);
    }

    public void Prime(List<PlayerData> pdl){
        Debug.Log("InventoryDisplay Prime()");
        foreach (PlayerData pd in pdl){
            StatsDisplayer display = (StatsDisplayer)Instantiate(statsDisplayerPrefab);
            display.transform.SetParent(targetTransform, false);
            display.Prime(pd);
        }
    }
}
