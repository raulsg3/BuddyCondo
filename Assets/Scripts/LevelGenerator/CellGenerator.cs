using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGenerator : MonoBehaviour
{
    public MKCellData mKCellData;
    
    [Button]
    public void SetFurniture(){
        if (transform.childCount > 0)
        {
            mKCellData.PrefabName = gameObject.transform.GetChild(0).gameObject.name;
            mKCellData.RotationY = gameObject.transform.GetChild(0).eulerAngles.y;
        }
    }

    void OnDrawGizmos(){
        if(mKCellData != null && mKCellData.Type == EMKCellType.Target ){
            if(mKCellData.Color == EMKColor.Blue)
                Gizmos.color = Color.blue;
            else if(mKCellData.Color == EMKColor.Green)
                Gizmos.color = Color.green;
            else if(mKCellData.Color == EMKColor.Red)
                Gizmos.color = Color.red;
            
            Gizmos.DrawWireSphere(transform.position,0.5f);
        }
    }
}
