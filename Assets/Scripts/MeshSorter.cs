using UnityEngine;

public class MeshSorter : MonoBehaviour 
{
    public string sortingLayer = "Default";
    public int orderInLayer = 0;
    
    void Start() 
    {
        var renderer = GetComponent<MeshRenderer>();
        renderer.sortingLayerName = sortingLayer;
        renderer.sortingOrder = orderInLayer;
    }
}