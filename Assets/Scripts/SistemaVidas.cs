using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas;
    [SerializeField] private Slider sliderPlayer;
    [SerializeField] private UnityEvent muerte;

    public void RecibirDano(float danoRecibido)
    {
        vidas -= danoRecibido;
        sliderPlayer.value = vidas;
        if (vidas <= 0)
        {
            if (muerte != null)
            {
                muerte.Invoke();
            }
            Destroy(this.gameObject); 
        }
    }


}
