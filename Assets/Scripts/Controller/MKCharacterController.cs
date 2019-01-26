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
    void Start()
    {
        m_CharacterContent = MKGame.Instance.GetGameContent().GetCharacterContent();
        SetPlayerFacing(PlayerFacing.UP);
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
        // Process input, grab & movement
        Vector2 CurrentInput = ProcessPlayerInput();

        ProcessCharacterGrab();

        ProcessCharacterMovement(CurrentInput);

        // Update movement
        UpdateMovementCoolDown();

        UpdateMovement();
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

    public bool isObjectGrabbed;

    void ProcessCharacterGrab()
    {
        if(bIsMoving)
        {
            return;
        }

        // Check if the player wants to perform a grab
        if (m_PlayerNumber == EMKPlayerNumber.Player1 && Input.GetButtonDown("Grab1"))
        {
            if(MKGame.Instance.GetGameManager().InteractWithCell(m_CharacterIndexPositionX,m_CharacterIndexPositionY,GetMoveFromFacing())){
                Debug.Log("Suscceful grab player 1");
                // Hack! use cheated object to grab if defined
                if (m_CheatedObjectToGrab)
                {
                    m_GrabbedGameObject = m_CheatedObjectToGrab;
                }
            }
        }
        else if(m_PlayerNumber == EMKPlayerNumber.Player2 && Input.GetButtonDown("Grab2"))
        {
            if(MKGame.Instance.GetGameManager().InteractWithCell(m_CharacterIndexPositionX,m_CharacterIndexPositionY,GetMoveFromFacing())){
                Debug.Log("Suscceful grab player 2");
                // Hack! use cheated object to grab if defined
                if (m_CheatedObjectToGrab)
                {
                    m_GrabbedGameObject = m_CheatedObjectToGrab;
                }
            }
        }
        else
        {
            // m_GrabbedGameObject = null;
        }
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

    void ProcessCharacterMovement(Vector2 _MovementToProcess)
    {
        // Debug.Log(_MovementToProcess);
        // Check if we can move to that cell!
        if(_MovementToProcess.x == 0&& _MovementToProcess.y==0) return;
        if (bIsMoving)
        {
            return;
        }

        SetPlayerFacingWithVector(_MovementToProcess);

        if(MKGame.Instance.GetGameManager().MoveToCell(ref m_CharacterIndexPositionX,ref m_CharacterIndexPositionY,GetEMKMoveFromVector(_MovementToProcess))){
            // Precalculate the adjacent cell position
            Vector2 PrecalculatedPositionIndex;
            
            PrecalculatedPositionIndex.x = m_CharacterIndexPositionX;
            PrecalculatedPositionIndex.y = m_CharacterIndexPositionY;

            PrecalculatedPositionIndex += _MovementToProcess;

            // Check the remaining time
            if (m_TimeRemainingToMove <= 0 && _MovementToProcess.magnitude > 0.1f)
            {
                Vector2 PositionToSet = transform.position;

                // Update the indexes
                // m_CharacterIndexPosition = PrecalculatedPositionIndex;

                // Update the position
                m_PositionToMove += new Vector3(_MovementToProcess.x, 0.0f, _MovementToProcess.y);
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

            // Lerp the grabbed object position if required
            if(m_GrabbedGameObject)
            {
                Debug.Log("asdf");
                m_GrabbedGameObject.transform.position = Vector3.Lerp(m_GrabbedGameObject.transform.position, m_MovementStartPosition, m_CharacterContent.m_MovementInterpSpeed * Time.deltaTime);
            }

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
