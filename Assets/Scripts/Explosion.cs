using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Cloner _cloner;
    
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private void Awake()
    {
        _cloner.Spawned += SpawnedExplosion;
        _cloner.Destroyed += DestroyedExplosion;
    }
    
    private void OnDisable()
    {
        _cloner.Spawned -= SpawnedExplosion;
        _cloner.Destroyed -= DestroyedExplosion;
    }
    
    private void SpawnedExplosion(GameObject[] gameObjects)
    {
        List<Rigidbody> explodingObjects = gameObjects
            .Select(gameObject => gameObject
            .GetComponent<Rigidbody>()).ToList();
        
        foreach (var rigidbody in explodingObjects)
        {
            rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private void DestroyedExplosion(float explosionMultiplier)
    {
        _explosionForce *= explosionMultiplier;
        _explosionRadius *= explosionMultiplier;

        List<Collider> hits = Physics.OverlapSphere(transform.position, _explosionRadius).ToList();

        foreach (var hit in hits)
        {
            if(hit.attachedRigidbody != null)
            hit.attachedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}