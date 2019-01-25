using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKGameManager : MonoBehaviour
{
    //Nombre de jugadores
    //Tiempo
    //Nivel actual

    //Tipos de casillas y contenido
    private enum EMKTileType
    {
        Empty,
        Character,
        Obstacle,
        Container,
        Object
    }

    //Colores de personajes y objetos
    private enum EMKColor
    {
        None,
        Black,
        Blue,
        Red,
        Yellow
    }

    //Typedef TILE
    private struct MKTile
    {
        public uint row;
        public uint col;
        public EMKTileType type;
    }

    //Grid del juego
    private MKTile[,] m_grid;

    void Start()
    {
        uint gridWidth = MKGame.Instance.GetGameContent().GetGameManagerContent().m_gridWidth;
        uint gridHeight = MKGame.Instance.GetGameContent().GetGameManagerContent().m_gridHeight;

        m_grid = new MKTile[gridWidth, gridHeight];

        for (uint r = 0; r < gridWidth; ++r)
        {
            for (uint c = 0; c < gridHeight; ++c)
            {
                m_grid[r, c].row = r;
                m_grid[r, c].col = c;
                m_grid[r, c].type = EMKTileType.Empty;
            }
        }
    }

    public Vector3 GetWorldPosition(Vector2 logicPosition)
    {
        return new Vector3(0, 0, 0);
    }
}
