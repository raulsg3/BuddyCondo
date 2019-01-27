using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOFurnitureContent", menuName = "MK/SOFurnitureContent", order = 1)]
public class MKFurnitureContent : ScriptableObject
{
    public List<GameObject> furnitureList;
    public List<GameObject> movableObjectsList;
    public GameObject swapPowersButton;

}
