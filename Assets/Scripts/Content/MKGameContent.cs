using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOGameContent", menuName = "MK/SOGameContent", order = 0)]
public class MKGameContent : ScriptableObject
{
    public MKGameManagerContent GetGameManagerContent()
    {
        return m_MKGameManagerContent;
    }

    public MKCharacterContent GetCharacterContent()
    {
        return m_MKCharacterContent;
    }

    public MKFurnitureContent GetFurnitureContent()
    {
        return m_MKFurnitureContent;
    }

    [SerializeField]
    private MKGameManagerContent m_MKGameManagerContent;

    [SerializeField]
    private MKCharacterContent m_MKCharacterContent;

    [SerializeField]
    private MKFurnitureContent m_MKFurnitureContent;
}
