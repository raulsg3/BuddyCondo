using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabFeedBack : MonoBehaviour
{
    public Image iconImage;
    public Canvas canvas;
    private Camera targetCamera;
    void Update(){
        if(targetCamera== null) targetCamera = Camera.main;
        if(targetCamera== null){
            Debug.Log("No hay camara para look at");
            return;
        } 
        transform.LookAt (targetCamera.transform);
    }
    public float growSpeed = 1f;
    public float shrinkSpeed = 1f;

    IEnumerator Grow(){
        Vector3 currentScale = iconImage.transform.localScale; 
        while(currentScale.x < 1.3f){
            currentScale.x += Time.deltaTime *growSpeed;
            currentScale.y += Time.deltaTime *growSpeed;
            currentScale.z += Time.deltaTime *growSpeed;
            iconImage.transform.localScale = currentScale;
            yield return null;
        }
        currentScale.x = 1.3f;
        currentScale.y = 1.3f;
        currentScale.z = 1.3f;
        iconImage.transform.localScale = currentScale;
    }

    IEnumerator Shrink(){
        Vector3 currentScale = iconImage.transform.localScale; 
        while(currentScale.x > 1f){
            currentScale.x -= Time.deltaTime *shrinkSpeed;
            currentScale.y -= Time.deltaTime *shrinkSpeed;
            currentScale.z -= Time.deltaTime *shrinkSpeed;
            iconImage.transform.localScale = currentScale;
            yield return null;
        }
        currentScale.x = 1f;
        currentScale.y = 1f;
        currentScale.z = 1f;
        iconImage.transform.localScale = currentScale;
    }
    public void ShowAsGrabbed(){
        StopAllCoroutines();
        StartCoroutine(Grow());
        iconImage.color = Color.green;
    }
    public void ShowAsUnGrabbed(){
        StopAllCoroutines();
        StartCoroutine(Shrink());
        iconImage.color = Color.white;
    }
}
