using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKFlowManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        m_State = new State();
        m_MKLevelLoader = MKGame.Instance.GetLevelLoader();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLevel()
    {
        int level = Random.Range(1,1); //1 FOR DEBUG PORPOUSES
        m_MKLevelLoader.LoadLevel("level_"+level.ToString());
        if (m_State.GameStatus != (int)Status.Game)
        {
            m_State.GameStatus = (int)Status.Game;
        }

        //MOSTRAR UI DE VICTORIA
    }

    public void GameOver()
    {
        
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

    enum Status { Menu, Game, GameOver }
    State m_State;
    MKLevelLoader m_MKLevelLoader;
}
