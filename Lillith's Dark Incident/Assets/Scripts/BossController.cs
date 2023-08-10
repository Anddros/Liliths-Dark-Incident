using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Intervalo de tiempo entre cambios de estado
    public float stateChangeInterval = 3f;

    // Probabilidades de cada estado
    public float[] stateProbabilities = { 0.4f, 0.3f, 0.3f };

    // Estado actual del jefe
    private int currentState = 2;

    // Tiempo transcurrido desde el último cambio de estado
    private float timer = 0f;

    // Velocidad de movimiento para el método Move()
    public float moveSpeed = 5f;

    // Posición inicial y de destino para el movimiento de Move()
    private Vector3 initialPosition;
    private Vector3 moveDestination;

    // Movimiento Horizontal
    public float amplitude = 4f; // Amplitud del movimiento horizontal
    public float frequency = 1f; // Frecuencia del movimiento horizontal

    //Detener Movimiento
    private bool continueHorizontalMovement = true;

    //Transform de jugador
    public Transform player;
    private bool isRushing = false; // Variable para controlar el movimiento en el estado "Rush"

    //Centro de pantalla
    private Vector3 center = new Vector3(0, 0, 0);


    // Referencia al objeto hijo
    private GameObject bulletHellSpawner;
    // Referencia al script BulletHellSpawner
    private BulletHellSpawner bulletHellSpawnerScript;

    //Vida jefe
    private float maxHealth = 15000f;
    [SerializeField] private float currentHealth;

    [SerializeField] private BossHealthBar bossHealthBar;


    private void Start()
    {
        // Guardar la posición inicial del objeto
        initialPosition = transform.position;

        // Calcular la posición de destino para el movimiento de Move()
        moveDestination = new Vector3(0f, 3f, 0f);

        // Obtener referencia al objeto hijo
        bulletHellSpawner = transform.GetChild(0).gameObject;

        // Obtener referencia al script BulletHellSpawner en el objeto hijo
        bulletHellSpawnerScript = transform.GetChild(0).GetComponent<BulletHellSpawner>();

        // Determinar vida máxima del jefe
        currentHealth = maxHealth;

        bossHealthBar.StartHealthBar(currentHealth);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Verificar si es tiempo de cambiar de estado
        if (timer >= stateChangeInterval)
        {
            // Reiniciar el temporizador
            timer = 0f;

            // Determinar el nuevo estado utilizando el método Choose()
            currentState = (int)Choose(stateProbabilities);

            // Llamar al método correspondiente al estado actual
            switch (currentState)
            {
                case 0:
                    Move();
                    break;
                case 1:
                    Rush();
                    break;
                case 2:
                    BulletHell();
                    break;
            }
        }
    }

    private float Choose(float[] probs)
    {
        float total = 0;
        foreach (float elem in probs)
        {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else
            {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }

    private void Move()
    {
        Debug.Log("Estado Move() cumplido");

        continueHorizontalMovement = true;

        if (isRushing)
        {
        // Si el jefe estaba en el estado 1 (Rush), iniciar la corrutina MoveToDestination para regresar a la posición de destino original
            StopAllCoroutines();
            StartCoroutine(MoveToDestination(moveDestination));
        }

        // Iniciar la interpolación lineal desde la posición actual hacia la posición de destino
        StartCoroutine(MoveToDestination(moveDestination));

        // Eliminar todas las partículas existentes en el objeto hijo
        bulletHellSpawnerScript.ClearParticles();

        // Activar el objeto hijo (lluvia de balas)
        bulletHellSpawner.SetActive(true);

        // Desactivar la lluvia de balas si estaba activa
        bulletHellSpawnerScript.enabled = false;
    }

    private void Rush()
    {   
        Debug.Log("Estado Rush() cumplido");
        StopAllCoroutines();
        continueHorizontalMovement = false;
        isRushing = false;

        // Detener la emisión de partículas antes de desactivar el objeto hijo
        bulletHellSpawnerScript.StopEmit();

        // Eliminar todas las partículas existentes en el objeto hijo
        bulletHellSpawnerScript.ClearParticles();

        // Desactivar el objeto hijo (lluvia de balas) si está activo
        bulletHellSpawner.SetActive(false);

        StartCoroutine(RushTowardsPlayer());
    }

    private void BulletHell()
    {
        Debug.Log("Estado BulletHell() cumplido");
        StopAllCoroutines();
        continueHorizontalMovement = false;

        // Detener la emisión de partículas antes de activar el objeto hijo
        bulletHellSpawnerScript.StopEmit();

        // Eliminar todas las partículas existentes en el objeto hijo
        bulletHellSpawnerScript.ClearParticles();

        // Activar el objeto hijo (lluvia de balas)
        bulletHellSpawner.SetActive(true);

        // Activar la lluvia de balas
        bulletHellSpawnerScript.enabled = true;
        
        StartCoroutine(MoveToDestination(center));
    }

    private IEnumerator MoveToDestination(Vector3 destination)
    {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, destination);
        float duration = distance / moveSpeed * 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calcular la posición intermedia en función del tiempo transcurrido y la velocidad
            float t = elapsedTime / duration;
            Vector3 newPosition = Vector3.Lerp(startPosition, destination, t);

            // Actualizar la posición del objeto
            transform.position = newPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la posición final sea exactamente la posición de destino
        transform.position = destination;

        // Agregar movimiento horizontal
        StartCoroutine(HorizontalMovement());
    }

    private IEnumerator HorizontalMovement()
    {
        Vector3 startPosition = transform.position;
        float localElapsedTime = 0f;

        while (continueHorizontalMovement)
        {
            localElapsedTime += Time.deltaTime;

            // Calcular la posición horizontal en función del tiempo transcurrido
            float horizontalOffset = amplitude * Mathf.Sin(2f * Mathf.PI * frequency * localElapsedTime);

            // Calcular la nueva posición del objeto
            Vector3 newPosition = new Vector3(startPosition.x + horizontalOffset, startPosition.y, startPosition.z);

            // Actualizar la posición del objeto
            transform.position = newPosition;

            yield return null;
        }
    }

    private Vector3 GetPlayerPosition()
    {
        return player.position;
    }

    private IEnumerator RushTowardsPlayer()
    {
        isRushing = true; // Establecer el flag de movimiento "Rush" en true

        while (isRushing)
        {
           Vector3 playerPosition = GetPlayerPosition();
           yield return StartCoroutine(MoveToDestination(playerPosition));
           yield return new WaitForSeconds(0.4f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bullet")
        {
            MinusDamage();
        }
    }

    private void MinusDamage()
    {
        currentHealth -= 15;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        bossHealthBar.ChangeCurrentHealth(currentHealth);
    }
}