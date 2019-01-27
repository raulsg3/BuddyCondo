using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MKGameUIManager : MonoBehaviour
{   
    private Text levelText;
    private Text timeText;
    private Vector3 initalVerticalPos;
    private Vector3 initalHorizontalPos;
    private bool hasInitial =false;

    private GameObject UpDownGO;
    private GameObject LeftRigthGO;
    
    [Button]
    public void ChangePowersIU(){
        UpDownGO = GameObject.FindGameObjectWithTag("UpDownUI");
        LeftRigthGO = GameObject.FindGameObjectWithTag("LeftRightUI");
        if (UpDownGO == null || LeftRigthGO == null)
        {
            return;
        }
        if(!hasInitial){
            hasInitial = true;
            initalVerticalPos = UpDownGO.transform.position; 
            initalHorizontalPos = LeftRigthGO.transform.position; 
        }
        // TODO: Hacer esto con una fucking animación
        Vector3 posAux = UpDownGO.transform.position;
        UpDownGO.transform.position = LeftRigthGO.transform.position;
        LeftRigthGO.transform.position = posAux;
    }

    public void ResetUI(){
        UpDownGO.transform.position =initalVerticalPos;
        LeftRigthGO.transform.position = initalHorizontalPos;
    }

    public void UpdateLevelText(string level){
        // if(levelText == null){
        //     GameObject levelTextGO = GameObject.FindGameObjectWithTag("LevelTextTag");
        //     if(levelTextGO == null)
        //     {
        //         Debug.LogError("No se encontro el levelText. return");
        //         return;
        //     }
        //     levelText = GetComponent<Text>();
        // }
        // Debug.Log(levelText);
        SingletonRichardSalvation.Instance.levelText.text = ""+level;
        // levelText.text = "Level " + level;
    }

    public void UpdateTimeText(float time){
        // if(timeText == null){
        //     GameObject timeTextGO = GameObject.FindGameObjectWithTag("TimeTextTag");
        //     if(timeText == null)
        //     {
        //         return;
        //     }
        //     timeText = GetComponent<Text>();
        // }

        string minutes = Mathf.Floor(time / 60).ToString("00");
        string seconds = (time % 60).ToString("00");

        string textMinSec = string.Format("{0}:{1}", minutes, seconds);

        // timeText.text = "Time: " + time;
        SingletonRichardSalvation.Instance.timeText.text = textMinSec;

    }

    void OnDestroy(){
        levelText = null;
        timeText = null;
    }
}
