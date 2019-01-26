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
  public void ChangePowers(){
    if(player1.playerPower == PlayerPower.HORIZONTAL){
        player1.playerPower = PlayerPower.VERTICAL;
    }else
        player1.playerPower = PlayerPower.HORIZONTAL;

    if(player2.playerPower == PlayerPower.HORIZONTAL){
        player2.playerPower = PlayerPower.VERTICAL;
    }else
        player2.playerPower = PlayerPower.HORIZONTAL;
   }
}
