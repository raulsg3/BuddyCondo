using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKUIData : MonoBehaviour
{
    public GameObject rankingUI;
    public GameObject victoryUI;
    public GameObject startingUI;

    public GameObject GetUIGO(string uiElement)
    {
        switch (uiElement)
        {
            case "rankingUI":
                return rankingUI;
                break;
            case "victoryUI":
                return victoryUI;
                break;
            case "startingUI":
                return startingUI;
                break;
            default:
                return null;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
