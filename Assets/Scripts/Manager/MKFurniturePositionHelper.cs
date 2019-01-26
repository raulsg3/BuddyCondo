using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKFurniturePositionHelper : MonoBehaviour
{
    public uint PosX = 0;
    public uint PosY = 0;

    public void UpdatePosition(uint posx, uint posy)
    {
        PosX = posx;
        PosY = posy;

        // Update the transform
        this.transform.position = MKGame.Instance.GetGameManager().GetWorldPosition(posx, posy);

        // TODO: Move with Lerp
        //transform.position = Vector3.Lerp(transform.position, m_PositionToMove, m_CharacterContent.m_MovementInterpSpeed * Time.deltaTime);

        //// Stop moving if destination reached
        //if (Vector3.Distance(transform.position, m_PositionToMove) <= 0.01f)
        //{
        //    transform.position = m_PositionToMove;
        //    bIsMoving = false;
        //}
    }
}
