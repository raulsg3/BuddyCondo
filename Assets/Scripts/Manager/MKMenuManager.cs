using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MKMenuManager : MonoBehaviour
{
    #region Singleton
    private static MKMenuManager s_instance = null;
    public static MKMenuManager Instance
    {
        get { return s_instance; }
    }
    void Awake()
    {
        s_instance = this;
    }
    #endregion

    public Image fadeImage;
    public GameObject menuGO;
    public GameObject enterNameGO;
    private bool loading = false;
    public float fadeSpeed = 0.4f; 

    [Header("Input")]
    public InputField player1;
    public GameObject player1RedError;
    public InputField player2;
    public GameObject player2RedError;

    public void B_EnterName(){
        ActivateNameMenu();
    }
    public void B_Play(){
        if(string.IsNullOrEmpty(player1.text)){
            player1RedError.SetActive(true);
            Invoke("DeactivatePlayer1Error",2f);
            return;
        }

        if(string.IsNullOrEmpty(player2.text)){
            player2RedError.SetActive(true);
            Invoke("DeactivatePlayer2Error",2f);
            return;
        }
        MKGame.Instance.GetGameManager().Player1Name = player1.text;
        MKGame.Instance.GetGameManager().Player2Name = player2.text;
        StartCoroutine(FadeAndLoad());
    }
    public void DeactivatePlayer1Error(){
        player1RedError.SetActive(false);
    }
    public void DeactivatePlayer2Error(){
        player2RedError.SetActive(false);
    }

    public void ActivateNameMenu(){
        menuGO.SetActive(false);
        enterNameGO.SetActive(true);
    }


    IEnumerator FadeAndLoad(){
        if(loading) yield break;
        loading=true;
        Color currentColor = fadeImage.color;
        currentColor.a = 0f;
        fadeImage.color = currentColor;
        fadeImage.gameObject.SetActive(true);
        while(fadeImage.color.a < 1f){
            currentColor.a += Time.deltaTime * fadeSpeed;
            fadeImage.color = currentColor;
            yield return null;
        }
        SceneManager.LoadScene("MainScene");

    }
    public void B_Exit(){
        if(loading) return;

        Application.Quit();
    }
}
