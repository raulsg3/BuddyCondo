using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKRankingManager : MonoBehaviour
{
    MKScores m_scores; 
   
    // Start is called before the first frame update
    void Start()
    {
        PopulateRankingData();
    }

    // Update is called once per frame
    void Update()
    {

    }
    [Button]
    public void ScoreToJson()
    {
        /*m_MKGameManagerContent = MKGameContent.GetGameManagerContent();*/
        //MKScore score = new MKScore(m_MKGameManagerContent.playerOne, m_MKGameManagerContent.playerTwo, m_MKGameManagerContent.level, m_MKGameManagerContent.score);
        MKScore score = new MKScore("ASD", "asd", 2, 2);
        m_scores.scores.Add(score);
        SortRankingData();
        string json = JsonUtility.ToJson(m_scores);
        File.WriteAllText(Application.persistentDataPath + "/ranking.json", json);
    }

    [Button]
    public void PopulateRankingData()
    {
        m_scores = new MKScores();
        string json = File.ReadAllText(Application.persistentDataPath + "/ranking.json");
        m_scores = JsonUtility.FromJson<MKScores>(json);
    }

    public void SortRankingData()
    {
        m_scores.scores.Sort((x,y) => x.score > y.score ? -1 : (x.score == y.score ? (x.level > y.level ? -1 : 0 ) : 1));
    }

    //public MKGameManagerContent m_MKGameManagerContent;

    [Serializable]
    public struct MKScore
    {
        public string playerOne;
        public string playerTwo;
        public int level;
        public int score;

        public MKScore(string playerOne, string playerTwo, int level, int score)
        {
            this.playerOne = playerOne;
            this.playerTwo = playerTwo;
            this.level = level;
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
