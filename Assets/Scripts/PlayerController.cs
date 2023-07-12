using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Float Variables
    private float moveSpeed = 7.5f;
    private float bulletCooldown = 0f;

    //Vector Variables
    [SerializeField] private Vector2 input;
    [SerializeField] private Vector2 finalInput;

    //Components
    [SerializeField] private new Rigidbody2D rigidbody2D;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private Animator animator;

    //Bullet Controller
    [SerializeField] private GameObject bullet;
    private List<GameObject> pool = new List<GameObject>();

    //Health
    private float maxHealth = 250f;
    [SerializeField] private float currentHealth;

    [SerializeField] private LillithHealthBar lillithHealthBar;

    public AudioSource audioSource;


    private void Start() 
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        lillithHealthBar.StartHealthBar(currentHealth);
    }

    private void Update() 
    {
        ReadInput();
        animator.SetFloat("Move", Mathf.Abs(finalInput.magnitude));
    }

    private void FixedUpdate() 
    {
        Move();
        Shoot();
    }

    private void ReadInput()
    {
        input = playerInput.actions["Move"].ReadValue<Vector2>();
        finalInput = new Vector2(input.x, input.y).normalized;
    }

    private void Move()
    {
        rigidbody2D.MovePosition(moveSpeed * Time.fixedDeltaTime * finalInput + rigidbody2D.position);
    }

    private void Shoot()
    {
        bulletCooldown += Time.fixedDeltaTime;
        if (playerInput.actions["Shoot"].ReadValue<float>() > 0f && bulletCooldown >= 0.055f)
        {
            GameObject obj = GetBullet();
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
            bulletCooldown = 0;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    private void TakeDamage()
    {
        currentHealth -= 5f;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        lillithHealthBar.ChangeCurrentHealth(currentHealth);
    }

    //Bullet Spawner
    public GameObject GetBullet()
    {
        //Reuse Prefab
        for (int i = 0; i < pool.Count; i++)
        {
            //If deactivated in hierrarchy
            if (!pool[i].activeInHierarchy)
            {
                //Activate again
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        //Spawn Prefab
        GameObject obj = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        pool.Add(obj);
        return obj;
    }

    void OnParticleCollision(GameObject other)
    {
        TakeDamage();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "enemy")
        {
            TakeDamage();
        }
    }

    public void AddHealth()
    {
        currentHealth += 20;
    }
}