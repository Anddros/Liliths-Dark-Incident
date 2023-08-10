using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BulletHellSpawner : MonoBehaviour
{
    public int numberOfColumns; // Columnas de la lluvia
    public float speed; // Velocidad de las balas
    public Sprite texture;
    public Color color;
    public float lifeTime; // Tiempo que duran las balas
    public float fireRate; // Velocidad en que se disparan las balas
    public float size;
    private float angle;
    public Material material;
    public float spinSpeed; // Velocidad de giro
    private float time;
    public LayerMask whatLayer;
    public float lastSpeed;

    private List<ParticleSystem> systems = new List<ParticleSystem>(); // Lista para almacenar las instancias de ParticleSystem

    private void Awake()
    {
        Summon();
    }

    private void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, 0, time * spinSpeed); // Rotar el objeto
    }

    void Summon()
    {
        angle = 360f / numberOfColumns;

        for (int i = 0; i < numberOfColumns; i++)
        {
            Material particleMaterial = material;

            var go = new GameObject("Particle System");
            go.transform.Rotate(angle * i, 90, 0);
            go.transform.parent = this.transform;
            go.transform.position = this.transform.position;
            var system = go.AddComponent<ParticleSystem>();
            systems.Add(system); // Agregar la instancia de ParticleSystem a la lista
            go.GetComponent<ParticleSystemRenderer>().material = particleMaterial;
            var mainModule = system.main;
            mainModule.startColor = Color.green;
            mainModule.startSize = 0.5f;
            mainModule.startSpeed = speed;
            mainModule.maxParticles = 1000000;
            mainModule.simulationSpace = ParticleSystemSimulationSpace.World;

            var collModule = system.collision;
            collModule.enabled = true;
            collModule.type = ParticleSystemCollisionType.World;
            collModule.mode = ParticleSystemCollisionMode.Collision2D;
            collModule.sendCollisionMessages = true;
            collModule.collidesWith = whatLayer;
            collModule.lifetimeLoss = 1;

            var lifeSpeed = system.velocityOverLifetime;
            lifeSpeed.enabled = true;
            lifeSpeed.y = lastSpeed;

            var emission = system.emission;
            emission.enabled = false;

            var shape = system.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Sprite;
            shape.sprite = null;

            var text = system.textureSheetAnimation;
            text.enabled = true;
            text.mode = ParticleSystemAnimationMode.Sprites;
            text.AddSprite(texture);
        }

        InvokeRepeating("DoEmit", 2.0f, fireRate);
    }

    void DoEmit()
    {
        foreach (var system in systems) // Iterar sobre la lista de sistemas
        {
            var emitParams = new ParticleSystem.EmitParams();
            emitParams.startColor = color;
            emitParams.startSize = size;
            emitParams.startLifetime = lifeTime;
            system.Emit(emitParams, 10);
        }
    }

    public void StopEmit()
    {
        foreach (var system in systems)
        {
            system.Stop(); // Detener la emisión de partículas en cada sistema
        }
    }

    public void ClearParticles()
    {
        // Obtener todas las instancias de ParticleSystem
        var particleSystems = GetComponentsInChildren<ParticleSystem>().ToList();

        // Limpiar todas las partículas en cada sistema
        foreach (var system in particleSystems)
        {
            system.Clear();
        }
    }
}