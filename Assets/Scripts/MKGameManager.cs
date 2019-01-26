using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKGameManager : MonoBehaviour
{
    //Nombre de jugadores
    //Tiempo
    //Nivel actual

    //Game grid
    private MKCellData.TMKCell[,] m_cells;

    void Start()
    {
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
