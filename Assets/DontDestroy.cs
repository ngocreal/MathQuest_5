using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DontDestroy : MonoBehaviour
{
    private static GameObject[] persistentObjects = new GameObject[3]; 
    public int objectIndex;
    // Start is called before the first frame update 
    void Awake()
    {
        if (persistentObjects[objectIndex] = null)
        {
            persistentObjects[objectIndex] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (persistentObjects[objectIndex] != gameObject)
        {
            Destroy(gameObject);
        }
    }
}
