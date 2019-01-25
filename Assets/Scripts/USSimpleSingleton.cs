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
                Debug.LogError("Someone is trying to access the singleton of " + typeof(T).ToString() + " after being destroyed");
                return null;
            }

            if (m_Instance == null)
            {
                Debug.LogError("Someone is trying to access the singleton of " + typeof(T).ToString() + " without being initialized");
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
            Debug.LogError("There is more than one instance of " + this.GetType().Name);
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