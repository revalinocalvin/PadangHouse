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
    private GameManager GM;
    private DayManager DM;
    public static string savePath;

    private void Awake()
    {
        GM = GameManager.Instance;
        DM = DayManager.Instance;
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public void SaveToJson()
    {
        PlayerSaveData data = new PlayerSaveData();

        // Data
        data.totalStars = GM.totalStars;
        data.dayValue = DM.dayValue;

        string json = JsonUtility.ToJson(data, true);

        // Write to the correct path
        File.WriteAllText(savePath, json);
        Debug.Log("Saved to " + savePath);
    }

    public void LoadFromJson()
    {

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);
            Debug.Log("Loaded");

            // Data
            GM.totalStars = data.totalStars;
            DM.dayValue = data.dayValue;

            DM.SetDay();
        }
        else
        {
            Debug.Log("No save file");
        }
    }

    /*private void OnApplicationQuit()
    {
        SaveToJson();
    }*/
}
