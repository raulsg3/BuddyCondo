using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKRankingManager : MonoBehaviour
{
    //REFERENSIA A LO QUE TIENE LOS DATOS DE LA PARTIDA
    //public MKGameManager gameManager = MKGame.Instance.GetGameManager();
    public MKRankingContent m_MKRankingContent;

    //[Serializable]
    public void ToJSON()
    {
        MKScore rank = new MKScore("a", "b", 0, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_MKRankingContent = MKGame.GetGameContent().GetRankingContent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
