using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class NewBehaviourScript : MonoBehaviour
{
    // [Button("Name of button")]
    [Button(ButtonSizes.Large), GUIColor(0, 0.5f, 0)]
    public void Test(){
        Debug.Log("Hello");

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
