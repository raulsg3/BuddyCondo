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

    public bool CreateFurniture(List<FurnitureInfo> furnitureList)
    {
        return false;
    }

    public struct FurnitureInfo
    {
        private int PosX { get; set; }
        private int PosY { get; set; }
        private string PrefabName { get; set; }
        private EColor Color { get; set; }
        private EFurnitureType FurnitureType { get; set; }
    }

    // Different colors for furniture
    private enum EColor
    {
        None = 0,
        Black,
        Blue,
        Red,
        Yellow
    }

    // Furniture type
    private enum EFurnitureType
    {
        Movable = 0,
        No_movable
    }
}
