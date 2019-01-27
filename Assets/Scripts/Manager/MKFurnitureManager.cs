using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKFurnitureManager : MonoBehaviour
{
    public GameObject grabFeedbackPrefab;
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

            // Instantiate the GameObjects with the cell data
            if(!string.IsNullOrEmpty(cell.PrefabName))
            {
                GameObject furnitureObject = Instantiate(Resources.Load(cell.PrefabName)) as GameObject;       
                furnitureObject.transform.parent = MKGame.Instance.GetLevelLoader().levelContainer;

                furnitureObject.transform.Rotate(0f, cell.RotationY, 0f);
                furnitureObject.GetComponent<MKColorController>().SetColor(cell.Color,cell.Type);

                // Add the Position Helper to the GameObject and set The transform
                furnitureObject.AddComponent<MKFurniturePositionHelper>();
                furnitureObject.GetComponent<MKFurniturePositionHelper>().UpdatePosition(cell.PosX, cell.PosY);

                // Save the reference if it's the button
                if (cell.Type == EMKCellType.Button)
                {
                    MKGame.Instance.GetGameContent().GetFurnitureContent().swapPowersButton = furnitureObject;
                }

                furnitureList.Add(furnitureObject);
            }
        }

        MKGame.Instance.GetGameContent().GetFurnitureContent().furnitureList = furnitureList;

        return true;
    }

    public void MoveFurniture(uint oldX, uint oldY, uint newX, uint newY)
    {
        // Debug.Log("MOVING from (" + oldX + "," + oldY + ") to (" + newX + "," + newY + ")");

        GameObject movableFurniture = GetMovableFromPosition(oldX, oldY);

        if(movableFurniture == null)
            Debug.LogError("MoveFurniture CANNOT BE MOVED");

        movableFurniture.GetComponent<MKFurniturePositionHelper>().UpdatePosition(newX, newY);
    }

    public bool PressButton()
    {
        return MKGame.Instance.GetGameContent().GetFurnitureContent().swapPowersButton.GetComponent<MKButtonController>().PressButton();
    }

    private GameObject GetMovableFromPosition(uint posX, uint posY)
    {
        List<GameObject> furnitureList = MKGame.Instance.GetGameContent().GetFurnitureContent().furnitureList;
        foreach (GameObject furniture in furnitureList)
        {
            if(furniture.GetComponent<MKFurniturePositionHelper>() != null &&
                furniture.GetComponent<MKFurniturePositionHelper>().PosX == posX &&
                furniture.GetComponent<MKFurniturePositionHelper>().PosY == posY)
            {
                return furniture;
            }
        }
        return null;
    }
}
