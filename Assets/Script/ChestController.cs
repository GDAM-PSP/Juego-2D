using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public bool haskey = false;
    public GameObject panelWin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" &&  haskey)
        {
            GetComponent<Animator>().SetTrigger("open");
            
            //activar fin del juego al ganar
            panelWin.SetActive(true);
        }
    }
}
