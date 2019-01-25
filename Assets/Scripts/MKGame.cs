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

    public MKFurnitureManager GetFurnitureManager()
    {
        return m_MKFurnitureManager;
    }

    public MKGameContent GetGameContent()
    {
        return m_MKGameContent;
    }

    [SerializeField]
    private MKRankingManager m_MKRankingManager;

    [SerializeField]
    private MKFurnitureManager m_MKFurnitureManager;

    [SerializeField]
    private MKGameContent m_MKGameContent;
}