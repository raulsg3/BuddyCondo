﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKFurnitureManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check Positions

        //
    }

    public bool CreateFurniture(List<MKCellData> cellList)
    {
        List<GameObject> furnitureList = MKGame.Instance.GetGameContent().GetFurnitureContent().furnitureList;
        furnitureList = new List<GameObject>();     

        foreach (MKCellData cell in cellList)
        {
            // Initialize the cell in the GameManager
            MKGame.Instance.GetGameManager().PlaceInCell(cell.PosX, cell.PosY, cell.Type);

            // Instantiate the GameObjects with the cell data
            GameObject furnitureObject = Instantiate(Resources.Load(cell.PrefabName)) as GameObject;
            furnitureObject.transform.position = MKGame.Instance.GetGameManager().GetWorldPosition(cell.PosX, cell.PosY);
            furnitureObject.transform.Rotate(0f, cell.RotationY, 0f);
            furnitureObject.GetComponent<MKColorController>().SetColor(cell.Color);
            Debug.Log("Added: " + cell.PrefabName);
            furnitureList.Add(furnitureObject);
        }

        return true;
    }

    
}
