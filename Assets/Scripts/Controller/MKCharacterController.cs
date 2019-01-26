using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMKPlayerNumber
{
    Player1,
    Player2,
}

public class MKCharacterController : MonoBehaviour
{
    // --------------------------------------------------------------

    void Start()
    {
        m_CharacterContent = MKGame.Instance.GetGameContent().GetCharacterContent();
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
        if (CurrentInput.x > 0)
        {
            CurrentInput.y = 0.0f;
        }

        return CurrentInput;
    }

    void ProcessCharacterGrab()
    {
        if(bIsMoving)
        {
            return;
        }

        // Check if the player wants to perform a grab
        if (m_PlayerNumber == EMKPlayerNumber.Player1 && Input.GetButton("Grab1"))
        {
            // Hack! use cheated object to grab if defined
            if (m_CheatedObjectToGrab)
            {
                m_GrabbedGameObject = m_CheatedObjectToGrab;
            }
        }
        else if(m_PlayerNumber == EMKPlayerNumber.Player2 && Input.GetButton("Grab2"))
        {
            // Hack! use cheated object to grab if defined
            if(m_CheatedObjectToGrab)
            {
                m_GrabbedGameObject = m_CheatedObjectToGrab;
            }
        }
        else
        {
            m_GrabbedGameObject = null;
        }
    }

    void ProcessCharacterMovement(Vector2 _MovementToProcess)
    {
        // Debug.Log(_MovementToProcess);
        // Check if we can move to that cell!
        if (bIsMoving)
        {
            return;
        }

        // Precalculate the adjacent cell position
        Vector2 PrecalculatedPositionIndex = m_CharacterIndexPosition;
        PrecalculatedPositionIndex += _MovementToProcess;

        // Check the remaining time
        if (m_TimeRemainingToMove <= 0 && _MovementToProcess.magnitude > 0.1f)
        {
            Vector2 PositionToSet = transform.position;

            // Update the indexes
            m_CharacterIndexPosition = PrecalculatedPositionIndex;

            // Update the position
            m_PositionToMove += new Vector3(_MovementToProcess.x, 0.0f, _MovementToProcess.y);
            m_TimeRemainingToMove = m_CharacterContent.m_MoveCooldown;
            m_MovementStartPosition = transform.position;

            bIsMoving = true;
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

    // --------------------------------------------------------------

    MKCharacterContent m_CharacterContent;

    // --------------------------------------------------------------

    public EMKPlayerNumber m_PlayerNumber = EMKPlayerNumber.Player1;

    public Vector2 m_CharacterIndexPosition = new Vector2(0.0f, 0.0f);

    // ----------------------------------

    private bool bIsMoving = false;

    public float m_TimeRemainingToMove = 0;

    private Vector3 m_PositionToMove = new Vector3(0.0f, 0.0f);

    // ----------------------------------

    private Vector3 m_MovementStartPosition = new Vector3(0.0f, 0.0f);

    private GameObject m_GrabbedGameObject;

    public GameObject m_CheatedObjectToGrab;

}
