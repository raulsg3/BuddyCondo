using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKGameManager : MonoBehaviour
{
    //Level
    protected uint m_gameCurrentLevel;

    //Players
    protected String m_player1Name;
    protected String m_player2Name;

    //Time
    protected float m_gameAccTime;

    //Targets
    protected uint m_levelTargetsLeft;

    //Game grid
    protected MKCellData.TMKCell[,] m_cells;

    public uint GameCurrentLevel
    {
        get { return m_gameCurrentLevel; }
        private set { m_gameCurrentLevel = value; }
    }

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

    public uint LevelTargetsLeft
    {
        get { return m_levelTargetsLeft; }
        private set { m_levelTargetsLeft = value; }
    }

    void Start()
    {
        GameCurrentLevel = 1;
        GameAccTime = 0;
        LevelTargetsLeft = 0;

        uint gridRows = MKGame.Instance.GetGameContent().GetGameManagerContent().m_gridRows;
        uint gridCols = MKGame.Instance.GetGameContent().GetGameManagerContent().m_gridCols;

        m_cells = new MKCellData.TMKCell[gridRows, gridCols];

        for (uint r = 0; r < gridRows; ++r)
        {
            for (uint c = 0; c < gridCols; ++c)
            {
                m_cells[r, c].row = r;
                m_cells[r, c].col = c;
                m_cells[r, c].type = EMKCellType.Empty;
            }
        }
    }

    public void IncGameCurrentLevel()
    {
        GameCurrentLevel++;
    }

    public void IncGameAccTime(float time)
    {
        GameAccTime += time;
    }

    public void IncLevelTargetsLeft()
    {
        LevelTargetsLeft++;
    }

    public Vector3 GetWorldPosition(uint row, uint col)
    {
        float cellWidth  = MKGame.Instance.GetGameContent().GetGameManagerContent().m_cellWidth;
        float cellHeight = MKGame.Instance.GetGameContent().GetGameManagerContent().m_cellHeight;

        return new Vector3(row + cellWidth / 2, 0, col + cellHeight / 2);
    }

    public Vector2 GetNextLogicPosition(uint row, uint col, EMKMove move)
    {
        uint gridRows = MKGame.Instance.GetGameContent().GetGameManagerContent().m_gridRows;
        uint gridCols = MKGame.Instance.GetGameContent().GetGameManagerContent().m_gridCols;

        int nextRow = (int)row;
        int nextCol = (int)col;

        switch (move)
        {
            case EMKMove.Up:
                if (nextCol < gridCols - 1)
                    nextCol += 1;
                break;
            case EMKMove.Right:
                if (nextRow < gridRows - 1)
                    nextRow += 1;
                break;
            case EMKMove.Bottom:
                if (nextCol > 0)
                    nextCol -= 1;
                break;
            case EMKMove.Left:
                if (nextRow > 0)
                    nextRow -= 1;
                break;
        }

        return new Vector2(nextRow, nextCol);
    }

    public bool PlaceInCell(uint row, uint col, EMKCellType type)
    {
        if (!CanPlaceInCell(row, col, type))
            return false;

        if (type == EMKCellType.Target)
            IncLevelTargetsLeft();

        m_cells[row, col].type = type;
        return true;
    }

    protected bool CanPlaceInCell(uint row, uint col, EMKCellType type)
    {
        return m_cells[row, col].type == EMKCellType.Empty;
    }

    /*********************************************************
    * Move
    *********************************************************/
    public bool MoveToCell(ref uint currentCellRow, ref uint currentCellCol, EMKMove move)
    {
        EMKCellType currentCellType = m_cells[currentCellRow, currentCellCol].type;

        //Players
        if (currentCellType == EMKCellType.Player)
        {
            return CanMovePlayerToCell(ref currentCellRow, ref currentCellCol, move);
        }

        //Players with a movable object
        if (currentCellType == EMKCellType.PlayerWithMovable)
        {
            return CanMoveMovableToNextCell(ref currentCellRow, ref currentCellCol, move);
        }

        //Only players can move
        return false;
    }

    protected bool CanMovePlayerToCell(ref uint currentCellRow, ref uint currentCellCol, EMKMove move)
    {
        Vector2 nextCell = GetNextLogicPosition(currentCellRow, currentCellCol, move);
        uint nextCellRow = (uint)nextCell.x;
        uint nextCellCol = (uint)nextCell.y;

        //Grid limits
        if (nextCellRow == currentCellRow && nextCellCol == currentCellCol)
            return false;

        //Next cell is not empty
        if (m_cells[nextCellRow, nextCellCol].type != EMKCellType.Empty)
            return false;

        //Next cell is empty
        m_cells[nextCellRow, nextCellCol].type = EMKCellType.Player;
        m_cells[currentCellRow, currentCellCol].type = EMKCellType.Empty;

        currentCellRow = nextCellRow;
        currentCellCol = nextCellCol;

        return true;
    }

    protected bool CanMoveMovableToNextCell(ref uint playerCellRow, ref uint playerCellCol, EMKMove move)
    {
        Vector2 movableCell = GetNextLogicPosition(playerCellRow, playerCellCol, move);
        uint movableCellRow = (uint)movableCell.x;
        uint movableCellCol = (uint)movableCell.y;

        //Grid limits
        if (movableCellRow == playerCellRow && movableCellCol == playerCellCol)
            return false;

        Vector2 nextMovableCell = GetNextLogicPosition(movableCellRow, movableCellCol, move);
        uint nextMovableCellRow = (uint)movableCell.x;
        uint nextMovableCellCol = (uint)movableCell.y;

        //Grid limits again
        if (nextMovableCellRow == movableCellRow && nextMovableCellCol == movableCellRow)
            return false;

        bool canMove = false;

        /*
        if (m_cells[nextCellRow, nextCellCol].type == EMKCellType.Empty)
        {
            canMove = true;
        }
        else if (m_cells[nextCellRow, nextCellCol].type == EMKCellType.Target)
        {
            canMove = true;
        }


        //The next for the movable object is not empty
        if (m_cells[nextCellRow, nextCellCol].type != EMKCellType.Empty)
            return false;

        //Next cell is empty
        m_cells[nextCellRow, nextCellCol].type = EMKCellType.Player;
        m_cells[currentCellRow, currentCellCol].type = EMKCellType.Empty;

        currentCellRow = nextCellRow;
        currentCellCol = nextCellCol;
        */

        return canMove;
    }

    /*********************************************************
    * Interact
    *********************************************************/
    public bool InteractWithCell(uint currentCellRow, uint currentCellCol, EMKMove move)
    {
        EMKCellType currentCellType = m_cells[currentCellRow, currentCellCol].type;

        //Players
        if (currentCellType == EMKCellType.Player)
        {
            return CanPlayerInteractWithCell(currentCellRow, currentCellCol, move);
        }

        //Players with a movable object
        if (currentCellType == EMKCellType.PlayerWithMovable)
        {
            m_cells[currentCellRow, currentCellCol].type = EMKCellType.Player;
            return true;
        }

        //Only players can interact
        return false;
    }

    protected bool CanPlayerInteractWithCell(uint currentCellRow, uint currentCellCol, EMKMove move)
    {
        Vector2 nextCell = GetNextLogicPosition(currentCellRow, currentCellCol, move);
        uint objCellRow = (uint)nextCell.x;
        uint objCellCol = (uint)nextCell.y;

        //Grid limits
        if (objCellRow == currentCellRow && objCellCol == currentCellCol)
            return false;

        //The objective cell is not a Movable object
        if (m_cells[objCellRow, objCellCol].type != EMKCellType.Movable)
            return false;

        //The objective cell is a Movable object
        m_cells[currentCellRow, currentCellCol].type = EMKCellType.PlayerWithMovable;

        return true;
    }
}
