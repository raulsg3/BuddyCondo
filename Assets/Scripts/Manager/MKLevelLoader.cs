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
        LevelJson loadedData = new LevelJson(); 
        try{
            string jsonString = File.ReadAllText(Application.streamingAssetsPath + "/level_" + levelName + ".json"); 
            // Debug.Log(jsonString);
            loadedData = JsonUtility.FromJson<LevelJson>(jsonString);
        }catch{
            Debug.Log("No se encontro el archivo");
            MKGame.Instance.GetFlowManager().NoMoreLevels();
            // Nomore
        }

        //Los muebles los intancia lin e ignora los playesr
        MKGame.Instance.GetFurnitureManager().CreateFurniture(loadedData.cellDataList);

        foreach (MKCellData mKCellData in loadedData.cellDataList)
        {   
            // Debug.Log(mKCellData.Type);
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

        //TODO nomore levels

        MKGame.Instance.GetFlowManager().NextLevelLoaded();
    }

    [Button]
    public void DestroyLevel(){
        List<GameObject> objectToDestroy = new List<GameObject>();
        foreach (Transform child in levelContainer)
        {
            objectToDestroy.Add(child.gameObject);
        }
        foreach (GameObject GO in objectToDestroy)
        {
            DestroyImmediate(GO);
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
