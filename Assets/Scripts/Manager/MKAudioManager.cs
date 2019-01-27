using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKAudioManager : MonoBehaviour
{
    #region Singleton
    private static MKAudioManager s_instance = null;
    public static MKAudioManager Instance
    {
        get { return s_instance; }
    }
    void Awake()
    {
        s_instance = this;
    }
    #endregion

    public AudioSource m_AudioTest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
