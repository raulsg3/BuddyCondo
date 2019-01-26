using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGenerator : MonoBehaviour
{
    public MKCellData mKCellData;
    
    [Button]
    public void SetFurniture(){
        mKCellData.PrefabName = gameObject.transform.GetChild(0).gameObject.name;
        mKCellData.RotationY = gameObject.transform.GetChild(0).eulerAngles.y;
    }
}
