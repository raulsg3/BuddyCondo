using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOCharacterContent", menuName = "MK/SOCharacterContent", order = 1)]
public class MKCharacterContent : ScriptableObject
{
    public MKCharacterController m_CharacterPrefab;

    public float m_MoveCooldown = 1;

    public  float m_MovementInterpSpeed = 5.0f;
}
