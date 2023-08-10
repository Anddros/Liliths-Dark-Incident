using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Vector2 moveY;
    private Vector2 offset;
    private Material material;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        offset = moveY * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
