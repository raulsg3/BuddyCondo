using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKGameManager : MonoBehaviour
{
    //Players
    private String m_player1Name;
    private String m_player2Name;

    //Time
    private float m_gameAccTime;

    //Level
    private uint m_gameCurrentLevel;

    //Game grid
    private MKCellData.TMKCell[,] m_cells;

    public String Player1Name
    {
        get { return m_player1Name; }
        set { m_player1Name = value; }
    }

    public String Player2Name
    {
        get { return m_player2Name; }
        set { m_player2Name = value; }
    }

    public float GameAccTime
    {
        get { return m_gameAccTime; }
        private set { m_gameAccTime = value; }
    }

    public uint GameCurrentLevel
    {
        get { return m_gameCurrentLevel; }
        private set { m_gameCurrentLevel = value; }
    }

    void Start()
    {
        GameAccTime = 0;
        GameCurrentLevel = 1;

        uint gridWidth  = MKGame.Instance.GetGameContent().GetGameManagerContent().m_gridWidth;
        uint gridHeight = MKGame.Instance.GetGameContent().GetGameManagerContent().m_gridHeight;

        m_cells = new MKCellData.TMKCell[gridWidth, gridHeight];

        for (uint r = 0; r < gridWidth; ++r)
        {
            for (uint c = 0; c < gridHeight; ++c)
            {
                m_cells[r, c].row = r;
                m_cells[r, c].col = c;
                m_cells[r, c].type = MKCellData.EMKCellType.Empty;
            }
        }
    }

    public bool InitializeCell(int row, int col, MKCellData.EMKCellType type)
    {
        if (m_cells[row, col].type != MKCellData.EMKCellType.Empty)
            return false;

        m_cells[row, col].type = type;
        return true;
    }

    public Vector3 GetWorldPosition(Vector2 logicPosition)
    {
        float cellWidth  = MKGame.Instance.GetGameContent().GetGameManagerContent().m_cellWidth;
        float cellHeight = MKGame.Instance.GetGameContent().GetGameManagerContent().m_cellHeight;

        return new Vector3(logicPosition.x + cellWidth / 2, 0, logicPosition.y + cellHeight / 2);
    }
}
