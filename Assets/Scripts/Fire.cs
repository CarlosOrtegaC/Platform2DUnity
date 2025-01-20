using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float danoAtaqueCast;
    [SerializeField] private int tiempoFire;
    [SerializeField] private LayerMask queEsSuelo;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tiempoFire);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            collision.gameObject.GetComponent<SistemaVidas>().RecibirDano(danoAtaqueCast);    
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Suelo"))
        {
            Destroy(gameObject);
        }
    }
}



