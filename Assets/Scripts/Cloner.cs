using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cloner : MonoBehaviour, IInteractive
{
    [SerializeField] private int _maxNumberOfCopies;
    [SerializeField] private int _minNumberOfCopies;
    [SerializeField] private float _copyParameterMultiplier = 0.5f;

    private float _creationChance = 1f;

    protected float _multiplierRelativeOfBase = 1f;

    public event Action<GameObject[]> Spawned;
    public event Action<float> Destroyed;

    public void Click() => Create();

    private void Create()
    {
        bool isCreationPossible =  Random.Range(0f, 1f) <= _creationChance;

        if (isCreationPossible == false)
        {
            float explosionMultiplier = 1.0f / _multiplierRelativeOfBase;
            Destroyed?.Invoke(explosionMultiplier);

            Destroy(gameObject);
            return;
        }

        List<Cloner> childrens = new List<Cloner>();
        int numberOfCopies = GetNumberOfCopies();

        for (int i = 0; i < numberOfCopies; i++)
            childrens.Add(BuildClone());
        
        Spawned?.Invoke(childrens.Select(children => children.gameObject).ToArray());
        
        Destroy(gameObject);
    }

    private Cloner BuildClone()
    {
        var offsetX = Random.Range(-1f, 1f);
        var offsetY = 0.1f;
        var offsetZ = Random.Range(-1f, 1f);

        var children = Instantiate(this, gameObject.transform.position + new Vector3(offsetX, offsetY, offsetZ),Quaternion.identity);

        Vector3 scale = gameObject.transform.localScale * _copyParameterMultiplier;
        children.gameObject.transform.localScale = scale;
        children._multiplierRelativeOfBase = _multiplierRelativeOfBase * _copyParameterMultiplier;

        var newCreationChance = _creationChance * _copyParameterMultiplier;
        children._creationChance = newCreationChance;

        return children;
    }

    private int GetNumberOfCopies()
    {
        return Random.Range(_minNumberOfCopies, _maxNumberOfCopies);
    }
}