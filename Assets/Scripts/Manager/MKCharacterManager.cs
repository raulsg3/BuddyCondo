using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKCharacterManager : MonoBehaviour
{
  public MKCharacterController player1;
  public MKCharacterController player2;

   public void OnMovebleInTarget(EMKCellType eMKPlayerNumber){
       if(eMKPlayerNumber == EMKCellType.Player1){

       }else{

       }
       
   }
  public void SetPlayerActiveStatus(bool status){
    if(player1 != null){
        player1.playerActive = status;
    }else{
        Debug.Log("player 1 is null");
    }
    if(player2 != null){
        player2.playerActive = status;
    }else{
        Debug.Log("player 2 is null");
    }
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
