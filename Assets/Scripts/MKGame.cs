using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKGame : USSimpleSingleton<MKGame>
{
    /*public MKGameManager GetGameManager()
    {
        return m_MKGameManager;
    }*/
    public MKRankingManager GetRankingManager()
    {
        return m_MKRankingManager;
    }

    public MKGameContent GetGameContent()
    {
        return m_MKGameContent;
    }

    /*[SerializeField]
    private MKGameManager m_MKGameManager;*/
    [SerializeField]
    private MKRankingManager m_MKRankingManager;


    [SerializeField]
    private MKGameContent m_MKGameContent;
}