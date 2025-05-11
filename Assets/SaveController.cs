using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class SaveController : MonoBehaviour
{
    private string saveLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveDate.json");
    }

    // Update is called once per frame
    public void SaveGame()
    {
        SaveData saveData = new SaveData()
        {
            //playerPosition = GameObject.FindGameObjectsWithTag("Player").transform.position
        };
    }
}
