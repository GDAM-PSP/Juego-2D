using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public List<GoblinController> enemigos = new List<GoblinController>();
    public List<Minotaur> enemigosMinotauros = new List<Minotaur>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // CompareTag para comparar tags, es más eficiente que comparar strings.
        {
            GoblinController goblin = collision.gameObject.GetComponent<GoblinController>();
            Animator animator = goblin.GetComponent<Animator>();

            if (lessLive(goblin))
            {

            }
            else
            {
                Rigidbody2D rb = goblin.GetComponent<Rigidbody2D>();
                // Aplicar una fuerza de empuje al goblin cuando recibe un golpe
                rb.AddForce(new Vector2(6f, 0f), ForceMode2D.Impulse);
                animator.SetTrigger("hit");
                Debug.Log("La vida del enemigo es: " + goblin.live);
            }
        }
        if (collision.gameObject.CompareTag("boss")) // CompareTag para comparar tags, es más eficiente que comparar strings.
        {
            Minotaur minotaur = collision.gameObject.GetComponent<Minotaur>();
            Animator animator = minotaur.GetComponent<Animator>();

            if (lessLive(minotaur))
            {

            }
            else
            {
                Rigidbody2D rb = minotaur.GetComponent<Rigidbody2D>();
                // Aplicar una fuerza de empuje al goblin cuando recibe un golpe
                rb.AddForce(new Vector2(3f, 0f), ForceMode2D.Impulse);
                animator.SetTrigger("hit");
                Debug.Log("La vida del enemigo es: " + minotaur.live);
            }
        }
    }



    private bool lessLive(GoblinController goblin)
    {
        goblin.live -= 50; // Reduce la vida del Goblin

        if (goblin.live <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool lessLive(Minotaur minotaur)
    {
        minotaur.live -= 50; // Reduce la vida del Goblin

        if (minotaur.live <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
