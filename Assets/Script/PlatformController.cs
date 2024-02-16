using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{

    public Transform destino;
    private Vector3 start, end;
    [SerializeField] float speed = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        end = destino.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,destino.position,speed*Time.deltaTime);
        if(transform.position == destino.position)
        {
            if(destino.position == end)
            {
                destino.position = start;
            }
            else
            {
                destino.position = end;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
    }
}
