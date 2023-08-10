using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LillithBulletController : MonoBehaviour
{
    [SerializeField] private float speed; // La velocidad a la que se mueve la bala

    private void Update() 
    {
        // Mueve la bala hacia arriba con base en la velocidad
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        // Verifica si la bala colisiona con un objeto con etiqueta "pooling" o "enemy"
        if (collision.tag == "pooling" || collision.tag == "enemy")
        {
            gameObject.SetActive(false); // Desactiva la bala
        }
    }
}
