using UnityEngine;

public class MKButtonController : MonoBehaviour
{
    //Button cooldown
    public float m_cooldown = 1.0f;
    private float m_currentCooldown = 0.0f;
    public GameObject pressedButtonGO;
    public GameObject unpressedButtonGO;
    void Update()
    {
        // Decrement cooldown time if required
        if (m_currentCooldown > 0.0f)
        {
            m_currentCooldown = Mathf.Max(m_currentCooldown -= Time.deltaTime, 0.0f);
        } 

        if(m_currentCooldown <  0.0001f){
            pressedButtonGO.SetActive(false);
            unpressedButtonGO.SetActive(true);
        }
    }

    // Check if the button can be pressed
    public bool PressButton()
    {
        if(m_currentCooldown <= 0.0f)
        {
            m_currentCooldown = m_cooldown;
            pressedButtonGO.SetActive(true);
            unpressedButtonGO.SetActive(false);
            MKAudioManager.Instance.m_pushChangeButton.GetComponent<AudioSource>().Play();
            return true;
        }

        return false;
    }
}
