using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Transform origin, destination;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, origin.position);   
        lineRenderer.SetPosition(1, destination.position);   
    }
}