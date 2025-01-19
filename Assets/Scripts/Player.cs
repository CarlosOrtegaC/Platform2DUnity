using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputH;
    private int score = 0;
    [SerializeField] private TextMeshProUGUI textoScore;
    [SerializeField] private AudioClip sonidoMoneda;
    private AudioSource audioSource;

    [Header("Sistema de Movimiento")]
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private float distanciaDeteccionSuelo;
    [SerializeField] private Transform uiTransform;
    [SerializeField] private Transform pies;
    [SerializeField] private LayerMask queEsSaltable;
    [SerializeField] private GameObject pantallaMuerte;

    [Header ("Sistema de Combate")]
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float danoAtaque;
    [SerializeField] private LayerMask queEsDanable;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento();

        Saltar();

        LanzarAtaque();
    }

    private void LateUpdate()
    {
        uiTransform.rotation = Quaternion.identity;
    }

    private void LanzarAtaque()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
    }

    //Se ejecuta desde evento de animación.
    private void Ataque()
    {
        //LANZA EVENTO INSTANTANEO DESDE TRIGGER DE ANIMACIÓN
        Collider2D[] collidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDanable);
        foreach (Collider2D item in collidersTocados)
        {
            SistemaVidas sistemaVidas = item.gameObject.GetComponent<SistemaVidas>();
            sistemaVidas.RecibirDano(danoAtaque);
        }
    }

    private void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstoyEnSuelo())
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            anim.SetTrigger("saltar");
        }
    }

    private bool EstoyEnSuelo()
    {
        Debug.DrawRay(pies.position, Vector3.down, Color.red, 0.3f);
        return Physics2D.Raycast(pies.position, Vector3.down, distanciaDeteccionSuelo, queEsSaltable);
    }

    private void Movimiento()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * velocidadMovimiento, rb.velocity.y);
        if (inputH != 0) //Hay movimiento
        {
            anim.SetBool("running", true);
            if (inputH > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            anim.SetBool("running", false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }

    public void EstaMuerto()
    {
        pantallaMuerte.SetActive(true);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D elOtro)
    {
        if (elOtro.gameObject.CompareTag("Moneda"))
        {
            Destroy(elOtro.gameObject); // Destruye la moneda
            score++;
            textoScore.text = "x " + score;

            // Reproduce el sonido solo al recoger la moneda
            if (sonidoMoneda != null)
            {
                audioSource.PlayOneShot(sonidoMoneda, 0.5f);
            }
        }
    }
}
