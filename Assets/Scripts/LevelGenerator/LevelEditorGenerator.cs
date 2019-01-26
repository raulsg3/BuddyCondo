using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorGenerator : MonoBehaviour
{
    public float xSize;
    public float ySize;

    public GameObject cellPrefab;
    public Transform cellParent;

    [Button]
    public void SaveJson(){

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
