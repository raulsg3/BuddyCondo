using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USSimpleSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /********************************/
    /* Public methods                                                                             */
    /********************************/
    #region Public methods

    public static T Instance
    {
        get
        {
            if (m_IsDestroyed)
            {
                return null;
            }

            if (m_Instance == null)
            {
                return null;
            }

            return m_Instance;
        }
    }

    #endregion // Public methods

    /********************************/
    /* Protected methods                                                                          */
    /********************************/

    #region Protected methods

    protected virtual void Awake()
    {
        if (m_Instance != null)
        {
        }

        m_Instance = this as T;
    }

    #endregion // Protected methods

    /********************************/
    /* Private methods                                                                            */
    /********************************/

    #region Private methods 

    private void OnDestroy()
    {
        m_Instance = null;
        m_IsDestroyed = true;
    }

    #endregion // Private methods 

    /********************************/
    /* Types and private data                                                                     */
    /********************************/

    #region // Types and private data

    private static T m_Instance = null;
    private static bool m_IsDestroyed = false;

    #endregion // Types and private data

}