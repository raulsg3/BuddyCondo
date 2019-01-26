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
        if(mKCellData != null &&
            (mKCellData.Type == EMKCellType.Target || mKCellData.Type == EMKCellType.Movable))
        {
            if(mKCellData.Color == EMKColor.Blue)
                Gizmos.color = Color.blue;
            else if(mKCellData.Color == EMKColor.Green)
                Gizmos.color = Color.green;
            else if(mKCellData.Color == EMKColor.Red)
                Gizmos.color = Color.red;

            if (mKCellData.Type == EMKCellType.Movable)
            {
                Gizmos.DrawWireSphere(transform.position, 0.5f);
            }
            else // Target
            {
                Gizmos.DrawWireSphere(transform.position, 1.0f);
            }
        }

        if (mKCellData != null && mKCellData.Type == EMKCellType.Button)
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(0.9f, 1.0f, 0.9f));
        }

        if (mKCellData != null && mKCellData.Type == EMKCellType.Player1 ){
            Gizmos.DrawWireCube(transform.position,new Vector3(0.5f,1.5f,0.5f));
        }
            if(mKCellData != null && mKCellData.Type == EMKCellType.Player2 ){
            Gizmos.DrawWireCube(transform.position,new Vector3(0.5f,1.5f,0.5f));
        }
    }
}
