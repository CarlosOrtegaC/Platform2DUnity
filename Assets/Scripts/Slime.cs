using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float velocidadPatrulla;
    [SerializeField] private float danoAtaque;
    [SerializeField] private Transform uiTransform;
    private Vector3 destinoActual;
    private int indiceActual = 0;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        destinoActual = waypoints[indiceActual].position;
        StartCoroutine(Patrulla());
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update() //S = V * t
    {
        
    }
    IEnumerator Patrulla()
    {
        while (true)
        {
            while (transform.position != destinoActual)
            {
                transform.position = Vector3.MoveTowards(transform.position, destinoActual, velocidadPatrulla * Time.deltaTime);
                yield return null;
            }
            //Has llegado a destino
            DefinirNuevoDestino();
        }  
    }
    private void DefinirNuevoDestino()
    {
        indiceActual++;
        if (indiceActual >= waypoints.Length)
        {
            indiceActual = 0;
        }
        destinoActual = waypoints[indiceActual].position;
        EnfocarDestino();
    }
    private void EnfocarDestino()
    {
        if (destinoActual.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
            uiTransform.rotation = Quaternion.identity;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            uiTransform.rotation = new Quaternion(0, 180, 0, 1);

        }
    }
    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag ("DeteccionPlayer"))
        {
            Debug.Log("Playerdetectado!!!");
            anim.SetBool("atacar", true);
        }
        else if(elOtro.gameObject.CompareTag("PlayerHitBox"))
        {
            SistemaVidas sistemaVidas = elOtro.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.RecibirDano(20);
            anim.SetBool("atacar", true);
        }
    }
    private void OnTriggerExit2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
        {
            Debug.Log("Sa Escapao!!");
            anim.SetBool("atacar", false);

        }
        else if (elOtro.gameObject.CompareTag("PlayerHitBox"))
        {
            anim.SetBool("atacar", false);
        }
    }

}
