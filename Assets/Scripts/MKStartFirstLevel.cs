﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MKStartFirstLevel : MonoBehaviour
{

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        MKGame.Instance.GetFlowManager().LoadLevel(1);
    }

  
}
