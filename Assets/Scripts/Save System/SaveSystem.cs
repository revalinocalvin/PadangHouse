using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    //Scripts
    public GameManager GM;
    public DayManager DM;

    public void SaveToJson()
    {
        PlayerSaveData data = new PlayerSaveData();

        //Data
        data.totalStars = GM.totalStars;
        data.dayValue = DM.dayValue;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/Save/PlayerSaveFile.json", json);
        Debug.Log("Saved");
    }

    public void LoadFromJson()
    {
        if (File.Exists(Application.dataPath + "/Save/PlayerSaveFile.json"))
        {
            string json = File.ReadAllText(Application.dataPath + "/Save/PlayerSaveFile.json");
            PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);
            Debug.Log("Loaded");

            //Data
            GM.totalStars = data.totalStars;
            DM.dayValue = data.dayValue;

            DM.SetDay();
        }
        else
        {
            Debug.Log("No save file");
        }
    }

    private void OnApplicationQuit()
    {
        SaveToJson();
    }
}
