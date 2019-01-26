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

    public void SetColor(EMKColor baseColor, EMKColor color)
    {
        furnitureColor = colorMap[baseColor];
    }

    private IDictionary<EMKColor, Color> colorMap = new Dictionary<EMKColor, Color>()
    {
        { EMKColor.None, Color.white},
        { EMKColor.Black, Color.black},
        { EMKColor.Blue, Color.blue},
        { EMKColor.Red, Color.red},
        { EMKColor.Yellow, Color.yellow},
        { EMKColor.Green, Color.green}
    };

}
