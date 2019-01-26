using System;
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

    public MKGameUIManager GetUIManager()
    {
        return m_MKGameUIManager;
    }

    public MKFlowManager GetFlowManager()
    {
        return m_MKFlowManager;
    }

    public MKLevelLoader GetLevelLoader()
    {
        return m_MKLevelLoader;
    }

    public MKGameContent GetGameContent()
    {
        return m_MKGameContent;
    }

    public MKCharacterManager GetCharacterManager()
    {
        return m_MKCharacterManager;
    }

    [SerializeField]
    private MKRankingManager m_MKRankingManager;

    [SerializeField]
    private MKFurnitureManager m_MKFurnitureManager;

    [SerializeField]
    private MKGameManager m_MKGameManager;

    [SerializeField]
    private MKGameUIManager m_MKGameUIManager;

    [SerializeField]
    private MKFlowManager m_MKFlowManager;

    [SerializeField]
    private MKLevelLoader m_MKLevelLoader;

    [SerializeField]
    private MKGameContent m_MKGameContent;
    
    [SerializeField]
    private MKCharacterManager m_MKCharacterManager;
}