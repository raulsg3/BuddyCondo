using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MKGameUIManager : MonoBehaviour
{   
    private Text levelText;
    private Text timeText;

    [Button]
    public void ChangePowersIU(){
        GameObject UpDownGO = GameObject.FindGameObjectWithTag("UpDownUI");
        GameObject LeftRigthGO = GameObject.FindGameObjectWithTag("LeftRightUI");
        if (UpDownGO == null || LeftRigthGO == null)
        {
            Debug.LogError("No se encontro el UpDown o LeftRigthGO tag en MainScene. return");
            return;
        }

        // TODO: Hacer esto con una fucking animación
        Vector3 posAux = UpDownGO.transform.position;
        UpDownGO.transform.position = LeftRigthGO.transform.position;
        LeftRigthGO.transform.position = posAux;
    }

    public void UpdateLevelText(string level){
        if(levelText == null){
            GameObject levelTextGO = GameObject.FindGameObjectWithTag("LevelTextTag");
            if(levelTextGO == null)
            {
                Debug.LogError("No se encontro el levelText. return");
                return;
            }
            levelText = GetComponent<Text>();
        }

        levelText.text = "Level " + level;
    }

    public void UpdateTimeText(float time){
        if(timeText == null){
            GameObject timeTextGO = GameObject.FindGameObjectWithTag("TimeTextTag");
            if(timeText == null)
            {
                // Debug.LogError("No se encontro el timeText. return");
                return;
            }
            timeText = GetComponent<Text>();
        }

        string textMinSec = string.Format("{0}:{1:00}", (int)time / 60, (int)time % 60);
        timeText.text = "Time: " + time;
    }

    void OnDestroy(){
        levelText = null;
        timeText = null;
    }
}
