﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [Button]
    public void ScoreToJson()
    {
        m_MKGameManager = MKGame.Instance.GetGameManager();
        MKScore score = new MKScore(m_MKGameManager.Player1Name, m_MKGameManager.Player2Name, m_MKGameManager.GameCurrentLevel, m_MKGameManager.GameAccTime);
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
        m_scores.scores.Sort((x, y) => x.score > y.score ? -1 : (x.score == y.score ? (x.level > y.level ? -1 : 0) : 1));
    }

    public MKGameManager m_MKGameManager;
    public MKScores m_scores;

    [Serializable]
    public struct MKScore
    {
        public string playerOne;
        public string playerTwo;
        public uint level;
        public float score;

        public MKScore(string playerOne, string playerTwo, uint level, float score)
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
