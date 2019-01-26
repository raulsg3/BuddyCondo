using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MKLevelLoader : MonoBehaviour
{
    public int testLevelNumber;
    public LevelJson currentLevelJson;
    public GameObject characterPrefab;

    [Button]
    public void Debug_LoadLevelTest(){
        LoadLevel(testLevelNumber);
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

        //Los muebles los intancia lin e ignora los playesr
        MKGame.Instance.GetFurnitureManager().CreateFurniture(loadedData.cellDataList);

        foreach (MKCellData mKCellData in loadedData.cellDataList)
        {   
            //SI es jugador lo instancio
            if(mKCellData.Type == MKCellData.EMKCellType.Player1){
                if(MKGame.Instance.GetGameManager().PlaceInCell(mKCellData.PosX,mKCellData.PosY,mKCellData.Type)){
                    GameObject characterInstance = Instantiate(characterPrefab);
                    characterInstance.transform.position = MKGame.Instance.GetGameManager().GetWorldPosition(mKCellData.PosX,mKCellData.PosY); 
                    MKCharacterController characterController = characterInstance.GetComponent<MKCharacterController>();
                    if(mKCellData.Type == MKCellData.EMKCellType.Player1) characterController.m_PlayerNumber = EMKPlayerNumber.Player1;
                    else characterController.m_PlayerNumber = EMKPlayerNumber.Player2;
                    characterController.m_CharacterIndexPosition.x = mKCellData.PosX;
                    characterController.m_CharacterIndexPosition.y = mKCellData.PosY;

                }
            }
        }
    }

    public void AllFurnitureLoaded(){
        
    }
    
}

[System.Serializable]
public class LevelJson{
    public LevelJson(){
        this.cellDataList = new List<MKCellData>();
    }
    public List<MKCellData> cellDataList; 
}
