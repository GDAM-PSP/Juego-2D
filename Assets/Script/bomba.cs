using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomba : MonoBehaviour
{
    [SerializeField] private float radio;
    [SerializeField] private float fuerzaExplosion;
    GoblinController goblinController;
    private GameObject player; // Referencia al objeto del jugador
    private PlayerMovement playerScript; // Referencia al script del jugador
    public GameObject jugador; // Referencia al GameObject del jugador
    public Vector3 offset; // Offset de posición para la cámara fuera del jugador
    private bool puedeDanar = true;
    private float tiempoUltimaExplosion;


    // Start is called before the first frame update
    void Start()
    {
        goblinController = GetComponent<GoblinController>();
        // Encuentra al objeto del jugador por su tag al comienzo del juego
        player = GameObject.FindWithTag("Player");

        // Comprueba si se encontró al jugador
        if (player == null)
        {
            Debug.LogError("No se encontró al jugador en la escena. Asegúrate de que el GameObject del jugador tenga el tag 'Player'.");
        }
        else
        {
            // Obtén el componente de script asociado al GameObject del jugador
            playerScript = player.GetComponent<PlayerMovement>();

            // Comprueba si se encontró el script del jugador
            if (playerScript == null)
            {
                Debug.LogError("No se encontró el script del jugador en el GameObject del jugador.");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void Explosion()
    {
        if (puedeDanar)
        {
            Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radio);
            foreach (Collider2D colisionador in objetos)
            {
                Rigidbody2D rb2D = colisionador.GetComponent<Rigidbody2D>();
                if (rb2D != null)
                {
                    Vector2 direccion = colisionador.transform.position - transform.position;
                    float distancia = 1 + direccion.magnitude;
                    float fuerzaFinal = fuerzaExplosion / distancia;
                    rb2D.AddForce(direccion * fuerzaFinal);

                    if (colisionador.gameObject.CompareTag("Player"))
                    {
                        if (Time.time - tiempoUltimaExplosion >= 1f)
                        {
                            tiempoUltimaExplosion = Time.time;
                            playerScript.live = playerScript.live - 1;
                            if (playerScript.live <= 0)
                            {
                                playerScript.Destruir();
                            }
                            else
                            {
                                Animator animator = playerScript.GetComponent<Animator>();
                                animator.SetTrigger("dammage");
                            }
                        }
                    }
                }
            }
            //Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }

}
