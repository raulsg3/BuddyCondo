using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKStartFirstLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        MKGame.Instance.GetFlowManager().LoadLevel(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
