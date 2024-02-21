using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour
{
    PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si el objeto con el que se colisiona tiene la etiqueta "Obstaculo"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destruye este objeto
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            player.Destruir();
        }
    }
}
