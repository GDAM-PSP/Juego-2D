using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CrearEnemigos");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CrearEnemigos()
    {
        yield return new WaitForSeconds(2);
        while (true) 
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f,10f));
        } 
    }

    public void CancelarEnemigos()
    {
        StopCoroutine("CrearEnemigos");
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < enemigos.Length; i++)
        {
            enemigos[i].GetComponent<EnemyController>().endGame = true;
        }
    }

}
