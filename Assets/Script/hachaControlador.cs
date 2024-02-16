using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hachaControlador : MonoBehaviour
{
    public PlayerMovement player;
    private float tiempoUltimaExplosion;
    [SerializeField] private float radio;
    [SerializeField] private float fuerzaDelGolpe;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radio);
            foreach (Collider2D colisionador in objetos)
            {
                Rigidbody2D rb2D = colisionador.GetComponent<Rigidbody2D>();
                if (rb2D != null)
                {
                    Vector2 direccion = colisionador.transform.position - transform.position;
                    float distancia = 1 + direccion.magnitude;
                    float fuerzaFinal = fuerzaDelGolpe / distancia;
                    rb2D.AddForce(direccion * fuerzaFinal);

                    if (colisionador.gameObject.CompareTag("Player"))
                    {
                        if (Time.time - tiempoUltimaExplosion >= 2f)
                        {
                            tiempoUltimaExplosion = Time.time;
                            player.live = player.live - 1;
                            if (player.live <= 0)
                            {
                                player.Destruir();
                            }
                            else
                            {
                                Animator animator = player.GetComponent<Animator>();
                                animator.SetTrigger("dammage");
                            }
                        }
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
