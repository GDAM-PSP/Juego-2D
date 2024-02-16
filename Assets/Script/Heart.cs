using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Heart : MonoBehaviour
{
    public Image imagen;
    public Image imagen2;
    public Image imagen3;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(player.live == 3)
        {
            imagen.enabled = true;
            imagen2.enabled = true;
            imagen3.enabled = true;
        }
        else if(player.live == 2)
        {
            imagen.enabled = true;
            imagen2.enabled = true;
            imagen3.enabled = false;
        } 
        else if(player.live == 1)
        {
            imagen.enabled = true;
            imagen2.enabled = false;
            imagen3.enabled = false;
        }
        else if (player.live == 0)
        {
            imagen.enabled = false;
            imagen2.enabled = false;
            imagen3.enabled = false;
        }
    }
}
