using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SORankingContent", menuName = "MK/SORankingContent", order = 2)]
public class MKRankingContent : ScriptableObject
{
    public class MKScore
    {
        public string playerOne = "";
        public string playerTwo = "";
        public int level = 0;
        public int score = 0;

        public MKScore(string playerOne, string playerTwo, int level, int score)
        {
            this.playerOne = playerOne;
            this.playerTwo = playerTwo;
            this.level = level;
            this.score = score;
        }
    }
}
