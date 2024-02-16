using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour
{
    Transform player;
    public GameObject Player;
    SpriteRenderer spriteRenderer;
    float speed = 3f;
    private Animator animator;
    public float live;
    bool CargaMinotauro = true;
    private float tiempoTranscurrido = 0f;
    private Rigidbody2D rb; // Referencia al Rigidbody del goblin
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("PlayerRed").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        live = 1000f;
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("CargaMinotauroTiempo", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null && Player.activeSelf || animator.GetBool("death"))
        {
            
                if (live <= 0)
                {
                    StartCoroutine(EnemigoMuere());
                }
                {
                    if (animator.GetBool("embestida"))
                    {
                        // Aumenta el tiempo transcurrido en cada fotograma
                        tiempoTranscurrido += Time.deltaTime;
                        if (tiempoTranscurrido > 2f)
                        {
                            animator.SetBool("embestida", false);
                            animator.SetBool("run", true);
                            tiempoTranscurrido = 0f;
                        }
                    }
                    float distance = Mathf.Abs(Player.transform.position.x - transform.position.x);
                    if (distance < 10.0f && distance > 5.0f && CargaMinotauro)
                    {
                        animator.SetBool("attack1", false);
                        animator.SetBool("run", false);
                        //StartCoroutine(EnemigoCarga());
                        CargaMinotauro = false;
                        animator.SetBool("embestida", false);
                        animator.SetBool("carga", false);


                    }
                    if (distance < 5.0f && animator.GetBool("carga") == false && animator.GetBool("embestida") == false || distance < 10.0f && CargaMinotauro == false)
                    {
                        animator.SetBool("attack1", false);
                        animator.SetBool("run", true);
                        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                        if (transform.position.x < player.position.x)
                        {
                            spriteRenderer.flipX = false;
                        }
                        else
                        {
                            spriteRenderer.flipX = true;
                        }
                    }
                    else
                    {
                        animator.SetBool("run", false);
                    }
                    if (distance < 2.0f && animator.GetBool("carga") == false && animator.GetBool("embestida") == false)
                    {
                        animator.SetBool("run", false);
                        animator.SetBool("attack1", true);
                    }
                
            }
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("attack1", false);
            animator.SetBool("embestida", false);
            animator.SetBool("carga", true);
        }
    }
    void Die()
    {
        // Coloca aquí cualquier lógica adicional que desees realizar cuando el minotauro muera
        Debug.Log("El Minotauro ha muerto.");
        Destroy(gameObject); // Destruye el GameObject del goblin
    }
    IEnumerator EnemigoMuere()
    {
        animator.SetBool("death", true);
        // Desactiva el componente Rigidbody del GameObject
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Esto hace que el Rigidbody no sea afectado por fuerzas
        }
        yield return new WaitForSeconds(3f); // Espera 2 segundos
        Die();
    }
    IEnumerator EnemigoCarga()
    {
        if (!animator.GetBool("embestida") && !CargaMinotauro)
        {
            animator.SetBool("carga", true);
        }
        yield return new WaitForSeconds(2f); // Espera 2 segundos
        if (!animator.GetBool("death") == false)
        {
            animator.SetBool("carga", false);
            animator.SetBool("embestida", true);

            // Calcula la posición final de la embestida
            Vector3 posicionFinal = player.position + new Vector3(20f, 0f, 0f);

            // Mueve al enemigo hacia la posición final
            transform.position = Vector3.MoveTowards(transform.position, posicionFinal, speed * Time.deltaTime);

            // Voltea al enemigo según su dirección de movimiento
            if (transform.position.x < player.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }

            // Verifica si el enemigo ha alcanzado la posición final
            if (transform.position == posicionFinal)
            {
                animator.SetBool("embestida", false);
                CargaMinotauro = false;
            }

            // Espera al siguiente frame
            yield return null;
        }
        
    }
    IEnumerator EsperarCarga()
    {
        yield return new WaitForSeconds(10);
        CargaMinotauro = true;
    }

}
