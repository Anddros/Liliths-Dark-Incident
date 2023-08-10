using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Vector2 moveY; // Velocidad de desplazamiento en el eje Y
    private Vector2 offset; // Desplazamiento que se aplicará al fondo
    private Material material; 

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        // Calcula el desplazamiento del movimiento en función de la velocidad
        offset = moveY * Time.deltaTime;

        // Aplica el desplazamiento al offset de la textura
        material.mainTextureOffset += offset;
    }
}