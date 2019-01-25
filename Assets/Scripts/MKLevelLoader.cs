using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MKLevelLoader : MonoBehaviour
{

    public LevelJson currentLevelJson;

    [Button]
    public void Debug_LoadLevel1(){
        LoadLevel(1);
    }
    [Button]
    public void Debug_Save(){
    File.WriteAllText(Application.streamingAssetsPath + "/level_"+1+".json",JsonUtility.ToJson(currentLevelJson)); 

    }
    public void LoadLevel(int levelNumber){

        // Read the json from the file into a string
        string jsonString = File.ReadAllText(Application.streamingAssetsPath + "/level_"+levelNumber+".json"); 
        Debug.Log(jsonString);
        LevelJson loadedData = new LevelJson(); 
        loadedData = JsonUtility.FromJson<LevelJson>(jsonString);
        Debug.Log(loadedData.furnitureList.Count);

        foreach (FurnitureTest item in loadedData.furnitureList){
            Debug.Log("create");
           GameObject gameObjectInstance = Instantiate(Resources.Load(item.prefabName)) as GameObject;         
        }        
    }
    
}

[System.Serializable]
public class FurnitureTest{
    public int i;
    public int j;
    public string prefabName;
}


[System.Serializable]
public class LevelJson{
    public LevelJson(){
        this.furnitureList = new List<FurnitureTest>();
    }
    public List<FurnitureTest> furnitureList; 
}
