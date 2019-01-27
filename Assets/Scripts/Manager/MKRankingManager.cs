using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MKRankingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //PopulateRankingData();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void ScoreToJson()
    {
        PopulateRankingData();
        m_MKGameManager = MKGame.Instance.GetGameManager();
        MKScore score = new MKScore(m_MKGameManager.Player1Name, m_MKGameManager.Player2Name, m_MKGameManager.GameAccTime);
        m_scores.scores.Add(score);
        SortRankingData();
        while (m_scores.scores.Count > 6)
        {
            m_scores.scores.RemoveAt(m_scores.scores.Count - 1);
        }
        string json = JsonUtility.ToJson(m_scores);
        File.WriteAllText(Application.persistentDataPath + "/ranking.json", json);
    }
    
    public void PopulateRankingData()
    {
        m_scores = new MKScores();
        try
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/ranking.json");
            m_scores = JsonUtility.FromJson<MKScores>(json);
        }catch(Exception e)
        {

        }
    }

    public void FillRankingMenuData()
    {
        PopulateRankingData();
        SortRankingData();
        if (m_scores != null && m_scores.scores.Count != 0)
        { 
            MKUIData uiData = GameObject.FindGameObjectWithTag("UIData").GetComponent<MKUIData>();
            MKMenuRanking menuRankingCO = uiData.GetUIGO("rankingUI").GetComponent<MKMenuRanking>();
            for (int i = 0; i <= m_scores.scores.Count - 1; i++)
            {
                MKScoreVisual scoreCO = menuRankingCO.scores[i].GetComponent<MKScoreVisual>();
                scoreCO.Player1.GetComponent<Text>().text = m_scores.scores[i].playerOne;
                scoreCO.Player2.GetComponent<Text>().text = m_scores.scores[i].playerTwo;
                scoreCO.Score.GetComponent<Text>().text = m_scores.scores[i].score.ToString();
            }
        }
    }

    public void SortRankingData()
    {
        m_scores.scores.Sort((x, y) => x.score < y.score ? -1 : 0);
    }

    private MKGameManager m_MKGameManager;
    private MKScores m_scores;

    [Serializable]
    public struct MKScore
    {
        public string playerOne;
        public string playerTwo;
        public float score;

        public MKScore(string playerOne, string playerTwo, float score)
        {
            this.playerOne = playerOne;
            this.playerTwo = playerTwo;
            this.score = score;
        }
    }

    [Serializable]
    public class MKScores
    {
        public MKScores()
        {
            this.scores = new List<MKScore>();
        }

        public List<MKScore> scores;
    }
}
