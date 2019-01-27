using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class LevelEditorGenerator : MonoBehaviour
{
    public GameObject GOtoCopy;

    public float xSize;
    public float ySize;

    public string levelNumber;

    public GameObject cellPrefab;
    public Transform cellParent;

    public LevelJson currentLevelJson;

    [Button]
    public void SaveJson(){
        currentLevelJson = new LevelJson();

        SetBordersAsDecoration();

        foreach (Transform item in cellParent)
        {
            item.GetComponent<CellGenerator>().SetFurniture();            
            currentLevelJson.cellDataList.Add(item.GetComponent<CellGenerator>().mKCellData);
        }
        foreach (MKCellData item in currentLevelJson.cellDataList)
        {
            Debug.Log(item.Type);
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

    [Button]
    public void FillSelectionWithGameObject()
    {
        foreach(GameObject go in Selection.gameObjects)
        {
            GameObject newGO = Instantiate(GOtoCopy, Vector3.zero, Quaternion.identity);
            newGO.name = newGO.name.Replace("(Clone)", "");
            newGO.transform.parent = go.transform;
            newGO.transform.localPosition = Vector3.zero;
        }
    }

    [Button]
    public void SetBordersAsDecoration()
    {
        foreach (Transform item in cellParent)
        {
            CellGenerator cell = item.GetComponent<CellGenerator>();
            // Is a border
            if ((cell.mKCellData.PosX == 0) ||
                (cell.mKCellData.PosX == 14) ||
                (cell.mKCellData.PosY == 0) ||
                (cell.mKCellData.PosY == 9) ||
                ((cell.mKCellData.PosX == 7) && (this.levelNumber != "6"))) // -> middle border (except in level 6)
            {
                if(cell.mKCellData.Type != EMKCellType.Button) // do NOT override the Buttons
                    cell.mKCellData.Type = EMKCellType.Decoration;
            }
        }
    }

}
