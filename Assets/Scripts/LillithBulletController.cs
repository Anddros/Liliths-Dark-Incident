using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LillithBulletController : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Update() 
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "pooling" || collision.tag == "enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
