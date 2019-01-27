using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingletonRichardSalvation : MonoBehaviour
{
   private static SingletonRichardSalvation s_instance = null;
   public static SingletonRichardSalvation Instance
   {
       get { return s_instance; }
   }
   private static bool created = false;
   void Awake()
   {
     s_instance = this;
    }

    public Text levelText; 
    public Text timeText; 
}
