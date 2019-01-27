using UnityEngine;

public class MKButtonController : MonoBehaviour
{
    //Button cooldown
    public float m_cooldown = 1.0f;
    private float m_currentCooldown = 0.0f;

    void Update()
    {
        // Decrement cooldown time if required
        if (m_currentCooldown > 0.0f)
        {
            m_currentCooldown = Mathf.Max(m_currentCooldown -= Time.deltaTime, 0.0f);
        }
    }

    // Check if the button can be pressed
    public bool PressButton()
    {
        if(m_currentCooldown <= 0.0f)
        {
            m_currentCooldown = m_cooldown;
            return true;
        }

        return false;
    }
}
