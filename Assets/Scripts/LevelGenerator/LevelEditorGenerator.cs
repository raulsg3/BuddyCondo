using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelEditorGenerator : MonoBehaviour
{
    public float xSize;
    public float ySize;

    public string levelNumber;

    public GameObject cellPrefab;
    public Transform cellParent;

    public LevelJson currentLevelJson;

    [Button]
    public void SaveJson(){
        currentLevelJson = new LevelJson();
        foreach (Transform item in cellParent)
        {
            item.GetComponent<CellGenerator>().SetFurniture();
            currentLevelJson.cellDataList.Add(item.GetComponent<CellGenerator>().mKCellData);
        }
        
       File.WriteAllText(Application.streamingAssetsPath + "/level_" + levelNumber + "_" + System.DateTime.Now.Minute + ".json", JsonUtility.ToJson(currentLevelJson)); 

    }

  
    public void Delete(){
        List<GameObject> destroyList = new List<GameObject>(); 
        foreach (Transform item in cellParent)
        {
            destroyList.Add(item.gameObject);
        }
        foreach (GameObject item in destroyList)
        {
            DestroyImmediate(item);
        }

    }
    [Button]
    public void CreateGrid()
    {
        Delete();


        for (uint i = 0; i < xSize; i++)
        {
            for (uint j = 0; j < ySize; j++)
            {
                GameObject cellInstance = Instantiate(cellPrefab,cellParent);
                cellInstance.transform.position = new Vector3(i + 0.5f,0f,j+0.5f);
                cellInstance.gameObject.name = "i " + i + " j "+j;
                cellInstance.GetComponent<CellGenerator>().mKCellData.PosX = i;
                cellInstance.GetComponent<CellGenerator>().mKCellData.PosY = j;

            }
        }
    }

}
