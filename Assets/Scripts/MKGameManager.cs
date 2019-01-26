using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKGameManager : MonoBehaviour
{
    //Players
    protected String m_player1Name;
    protected String m_player2Name;

    //Time
    protected float m_gameAccTime;

    //Level
    protected uint m_gameCurrentLevel;

    //Game grid
    protected MKCellData.TMKCell[,] m_cells;

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

    public Vector3 GetWorldPosition(uint row, uint col)
    {
        float cellWidth  = MKGame.Instance.GetGameContent().GetGameManagerContent().m_cellWidth;
        float cellHeight = MKGame.Instance.GetGameContent().GetGameManagerContent().m_cellHeight;

        return new Vector3(row + cellWidth / 2, 0, col + cellHeight / 2);
    }

    public bool PlaceInCell(uint row, uint col, MKCellData.EMKCellType type)
    {
        if (CanPlaceInCell(row, col, type))
            return false;

        m_cells[row, col].type = type;
        return true;
    }

    protected bool CanPlaceInCell(uint row, uint col, MKCellData.EMKCellType type)
    {
        return m_cells[row, col].type != MKCellData.EMKCellType.Empty;
    }

    protected bool CanMoveToCell(uint currentCellRow, uint currentCellCol, MKCharacterController.EMKCharacterMove move)
    {
        int nextCellRow = (int)currentCellRow;
        int nextCellCol = (int)currentCellCol;

        switch (move)
        {
            case MKCharacterController.EMKCharacterMove.Up:
                nextCellRow -= 1;
                break;
            case MKCharacterController.EMKCharacterMove.Right:
                nextCellCol += 1;
                break;
            case MKCharacterController.EMKCharacterMove.Bottom:
                nextCellRow += 1;
                break;
            case MKCharacterController.EMKCharacterMove.Left:
                nextCellCol -= 1;
                break;
        }

        return m_cells[nextCellRow, nextCellCol].type == MKCellData.EMKCellType.Empty;
    }

}
