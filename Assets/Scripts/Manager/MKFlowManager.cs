﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MKFlowManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        m_State = new State();
        m_MKLevelLoader = MKGame.Instance.GetLevelLoader();
        m_MKCharacterManager = MKGame.Instance.GetCharacterManager();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WinLevel(uint level)
    {
        if (m_State.GameStatus != (int)Status.Game) { throw new System.Exception("No puedes llamarme desde fuera del Game."); }

        m_MKCharacterManager.SetPlayerActiveStatus(false);

        //ALGO PARA FADE IN EL CARTEL DE VICTORIA?
        GameObject uIDataGO = GameObject.FindGameObjectWithTag("UIData");
        GameObject victoryUIGO = uIDataGO.GetComponent<MKUIData>().GetUIGO("victoryUI");
        victoryUIGO.SetActive(true);

        LoadLevel(level);
    }

    public void LoadLevel(uint level)
    {
        m_MKLevelLoader.LoadLevel("level_" + level.ToString());
        m_State.GameStatus = (int)Status.LoadingLevel;
    }

    public void NextLevelLoaded()
    {
        if (m_State.GameStatus != (int)Status.LoadingLevel || m_State.GameStatus != (int)Status.Menu) { throw new System.Exception("No puedes llamarme desde fuera del Loading o del menu."); }

        //ALGO PARA FADE OUT EL CARTEL DE VICTORIA?
        GameObject uIDataGO = GameObject.FindGameObjectWithTag("UIData");
        GameObject victoryUIGO = uIDataGO.GetComponent<MKUIData>().GetUIGO("victoryUI");
        victoryUIGO.SetActive(false);
        GameObject startingUIGO = uIDataGO.GetComponent<MKUIData>().GetUIGO("startingUI");
        startingUIGO.SetActive(true);
        StartCoroutine(TimeToStart());

        m_State.GameStatus = (int)Status.Game;
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
}
