using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    private void Awake() => SetRandomColor();

    private void SetRandomColor()
    {
        var red = Random.Range(0f, 1f);
        var green = Random.Range(0f, 1f);
        var blue = Random.Range(0f, 1f);
        
        gameObject.GetComponent<MeshRenderer>().material.color = new Vector4(red, green, blue, 1f);
    }
}
