using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MKFlowManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        m_State = new State();
        m_MKLevelLoader = MKGame.Instance.GetLevelLoader();
        m_MKCharacterManager = MKGame.Instance.GetCharacterManager();
        m_MKRankingManager = MKGame.Instance.GetRankingManager();
        m_MKGameManager = MKGame.Instance.GetGameManager();
        m_MKAudioManager = MKAudioManager.Instance;
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
        m_MKAudioManager.m_AudioTest.GetComponent<AudioSource>().Play() ;

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
    [Button]
    public void NoMoreLevels()
    {
        if (m_State.GameStatus != (int)Status.LoadingLevel)
        {
            Debug.Log(m_State.GameStatus);
            throw new System.Exception("No puedes llamarme desde fuera del Loading o del menu.");
        }

        StartCoroutine(FadeOutGameIsOver());
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
        fadeImage.enabled = true;

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
        fadeImage.enabled = true;
        Color currentColor = fadeImage.color;
        currentColor.a = 1f;
        fadeImage.color = currentColor;
        while (fadeImage.color.a > 0f)
        {
            currentColor.a -= Time.deltaTime * fadeSpeed;
            fadeImage.color = currentColor;
            yield return null;
        }
        fadeImage.enabled = false;

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


    IEnumerator FadeOutGameIsOver()
    {
        if (loading) yield break;
        loading = true;

        yield return new WaitForSeconds(1);

        GameObject uIDataGO = GameObject.FindGameObjectWithTag("UIData");
        GameObject victoryUIGO = uIDataGO.GetComponent<MKUIData>().GetUIGO("victoryUI");
        victoryUIGO.SetActive(false);
        GameObject endGameUIGO = uIDataGO.GetComponent<MKUIData>().GetUIGO("endGameUI");
        endGameUIGO.SetActive(true);

        yield return new WaitForSeconds(2);
        
        endGameUIGO.SetActive(false);
        GameObject yourScoreUIGO = uIDataGO.GetComponent<MKUIData>().GetUIGO("yourScoreUI");
        yourScoreUIGO.GetComponent<MKScoreVisual>().Player1.GetComponent<Text>().text = m_MKGameManager.Player1Name;
        yourScoreUIGO.GetComponent<MKScoreVisual>().Player2.GetComponent<Text>().text = m_MKGameManager.Player2Name;
        yourScoreUIGO.GetComponent<MKScoreVisual>().Score.GetComponent<Text>().text = m_MKGameManager.GameAccTime.ToString();
        yourScoreUIGO.SetActive(true);
        //GUARDAR ESOS DATOS AL JSON LA FUNCIÓN QUE YA HAY

        yield return new WaitForSeconds(5);
        
        yourScoreUIGO.SetActive(false);
        GameObject rankingUIGO = uIDataGO.GetComponent<MKUIData>().GetUIGO("rankingUI");
        rankingUIGO.transform.Find("MenuButton").gameObject.SetActive(false);
        rankingUIGO.SetActive(true);

        yield return new WaitForSeconds(5);

        m_State.GameStatus = (int)Status.Menu;
        loading = false;

        if (fadeImage == null) { fadeImage = GameObject.FindGameObjectWithTag("FadeImage").GetComponent<Image>(); }
        try { GameObject.FindGameObjectWithTag("UIData").GetComponent<MKUIData>().GetUIGO("victoryUI").SetActive(false); } catch { }
        fadeImage.enabled = true;
        Color currentColor = fadeImage.color;
        currentColor.a = 1f;
        fadeImage.color = currentColor;
        while (fadeImage.color.a > 0f)
        {
            currentColor.a -= Time.deltaTime * fadeSpeed;
            fadeImage.color = currentColor;
            yield return null;
        }
        fadeImage.enabled = false;

        SceneManager.LoadScene("MenuScene");
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
    MKAudioManager m_MKAudioManager;
    MKGameManager m_MKGameManager;
    MKRankingManager m_MKRankingManager;
    public Image fadeImage;
    private bool loading;
    public float fadeSpeed = 0.5f;
}
