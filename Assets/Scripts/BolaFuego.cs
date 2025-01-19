using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFuego : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float impulsoDisparo;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //transfor.forward -> Eje Z (Azul)
        //transfor.up -> Eje Y (Verde)
        //transfor.right -> Eje X (Rojo)   
        rb.AddForce(transform.right * impulsoDisparo, ForceMode2D.Impulse);
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D elOtro)
    {
        if (elOtro.gameObject.layer==8)
        {
          
            Debug.Log("Playerdetectado!!!");
            anim.SetBool("explotar", true);
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }
        else if (elOtro.gameObject.CompareTag("PlayerHitBox"))
        {
            SistemaVidas sistemaVidas = elOtro.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.RecibirDano(20);
            anim.SetBool("explotar", true);
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
        }
    }

    public void FinExplosion()
    {
        Destroy(gameObject);
    }
}
