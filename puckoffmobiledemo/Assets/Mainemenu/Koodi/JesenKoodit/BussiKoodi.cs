using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BussiKoodi : MonoBehaviour
{

    public Transform[] napit;

    void Start()
    {
        this.gameObject.transform.position = napit[0].position;
      
    }


    public void levelClicked(int btnNumber)
    {
        if (gameObject.transform.position == napit[btnNumber].position)
        {
            //levelSelectionPanels[_mapIndex].gameObject.SetActive(true);
            //mapSelectionPanel.gameObject.SetActive(false);
        }


        //this.gameObject.transform.position = napit[btnNumber].position;

        
    }


    //void Update()
    //{
        
    //}
}
