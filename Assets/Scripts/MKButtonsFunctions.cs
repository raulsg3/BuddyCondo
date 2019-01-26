using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKButtonsFunctions : MonoBehaviour
{
    public void PlayAgainButton()
    {
        Debug.Log("To implement playagain.");
    }

    public void MainMenuButton()
    {
        MKUIData uiDataGO = GameObject.FindGameObjectWithTag("UIData").GetComponent<MKUIData>();
        uiDataGO.GetUIGO("rankingUI").SetActive(false);
        uiDataGO.GetUIGO("menupanelgoUI").SetActive(true);
        uiDataGO.GetUIGO("titleUI").SetActive(true);
    }

    public void RankingButton()
    {
        MKUIData uiDataGO = GameObject.FindGameObjectWithTag("UIData").GetComponent<MKUIData>();
        uiDataGO.GetUIGO("rankingUI").SetActive(true);
        uiDataGO.GetUIGO("menupanelgoUI").SetActive(false);
        uiDataGO.GetUIGO("titleUI").SetActive(false);
    }
}
