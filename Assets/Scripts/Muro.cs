using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muro : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private AudioClip sonidoMuro;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void MoverMuro()
    {
        if (sonidoMuro != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoMuro);
        }

        StartCoroutine(MoverMuroArriba());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player TieneKey) && TieneKey.key > 0)
        {
            StartCoroutine(MoverMuroArriba());
        }
    }

    private IEnumerator MoverMuroArriba()
    {
        Vector3 muroPosicion = transform.position + new Vector3(0, 1, 0) * 4f;
        while (Vector3.Distance(transform.position, muroPosicion) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, muroPosicion, Time.deltaTime * 1f);
            yield return null;
        }
    }
}
