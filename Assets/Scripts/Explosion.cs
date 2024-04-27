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
        _cloner.Spawned += Init;
    }
    
    private void OnDisable()
    {
        _cloner.Spawned -= Init;
    }
    
    private void Init(GameObject[] gameObjects)
    {
        List<Rigidbody> explodingObjects = gameObjects.Select(gameObject => gameObject.GetComponent<Rigidbody>()).ToList();
        
        foreach (var rigidbody in explodingObjects)
        {
            rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}
