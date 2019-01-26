using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MKLevelLoader : MonoBehaviour
{
    public string testLevelName;
    public LevelJson currentLevelJson;
    public GameObject characterPrefab;

    [Button]
    public void Debug_LoadLevelTest(){
        LoadLevel(testLevelName);
    }
    [Button]
    public void Debug_Save(){
    File.WriteAllText(Application.streamingAssetsPath + "/level_" + testLevelName + ".json",JsonUtility.ToJson(currentLevelJson)); 

    }
    public void LoadLevel(string levelName){

        // Read the json from the file into a string
        string jsonString = File.ReadAllText(Application.streamingAssetsPath + "/level_" + levelName + ".json"); 
        Debug.Log(jsonString);
        LevelJson loadedData = new LevelJson(); 
        loadedData = JsonUtility.FromJson<LevelJson>(jsonString);

        //Los muebles los intancia lin e ignora los playesr
        MKGame.Instance.GetFurnitureManager().CreateFurniture(loadedData.cellDataList);

        foreach (MKCellData mKCellData in loadedData.cellDataList)
        {   
            //SI es jugador lo instancio
            if(mKCellData.Type == EMKCellType.Player1 || mKCellData.Type == EMKCellType.Player2){
                // if(true){
                if(MKGame.Instance.GetGameManager().PlaceInCell(mKCellData.PosX,mKCellData.PosY,mKCellData.Type,mKCellData.Color)){
                    GameObject characterInstance = Instantiate(characterPrefab);
                    characterInstance.transform.position = MKGame.Instance.GetGameManager().GetWorldPosition(mKCellData.PosX,mKCellData.PosY); 
                    MKCharacterController characterController = characterInstance.GetComponent<MKCharacterController>();
                    if(mKCellData.Type == EMKCellType.Player1){
                        characterController.m_PlayerNumber = EMKPlayerNumber.Player1;
                        characterController.playerPower = PlayerPower.HORIZONTAL;
                    } 
                    else
                    {
                        characterController.m_PlayerNumber = EMKPlayerNumber.Player2;
                        characterController.playerPower = PlayerPower.VERTICAL;
                    } 
                    characterController.m_CharacterIndexPositionX = mKCellData.PosX;
                    characterController.m_CharacterIndexPositionY = mKCellData.PosY;
                }
            }
        }

        //TODO llamar a kaito para decirle que se ha cargado el nivel
    }

   
    
}

[System.Serializable]
public class LevelJson{
    public LevelJson(){
        this.cellDataList = new List<MKCellData>();
    }
    public List<MKCellData> cellDataList; 
}
