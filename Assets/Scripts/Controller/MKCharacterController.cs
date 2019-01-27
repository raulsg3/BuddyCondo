using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMKPlayerNumber
{
    Player1,
    Player2,
}

public enum PlayerFacing{
    UP,DOWN,RIGHT,LEFT
}
public enum PlayerPower{
    VERTICAL,HORIZONTAL
}

public class MKCharacterController : MonoBehaviour
{
    // --------------------------------------------------------------
    public PlayerFacing currentPlayerFacing; 
    public Transform m_transform; 
    public PlayerPower playerPower; 
    public bool playerActive = false;
    public LayerMask furnitureLayermask;
    public MKColorController currentColorController;
    RaycastHit hit;
    private Transform myTransform;
    public Animator playerAnimator;
    void Start()
    {
        m_CharacterContent = MKGame.Instance.GetGameContent().GetCharacterContent();
        SetPlayerFacing(PlayerFacing.UP);
        grabbingObject=false;

    }

    [Button]
    public void PlayIlde(){
        playerAnimator.Play("Idle",0,0f);
    }

    [Button]
    public void PlayPush(){
        // playerAnimator.Play("Push",0,0f);
    }

    public void SetPlayerFacingWithVector(Vector2 facing){
        // Debug.Log(facing);
        if(facing.x == 0 && facing.y == 1){
            SetPlayerFacing(PlayerFacing.UP);
        }else if(facing.x == 0 && facing.y == -1){
            SetPlayerFacing(PlayerFacing.DOWN);
        }else if(facing.x == 1 && facing.y == 0){
            SetPlayerFacing(PlayerFacing.RIGHT);
        }else if(facing.x == -1 && facing.y == 0){
            SetPlayerFacing(PlayerFacing.LEFT);
        }else{
            // Debug.LogError("No se econtro facing para "+facing);
        }
    }

    public void SetPlayerFacing(PlayerFacing playerFacing ){
        currentPlayerFacing = playerFacing;
        if(playerFacing == PlayerFacing.UP){
            m_transform.eulerAngles = new Vector3(0,0,0);
        }else if (playerFacing == PlayerFacing.RIGHT){
            m_transform.eulerAngles = new Vector3(0,90,0);
        }else if (playerFacing == PlayerFacing.DOWN){
            m_transform.eulerAngles = new Vector3(0,180,0);
        }else if (playerFacing == PlayerFacing.LEFT){
            m_transform.eulerAngles = new Vector3(0,270,0);
        }
    }

    void Update()
    {
        if(!playerActive) return;

        CastRayForFeedback();
        
        // Process input, grab & movement
        Vector2 CurrentInput = ProcessPlayerInput();

        ProcessCharacterGrab();

        ProcessCharacterMovement(CurrentInput);

        // Update movement
        UpdateMovementCoolDown();

        UpdateMovement();
    }

    public bool CanMoveObjectInThisDirection(Vector3 direction){
        // Debug.Log(direction.x);
        if(PlayerPower.HORIZONTAL == playerPower){
            if((int)direction.x != 0f ){
                return true;
            }else{
                return false;
            }
        }else{
            if((int)direction.z != 0){
                return true;
            }else{

                return false;
            }
        }
    }



    private bool CanMoveInThisDirectionWithObjectGrabbed(Vector3 direction){
        // Debug.Log(direction);
         if(PlayerPower.HORIZONTAL == playerPower){
            if((int)direction.x != 0f ){
                return true;
            }else{
                return false;
            }
        }else{
            if((int)direction.y != 0){
                return true;
            }else{

                return false;
            }
        }
    }
    
   
    private void CastRayForFeedback(){
        if(myTransform == null) myTransform = transform;
        
        if(Physics.Raycast(myTransform.position,myTransform.forward,out hit,1.2f,furnitureLayermask)){
            MKColorController colorController = hit.transform.GetComponentInParent(typeof(MKColorController)) as MKColorController;
            if (colorController != null && colorController.myType == EMKCellType.Movable ){
                if(CanMoveObjectInThisDirection(myTransform.forward)){
                    // Debug.Log("Can move");
                    currentColorController = colorController;
                    currentColorController.ShowFeedback();
                }else{
                    if(currentColorController!= null){

                        currentColorController.HideFeedback();
                        currentColorController = null;
                    }       
                }
            }
            else
            {
                if(currentColorController!= null){

                    currentColorController.HideFeedback();
                    currentColorController = null;
                }
            }
        }
        else if (currentColorController != null)
        {
            currentColorController.HideFeedback();
            currentColorController = null;
        }
    }

    // --------------------------------------------------------------

    void SetPlayerNumber(EMKPlayerNumber _PlayerNumber)
    {
        m_PlayerNumber = _PlayerNumber;
    }



    // --------------------------------------------------------------

    Vector2 ProcessPlayerInput()
    {
        Vector2 CurrentInput = new Vector2(0, 0);

        // Check the configured player
        if (m_PlayerNumber == EMKPlayerNumber.Player1)
        {
            CurrentInput.x = Input.GetAxis("Horizontal1");
            CurrentInput.y = Input.GetAxis("Vertical1");
        }
        else if (m_PlayerNumber == EMKPlayerNumber.Player2)
        {
            CurrentInput.x = Input.GetAxis("Horizontal2");
            CurrentInput.y = Input.GetAxis("Vertical2");
        }

        // We only require the sign
        if(CurrentInput.x != 0.0f)
        {
            CurrentInput.x = Mathf.Sign(CurrentInput.x);
        }

        if(CurrentInput.y != 0.0f)
        {
            CurrentInput.y = Mathf.Sign(CurrentInput.y);
        }

        // Invalidate y if there is an x
        if (CurrentInput.x != 0)
        {
            CurrentInput.y = 0.0f;
        }

        return CurrentInput;
    }

    public bool grabbingObject = false;

    void ProcessCharacterGrab()
    {
        if(bIsMoving)
        {
            return;
        }

        if(!CanMoveObjectInThisDirection(transform.forward)){
            return;
        }

        // Check if the player wants to perform a grab
        if (m_PlayerNumber == EMKPlayerNumber.Player1 && Input.GetButtonDown("Grab1"))
        {
            if(MKGame.Instance.GetGameManager().InteractWithCell(m_CharacterIndexPositionX,m_CharacterIndexPositionY,GetMoveFromFacing())){
                Debug.Log("Suscceful grab or drop player 1");
                if(currentColorController!= null){
                    grabbingObject = !grabbingObject;
                    currentColorController.ToogleGrabFeedBack();
                }
                else{
                    Debug.Log("currentColorController is null");
                }
            }
        }
        else if(m_PlayerNumber == EMKPlayerNumber.Player2 && Input.GetButtonDown("Grab2"))
        {
            if(MKGame.Instance.GetGameManager().InteractWithCell(m_CharacterIndexPositionX,m_CharacterIndexPositionY,GetMoveFromFacing())){
                Debug.Log("Suscceful grab or drop player 2");
                if(currentColorController!= null){
                    grabbingObject = !grabbingObject;
                    currentColorController.ToogleGrabFeedBack();

                }
                else
                {
                    Debug.Log("currentColorController is null");
                }
            }
        }
    }

    public void ObjectPlacedInRightPlace(){

        // ProcessCharacterGrab
        if(currentColorController == null){
            if(Physics.Raycast(myTransform.position,myTransform.forward,out hit,1.2f,furnitureLayermask)){
                MKColorController colorController = hit.transform.GetComponentInParent(typeof(MKColorController)) as MKColorController;
                Debug.Log(colorController + colorController.gameObject.name);
                if (colorController != null && colorController.myType == EMKCellType.Movable ||colorController.myType == EMKCellType.TargetFull  ){
                    colorController.SetObjectInTarget();
                }
            }
        }else{
            currentColorController.SetObjectInTarget();
        }
        // if(currentColorController != null){
        // }else{
        //     Debug.Log("currentColorController is null");
        // }

        grabbingObject = false;
        
    }

    private EMKMove GetEMKMoveFromVector(Vector2 direction){
        if(direction.x == 1 && direction.y == 0){
            return EMKMove.Right;
        }else if(direction.x == -1 && direction.y == 0){
            return EMKMove.Left;
        }
        else if(direction.x == 0 && direction.y == 1){
            return EMKMove.Up;
        }else if(direction.x == 0 && direction.y == -1){
            return EMKMove.Bottom;
        }else{
            Debug.LogError("No se encontro dirección para "+direction);
            return EMKMove.Bottom;
        }
    }

    // private float currentMoveCooldown = 0;
    // private float moveCooldown = 2;

    void ProcessCharacterMovement(Vector2 _MovementToProcess)
    {
        // Debug.Log(_MovementToProcess);
        // Check if we can move to that cell!
        if(_MovementToProcess.x == 0&& _MovementToProcess.y==0) return;
        if (bIsMoving)
        {   
            return;
        }
        Debug.Log(grabbingObject);
        if(grabbingObject && !CanMoveInThisDirectionWithObjectGrabbed(_MovementToProcess)){
            Debug.Log("No se puede mover en esta direción co un objeto");
            return;
        }

        if(!grabbingObject)
            SetPlayerFacingWithVector(_MovementToProcess);

        if (m_TimeRemainingToMove <= 0 && _MovementToProcess.magnitude > 0.1f)
        {
            if(MKGame.Instance.GetGameManager().MoveToCell(ref m_CharacterIndexPositionX,ref m_CharacterIndexPositionY,GetEMKMoveFromVector(_MovementToProcess))){
            // Debug.Log("true");
            // Precalculate the adjacent cell position
            // Vector2 PrecalculatedPositionIndex;
            
            // PrecalculatedPositionIndex.x = m_CharacterIndexPositionX;
            // PrecalculatedPositionIndex.y = m_CharacterIndexPositionY;

            // PrecalculatedPositionIndex += _MovementToProcess;

            // Check the remaining time
                // Vector2 PositionToSet = transform.position;

                // Update the indexes
                // m_CharacterIndexPosition = PrecalculatedPositionIndex;

                // Update the position
                // m_PositionToMove += new Vector3(_MovementToProcess.x, 0.0f, _MovementToProcess.y);
                m_PositionToMove = MKGame.Instance.GetGameManager().GetWorldPosition(m_CharacterIndexPositionX,m_CharacterIndexPositionY);
                m_TimeRemainingToMove = m_CharacterContent.m_MoveCooldown;
                m_MovementStartPosition = transform.position;


                bIsMoving = true;
            }
        }
    }

    // --------------------------------------------------------------

    void UpdateMovementCoolDown()
    {
        // Decrement cooldown time if required
        if (m_TimeRemainingToMove > 0.0f)
        {
            m_TimeRemainingToMove = Mathf.Max(m_TimeRemainingToMove -= Time.deltaTime, 0.0f);
        }
    }

    void UpdateMovement()
    {
        // Lerp positions if moving
        if (bIsMoving)
        {
            transform.position = Vector3.Lerp(transform.position, m_PositionToMove, m_CharacterContent.m_MovementInterpSpeed * Time.deltaTime);

            // // Lerp the grabbed object position if required
            // if(m_GrabbedGameObject)
            // {
            //     m_GrabbedGameObject.transform.position = Vector3.Lerp(m_GrabbedGameObject.transform.position, m_MovementStartPosition, m_CharacterContent.m_MovementInterpSpeed * Time.deltaTime);
            // }

            // Stop moving if destination reached
            if (Vector3.Distance(transform.position, m_PositionToMove) <= 0.01f)
            {
                transform.position = m_PositionToMove;
                bIsMoving = false;
            }
        }
    }
    private EMKMove GetMoveFromFacing(){
        if(currentPlayerFacing == PlayerFacing.DOWN){
            return EMKMove.Bottom;
        }else if(currentPlayerFacing == PlayerFacing.RIGHT){
            return EMKMove.Right;
        }else if(currentPlayerFacing == PlayerFacing.LEFT){
            return EMKMove.Left;
        }else if(currentPlayerFacing == PlayerFacing.UP){
            return EMKMove.Up;
        }else{
            return EMKMove.Up;
        }
    }

    // --------------------------------------------------------------

    MKCharacterContent m_CharacterContent;

    // --------------------------------------------------------------

    public EMKPlayerNumber m_PlayerNumber = EMKPlayerNumber.Player1;

    // public Vector2 m_CharacterIndexPosition = new Vector2(0.0f, 0.0f);
    public uint m_CharacterIndexPositionX=0;
    public uint m_CharacterIndexPositionY=0;

    // ----------------------------------

    private bool bIsMoving = false;

    public float m_TimeRemainingToMove = 0;

    private Vector3 m_PositionToMove = new Vector3(0.0f, 0.0f);

    // ----------------------------------

    private Vector3 m_MovementStartPosition = new Vector3(0.0f, 0.0f);

    public GameObject m_GrabbedGameObject;

    public GameObject m_CheatedObjectToGrab;

}
