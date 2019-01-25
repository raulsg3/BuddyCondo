using System.Collections;
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

    public bool CreateFurniture(List<CellData> cellList)
    {
        List<GameObject> furnitureList = MKGame.Instance.GetGameContent().GetFurnitureContent().furnitureList;
        furnitureList = new List<GameObject>();

        foreach (CellData cell in cellList)
        {
            // Initialize the cell in the GameManager


            // Instantiate the GameObjects with the cell data
            GameObject furniture = Instantiate(Resources.Load(cell.PrefabName)) as GameObject;
            Debug.Log("Added: " + cell.PrefabName);
            furnitureList.Add(furniture);
        }


        return false;
    }

    
}
