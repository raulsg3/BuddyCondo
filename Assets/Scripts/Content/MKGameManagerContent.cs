using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOGameManagerContent", menuName = "MK/SOGameManagerContent", order = 2)]
public class MKGameManagerContent : ScriptableObject
{
    public uint m_gridRows = 40;
    public uint m_gridCols = 20;

    public float m_cellWidth  = 1;
    public float m_cellHeight = 1;
}
