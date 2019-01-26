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
    protected float m_levelInitTime;

    //Targets
    protected uint m_levelTargetsLeft;

    //Game grid
    protected MKCellData.TMKCell[,] m_cells;

    public uint GameCurrentLevel
    {
        get { return m_gameCurrentLevel; }
        protected set { m_gameCurrentLevel = value; }
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
        protected set { m_gameAccTime = value; }
    }

    public uint LevelTargetsLeft
    {
        get { return m_levelTargetsLeft; }
        protected set { m_levelTargetsLeft = value; }
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
                m_cells[r, c].color = EMKColor.None;
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

    public void DecLevelTargetsLeft()
    {
        LevelTargetsLeft--;
    }

    public void StartLevel()
    {
        m_levelInitTime = Time.time;
    }

    protected void EndLevel()
    {
        //@TODO Time
        IncGameCurrentLevel();
        MKGame.Instance.GetFlowManager().WinLevel(GameCurrentLevel);
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

    public bool PlaceInCell(uint row, uint col, EMKCellType type, EMKColor color)
    {
        if (!CanPlaceInCell(row, col, type))
            return false;

        if (type == EMKCellType.Target)
            IncLevelTargetsLeft();

        m_cells[row, col].type = type;
        m_cells[row, col].color = color;
        return true;
    }

    protected bool CanPlaceInCell(uint row, uint col, EMKCellType type)
    {
        return m_cells[row, col].type == EMKCellType.Empty;
    }

    protected bool IsCellTypePlayer(EMKCellType cellType)
    {
        return cellType == EMKCellType.Player1 || cellType == EMKCellType.Player2;
    }

    protected bool IsCellTypePlayerWithMovable(EMKCellType cellType)
    {
        return cellType == EMKCellType.PlayerWithMovable1 || cellType == EMKCellType.PlayerWithMovable2;
    }

    /*********************************************************
    * Move
    *********************************************************/
    public bool MoveToCell(ref uint currentCellRow, ref uint currentCellCol, EMKMove move)
    {
        EMKCellType currentCellType = m_cells[currentCellRow, currentCellCol].type;

        //Players
        if (IsCellTypePlayer(currentCellType))
        {
            return CanMovePlayerToCell(ref currentCellRow, ref currentCellCol, move);
        }

        //Players with a movable object
        if (IsCellTypePlayerWithMovable(currentCellType))
        {
            Vector2 nextCell = GetNextLogicPosition(currentCellRow, currentCellCol, move);
            uint nextCellRow = (uint)nextCell.x;
            uint nextCellCol = (uint)nextCell.y;

            bool nextCellMovable = true;

            if (nextCellRow == currentCellRow && nextCellCol == currentCellCol)
            {
                nextCellMovable = false; //Grid limits
            }
            else if (m_cells[nextCellRow, nextCellCol].type != EMKCellType.Movable)
            {
                nextCellMovable = false; //Next cell is not the movable object related to the player
            }

            if (nextCellMovable)
            {
                //The player is pushing the movable object
                return CanMoveMovableToNextCell(ref currentCellRow, ref currentCellCol, move);
            }
            else
            {
                //The player is pulling the movable object
                return CanMovePlayerWithMovableToNextCell(ref currentCellRow, ref currentCellCol, move);
            }
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
        m_cells[nextCellRow, nextCellCol].type = m_cells[currentCellRow, currentCellCol].type;
        m_cells[nextCellRow, nextCellCol].color = m_cells[currentCellRow, currentCellCol].color;

        m_cells[currentCellRow, currentCellCol].type = EMKCellType.Empty;
        m_cells[currentCellRow, currentCellCol].color = EMKColor.None;

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

        if (m_cells[nextMovableCellRow, nextMovableCellCol].type == EMKCellType.Empty)
        {
            //The next cell for the movable object is empty
            canMove = true;

            m_cells[nextMovableCellRow, nextMovableCellCol].type = EMKCellType.Movable;
            m_cells[nextMovableCellRow, nextMovableCellCol].color = m_cells[movableCellRow, movableCellCol].color;

            m_cells[movableCellRow, movableCellCol].type = m_cells[playerCellRow, playerCellCol].type;
            m_cells[movableCellRow, movableCellCol].color = m_cells[playerCellRow, playerCellCol].color;

            m_cells[playerCellRow, playerCellCol].type = EMKCellType.Empty;
            m_cells[playerCellRow, playerCellCol].color = EMKColor.None;

            playerCellRow = movableCellRow;
            playerCellCol = movableCellCol;
        }
        else if (m_cells[nextMovableCellRow, nextMovableCellCol].type == EMKCellType.Target)
        {
            //The next cell for the movable object is a target
            //Check colors
            EMKColor nextMovableColor = m_cells[nextMovableCellRow, nextMovableCellCol].color;
            EMKColor movableColor = m_cells[movableCellRow, movableCellCol].color;

            if (nextMovableColor == movableColor)
            {
                //Movable object and target with the same color
                canMove = true;
                DecLevelTargetsLeft();

                m_cells[nextMovableCellRow, nextMovableCellCol].type = EMKCellType.TargetFull;

                m_cells[movableCellRow, movableCellCol].type = m_cells[playerCellRow, playerCellCol].type;
                m_cells[movableCellRow, movableCellCol].color = m_cells[playerCellRow, playerCellCol].color;

                m_cells[playerCellRow, playerCellCol].type = EMKCellType.Empty;
                m_cells[playerCellRow, playerCellCol].color = EMKColor.None;

                playerCellRow = movableCellRow;
                playerCellCol = movableCellCol;
            }
        }

        return canMove;
    }

    protected bool CanMovePlayerWithMovableToNextCell(ref uint playerCellRow, ref uint playerCellCol, EMKMove move)
    {
        Vector2 nextPlayerCell = GetNextLogicPosition(playerCellRow, playerCellCol, move);
        uint nextPlayerCellRow = (uint)nextPlayerCell.x;
        uint nextPlayerCellCol = (uint)nextPlayerCell.y;

        //Grid limits
        if (nextPlayerCellRow == playerCellRow && nextPlayerCellCol == playerCellCol)
            return false;

        //Next player cell is not empty
        if (m_cells[nextPlayerCellRow, nextPlayerCellCol].type != EMKCellType.Empty)
            return false;

        //Next player cell is empty
        m_cells[nextPlayerCellRow, nextPlayerCellCol].type = m_cells[playerCellRow, playerCellCol].type;
        m_cells[nextPlayerCellRow, nextPlayerCellCol].color = m_cells[playerCellRow, playerCellCol].color;

        //@TODO Mover el movable object asociado
        //m_cells[playerCellRow, playerCellCol].type = EMKCellType.Empty;
        //m_cells[playerCellRow, playerCellCol].color = EMKColor.None;

        playerCellRow = nextPlayerCellRow;
        playerCellCol = nextPlayerCellCol;

        return true;
    }

    /*********************************************************
    * Interact
    *********************************************************/
    public bool InteractWithCell(uint currentCellRow, uint currentCellCol, EMKMove move)
    {
        EMKCellType currentCellType = m_cells[currentCellRow, currentCellCol].type;

        //Players
        if (IsCellTypePlayer(currentCellType))
        {
            return CanPlayerInteractWithCell(currentCellRow, currentCellCol, move);
        }

        //Players with a movable object
        if (IsCellTypePlayerWithMovable(currentCellType))
        {
            EMKCellType typePlayer = (currentCellType == EMKCellType.PlayerWithMovable1) ? EMKCellType.Player1 : EMKCellType.Player2;
            m_cells[currentCellRow, currentCellCol].type = typePlayer;
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
        EMKCellType currentCellType = m_cells[currentCellRow, currentCellCol].type;
        EMKCellType typePlayer = (currentCellType == EMKCellType.Player1) ? EMKCellType.PlayerWithMovable1 : EMKCellType.PlayerWithMovable2;
        m_cells[currentCellRow, currentCellCol].type = typePlayer;

        return true;
    }
}
