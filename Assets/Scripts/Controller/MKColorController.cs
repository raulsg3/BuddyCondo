using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKColorController : MonoBehaviour
{
    private Color furnitureColor;

    private void Awake()
    {
        //colorMap
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(MKCellData.EMKColor baseColor, MKCellData.EMKColor color)
    {
        furnitureColor = colorMap[baseColor];
    }

    private IDictionary<MKCellData.EMKColor, Color> colorMap = new Dictionary<MKCellData.EMKColor, Color>()
    {
        { MKCellData.EMKColor.None, Color.white},
        { MKCellData.EMKColor.Black, Color.black},
        { MKCellData.EMKColor.Blue, Color.blue},
        { MKCellData.EMKColor.Red, Color.red},
        { MKCellData.EMKColor.Yellow, Color.yellow},
        { MKCellData.EMKColor.Green, Color.green}
    };

}
