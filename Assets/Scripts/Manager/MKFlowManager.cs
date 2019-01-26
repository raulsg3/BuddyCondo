using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MKFlowManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        m_State = new State();
        m_MKLevelLoader = MKGame.Instance.GetLevelLoader();
        m_MKCharacterManager = MKGame.Instance.GetCharacterManager();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Button]
    public void test() { WinLevel(1); }
    [Button]
    public void test2() { WinLevel(2); }
    public void WinLevel(uint level)
    {
        //if (m_State.GameStatus != (int)Status.Game) { throw new System.Exception("No puedes llamarme desde fuera del Game."); }

        m_MKCharacterManager.SetPlayerActiveStatus(false);

        GameObject uIDataGO = GameObject.FindGameObjectWithTag("UIData");
        GameObject victoryUIGO = uIDataGO.GetComponent<MKUIData>().GetUIGO("victoryUI");
        victoryUIGO.SetActive(true);

        StartCoroutine(FadeInLoadLevel(level));
    }

    public void LoadLevel(uint level)
    {
        m_State.GameStatus = (int)Status.LoadingLevel;
        m_MKLevelLoader.LoadLevel(level.ToString());
    }

    public void NextLevelLoaded()
    {
        if (m_State.GameStatus != (int)Status.LoadingLevel && m_State.GameStatus != (int)Status.Menu)
        {
            Debug.Log(m_State.GameStatus);
            throw new System.Exception("No puedes llamarme desde fuera del Loading o del menu.");
        }

        StartCoroutine(FadeOut());
    }

    IEnumerator DelayDestroyCreate(uint level)
    {
        yield return new WaitForSeconds(2);
        LoadLevel(level);
    }
    
    IEnumerator FadeInLoadLevel(uint level)
    {
        if (loading) yield break;
        loading = true;
        fadeImage.gameObject.SetActive(true);
        Color currentColor = fadeImage.color;
        currentColor.a = 0f;
        fadeImage.color = currentColor;
        while (fadeImage.color.a < 1f)
        {
            currentColor.a += Time.deltaTime * fadeSpeed;
            fadeImage.color = currentColor;
            yield return null;
        }

        yield return new WaitForSeconds(2);
        m_MKLevelLoader.DestroyLevel();
        StartCoroutine(DelayDestroyCreate(level));
        loading = false;
    }

    IEnumerator TimeToStart()
    {
        GameObject uIDataGO = GameObject.FindGameObjectWithTag("UIData");
        GameObject startingUIGO = uIDataGO.GetComponent<MKUIData>().GetUIGO("startingUI");
        startingUIGO.GetComponent<Text>().text = 3.ToString();
        yield return new WaitForSeconds(1);
        startingUIGO.GetComponent<Text>().text = 2.ToString();
        yield return new WaitForSeconds(1);
        startingUIGO.GetComponent<Text>().text = 1.ToString();
        yield return new WaitForSeconds(1);
        startingUIGO.GetComponent<Text>().text = "GO!";
        yield return new WaitForSeconds(1);
        startingUIGO.SetActive(false);

        m_MKCharacterManager.SetPlayerActiveStatus(true);
        MKGame.Instance.GetGameManager().StartLevel();
    }


    IEnumerator FadeOut()
    {
        if (loading) yield break;
        loading = true;
        if (fadeImage == null) { fadeImage = GameObject.FindGameObjectWithTag("FadeImage").GetComponent<Image>(); }
        try { GameObject.FindGameObjectWithTag("UIData").GetComponent<MKUIData>().GetUIGO("victoryUI").SetActive(false); } catch { }
        fadeImage.gameObject.SetActive(true);
        Color currentColor = fadeImage.color;
        currentColor.a = 1f;
        fadeImage.color = currentColor;
        while (fadeImage.color.a > 0f)
        {
            currentColor.a -= Time.deltaTime * fadeSpeed;
            fadeImage.color = currentColor;
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        GameObject uIDataGO = GameObject.FindGameObjectWithTag("UIData");
        GameObject victoryUIGO = uIDataGO.GetComponent<MKUIData>().GetUIGO("victoryUI");
        victoryUIGO.SetActive(false);
        GameObject startingUIGO = uIDataGO.GetComponent<MKUIData>().GetUIGO("startingUI");
        startingUIGO.SetActive(true);

        yield return StartCoroutine(TimeToStart());

        m_State.GameStatus = (int)Status.Game;
        loading = false;
    }

    internal class State
    {
        int currentStatus;

        public State()
        {
            this.currentStatus = (int)Status.Menu;
        }

        public int GameStatus
        {
            get { return currentStatus; }
            set { this.currentStatus = value; }
        }   
    }

    enum Status { Menu, Game, GameOver, LoadingLevel }
    State m_State;
    MKLevelLoader m_MKLevelLoader;
    MKCharacterManager m_MKCharacterManager;
    public Image fadeImage;
    private bool loading;
    public float fadeSpeed = 0.5f;
}
