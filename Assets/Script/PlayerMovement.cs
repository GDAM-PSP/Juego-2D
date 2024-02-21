using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;

    private float Horizontal;
    public float speed;
    public float jumpForce;
    private bool Grounded;
    private Animator animator;
    private SpriteRenderer render;
    private Vector2 velocidadAnterior;
    // Variables para controlar el tiempo del último cambio en la velocidad
    float tiempoUltimoCambio = 0f;
    float tiempoEspera = 0.3f;
    public float live = 3;
    public Camera cameraPlayer;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("death") == false)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");



            if (Input.GetKeyDown(KeyCode.W) && Grounded)
            {
                animator.SetTrigger("jump");
                animator.SetBool("down", false);
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {

                //Ataque
                animator.SetTrigger("attack1");


            }

            if (Horizontal < 0)
            {
                render.flipX = true;
            }
            if (Horizontal > 0)
            {
                render.flipX = false;
            }

            Vector2 velocidadActual = Rigidbody2D.velocity;
            // Verificar si la velocidad ha cambiado
            if (velocidadActual != velocidadAnterior && Grounded)
            {
                // La velocidad ha cambiado, aquí puedes realizar acciones
                animator.SetBool("sprint", false);

                // Actualizar la velocidad anterior para la siguiente iteración
                velocidadAnterior = velocidadActual;

                // Actualizar el tiempo del último cambio
                tiempoUltimoCambio = Time.time;
            }
            else
            {
                if (Time.time - tiempoUltimoCambio >= tiempoEspera)
                {
                    animator.SetBool("sprint", true);
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (animator.GetBool("death") == false)
        {
            Rigidbody2D.velocity = new Vector2(Horizontal * speed, Rigidbody2D.velocity.y);

            Vector2 velocidadActual = Rigidbody2D.velocity;
            // Verificar si la velocidad ha cambiado
            if (velocidadActual != velocidadAnterior && Grounded)
            {
                // La velocidad ha cambiado, aquí puedes realizar acciones
                animator.SetBool("sprint", false);

                // Actualizar la velocidad anterior para la siguiente iteración
                velocidadAnterior = velocidadActual;
            }
            else
            {
                animator.SetBool("sprint", true);
            }
        }
    }
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * jumpForce);



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Grounded = true;
            animator.SetBool("down", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Grounded = false;
            animator.SetBool("down", true);
        }
    }
    public void Destruir()
    {
        cameraPlayer.transform.parent = null;
        animator.SetBool("death", true);
        StartCoroutine(Death());
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
