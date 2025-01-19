using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : MonoBehaviour
{
    [SerializeField] private GameObject bolaFuego; //Prefab
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private float tiempoAtaques;
    [SerializeField] private float danoAtaque;
    [SerializeField] private Transform uiTransform;

    private bool playerDetectado = false;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //StartCoroutine(RutinaAtaque());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RutinaAtaque()
    {
        while (playerDetectado)
        {
            anim.SetTrigger("atacar");
            yield return new WaitForSeconds(tiempoAtaques);
        }
    }

    private void LanzarBola()
    {
        Instantiate(bolaFuego, puntoSpawn.position, transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
        {
            //Debug.Log("Playerdetectado!!!");
            playerDetectado = true;
            StartCoroutine(RutinaAtaque());
        }  
    }

    private void OnTriggerStay2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
        {
            
            if (elOtro.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                uiTransform.rotation = new Quaternion(0, 180, 0, 1);
            }
            else
            {                
                transform.localScale = Vector3.one;
                uiTransform.rotation = Quaternion.identity;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("DeteccionPlayer"))
        {
            //Debug.Log("Sa Escapao!!");
            playerDetectado = false;
        }
        else if (elOtro.gameObject.CompareTag("PlayerHitBox"))
        {
            anim.SetBool("atacar", false);
        }
    }

}
