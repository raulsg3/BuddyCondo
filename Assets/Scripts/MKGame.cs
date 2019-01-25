using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKGame : USSimpleSingleton<MKGame>
{ 
    public MKRankingManager GetRankingManager()
    {
        return m_MKRankingManager;
    }
    public MKGameContent GetGameContent()
    {
        return m_MKGameContent;
    }

    [SerializeField]
    private MKRankingManager m_MKRankingManager;
    [SerializeField]
    private MKGameContent m_MKGameContent;
}