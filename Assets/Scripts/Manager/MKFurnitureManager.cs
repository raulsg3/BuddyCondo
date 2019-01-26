using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKFurnitureManager : MonoBehaviour
{
    public bool CreateFurniture(List<MKCellData> cellList)
    {
        List<GameObject> furnitureList = MKGame.Instance.GetGameContent().GetFurnitureContent().furnitureList;
        furnitureList = new List<GameObject>();

        foreach (MKCellData cell in cellList)
        {
            if(cell.Type == EMKCellType.Player1 || cell.Type == EMKCellType.Player2 ) {
                continue;
            }

            // Initialize the cell in the GameManager
            MKGame.Instance.GetGameManager().PlaceInCell(cell.PosX, cell.PosY, cell.Type, cell.Color);

            // Add it to the movable list if is of the type (Movable)
            if(cell.Type == EMKCellType.Movable)
            {

            }

            // Instantiate the GameObjects with the cell data
            if(!string.IsNullOrEmpty(cell.PrefabName))
            {
                GameObject furnitureObject = Instantiate(Resources.Load(cell.PrefabName)) as GameObject;                
                furnitureObject.transform.Rotate(0f, cell.RotationY, 0f);
                furnitureObject.GetComponent<MKColorController>().SetColor(cell.Color,cell.Type);

                // Add the Position Helper to the GameObject and set The transform
                furnitureObject.AddComponent<MKFurniturePositionHelper>();
                furnitureObject.GetComponent<MKFurniturePositionHelper>().UpdatePosition(cell.PosX, cell.PosY);

                furnitureList.Add(furnitureObject);
            }
        }

        return true;
    }

    public void MoveFurniture(uint oldX, uint oldY, uint newX, uint newY)
    {
        Debug.Log("MOVING from (" + oldX + "," + oldY + ") to (" + newX + "," + newY + ")");

        GameObject movableFurniture = GetMovableFromPosition(oldX, oldY);

        if(movableFurniture == null)
            Debug.LogError("MoveFurniture CANNOT BE MOVED");

        movableFurniture.GetComponent<MKFurniturePositionHelper>().UpdatePosition(newX, newY);
    }

    private GameObject GetMovableFromPosition(uint oldX, uint oldY)
    {
        List<GameObject> furnitureList = MKGame.Instance.GetGameContent().GetFurnitureContent().furnitureList;
        foreach (GameObject furniture in furnitureList)
        {
            if(furniture.GetComponent<MKFurniturePositionHelper>() != null &&
                furniture.GetComponent<MKFurniturePositionHelper>().PosX == oldX &&
                furniture.GetComponent<MKFurniturePositionHelper>().PosY == oldY)
            {
                return furniture;
            }
        }
        return null;
    }
}
