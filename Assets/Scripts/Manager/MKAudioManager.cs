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
    
    public AudioSource m_pushChangeButton;
    public AudioSource m_movingBox1;
    public AudioSource m_movingBox2;
    public AudioSource m_movingBox3;
    public AudioSource m_boxInPlace;
    public AudioSource m_ambientalMusic;
    public AudioSource m_grabbingstuf;
    public AudioSource m_fanfare;
    public AudioSource m_3;
    public AudioSource m_2;
    public AudioSource m_1;
    public AudioSource m_go;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
