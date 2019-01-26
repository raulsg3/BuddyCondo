using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MKLevelLoader : MonoBehaviour
{
    public string testLevelName;
    public LevelJson currentLevelJson;
    public GameObject characterPrefab;
    public Transform levelContainer;

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
                    GameObject characterInstance = Instantiate(characterPrefab,levelContainer);
                    characterInstance.transform.position = MKGame.Instance.GetGameManager().GetWorldPosition(mKCellData.PosX,mKCellData.PosY); 
                    MKCharacterController characterController = characterInstance.GetComponent<MKCharacterController>();
                    if(mKCellData.Type == EMKCellType.Player1){
                        characterController.m_PlayerNumber = EMKPlayerNumber.Player1;
                        characterController.playerPower = PlayerPower.VERTICAL;
                        MKGame.Instance.GetCharacterManager().player1 = characterController; 
                    } 
                    else
                    {
                        characterController.m_PlayerNumber = EMKPlayerNumber.Player2;
                        characterController.playerPower = PlayerPower.HORIZONTAL;
                        MKGame.Instance.GetCharacterManager().player2 = characterController; 

                    } 
                    MKGame.Instance.GetCharacterManager().SetPlayerActiveStatus(false);
                    characterController.m_CharacterIndexPositionX = mKCellData.PosX;
                    characterController.m_CharacterIndexPositionY = mKCellData.PosY;
                }
            }
        }

        MKGame.Instance.GetFlowManager().NextLevelLoaded();
    }

    public void DestroyLevel(){
        foreach (Transform child in levelContainer)
        {
            DestroyImmediate(child.gameObject);
        }
    }
    
}

[System.Serializable]
public class LevelJson{
    public LevelJson(){
        this.cellDataList = new List<MKCellData>();
    }
    public List<MKCellData> cellDataList; 
}
