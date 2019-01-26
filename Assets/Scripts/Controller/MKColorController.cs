﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKColorController : MonoBehaviour
{
    private Color furnitureColor;

    [HideInInspector]
    public MeshRenderer[] meshRendererArray;
    [HideInInspector]
    public List<MeshRenderer> meshRendererList;
    public EMKCellType myType;
    private void Awake()
    {
        meshRendererList = new List<MeshRenderer>();
        meshRendererArray = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in meshRendererArray){
            meshRendererList.Add(meshRenderer);
        }
    }


    public void SetColor(EMKColor baseColor, EMKCellType type)
    {
        myType = type;
        if(type == EMKCellType.Target){
          foreach (MeshRenderer meshRenderer in meshRendererList){
            meshRenderer.enabled = false;
            cakeslice.Outline outline = meshRenderer.gameObject.AddComponent<cakeslice.Outline>();
            if(baseColor == EMKColor.Red){
                outline.color = 0;
            }else if (baseColor == EMKColor.Green){
                outline.color = 1;
            }else if (baseColor == EMKColor.Blue){
                outline.color = 2;
            }
          }
        }else if (type == EMKCellType.Movable){
            foreach (MeshRenderer meshRenderer in meshRendererList){
                //set outline
                cakeslice.Outline outline = meshRenderer.gameObject.AddComponent<cakeslice.Outline>();
                if(baseColor == EMKColor.Red){
                    outline.color = 0;
                }else if (baseColor == EMKColor.Green){
                    outline.color = 1;
                }else if (baseColor == EMKColor.Blue){
                    outline.color = 2;
                }
                //set layer
                meshRenderer.gameObject.layer = LayerMask.NameToLayer("FurnitureRayLayer");
          }
        }
        furnitureColor = colorMap[baseColor];
    }

    public GameObject grabPrefab;
    public GameObject grabGOInstance;

    public void ShowFeedback(){
        if(grabGOInstance == null){
           grabGOInstance = Instantiate(MKGame.Instance.GetFurnitureManager().grabFeedbackPrefab,transform);
        }
        if(grabGOInstance == null){
            Debug.LogError("No se ha instanciado el feeback. Return"); 
            return;
        }

        grabGOInstance.transform.SetParent( transform,false);
        grabGOInstance.GetComponent<GrabFeedBack>().canvas.enabled=true;
    
    }

    public void HideFeedback(){
        grabGOInstance.GetComponent<GrabFeedBack>().canvas.enabled=false;
    }
    private bool isGrabbed = false;
    public void ToogleGrabFeedBack(){
        if(grabGOInstance == null){
            Debug.LogError("No se ha instanciado el feeback. Return"); 
            return;
        }

        isGrabbed = !isGrabbed;
        if(isGrabbed){
            grabGOInstance.GetComponent<GrabFeedBack>().ShowAsGrabbed();

        }else{
            grabGOInstance.GetComponent<GrabFeedBack>().ShowAsUnGrabbed();

        }

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
