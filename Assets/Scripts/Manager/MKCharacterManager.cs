using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKCharacterManager : MonoBehaviour
{
  public MKCharacterController player1;
  public MKCharacterController player2;

  public void SetPlayerActiveStatus(bool status){
    player1.playerActive = status;
    player2.playerActive = status;
  }
}
