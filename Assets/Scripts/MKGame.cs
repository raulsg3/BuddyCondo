﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKGame : USSimpleSingleton<MKGame>
{
    void Awake(){
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public MKRankingManager GetRankingManager()
    {
        return m_MKRankingManager;
    }

    public MKFurnitureManager GetFurnitureManager()
    {
        return m_MKFurnitureManager;
    }

    public MKGameManager GetGameManager()
    {
        return m_MKGameManager;
    }

    public MKGameContent GetGameContent()
    {
        return m_MKGameContent;
    }

    public MKGameUIManager GetUIManager()
    {
        return m_MKGameUIManager;
    }

    [SerializeField]
    private MKRankingManager m_MKRankingManager;

    [SerializeField]
    private MKFurnitureManager m_MKFurnitureManager;

    [SerializeField]
    private MKGameManager m_MKGameManager;

    [SerializeField]
    private MKGameContent m_MKGameContent;

    [SerializeField]
    private MKGameUIManager m_MKGameUIManager;



}