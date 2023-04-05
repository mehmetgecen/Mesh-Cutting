using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenLineRenderer : MonoBehaviour {

    // Line Drawn event handler
    public delegate void LineDrawnHandler(Vector3 begin, Vector3 end, Vector3 depth);
    public event LineDrawnHandler OnLineDrawn;

    bool dragging;
    Vector3 start;
    Vector3 end;
    Camera cam;

    public GameObject knifeEdge;

    public Material lineMaterial;

    // Use this for initialization
    void Start () {
        cam = Camera.main;
        dragging = false;
    }

    private void OnEnable()
    {
        Camera.onPostRender += PostRenderDrawLine;
    }

    private void OnDisable()
    {
        Camera.onPostRender -= PostRenderDrawLine;
    }

    // Update is called once per frame
    void Update () {
        
        
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Knife"))
        {
            Debug.Log("Entered..");
            
            if (!dragging) //OnTriggerEnter()
            {
                Debug.Log("Slice Started");
                start = cam.ScreenToViewportPoint(knifeEdge.transform.position);
                dragging = true;
            }

            if (dragging)
            {
                Debug.Log("Slice Continue...");
                end = cam.ScreenToViewportPoint(knifeEdge.transform.position);
            }  
        }
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit...");
        
        if (other.gameObject.CompareTag("Knife"))
        {
            if (dragging) // to do : must be linked with isTriggerExit()
            {
                Debug.Log("Slice Ended.");
                
                // Finished dragging. We draw the line segment
                end = cam.ScreenToViewportPoint(knifeEdge.transform.position); 
                dragging = false;

                var startRay = cam.ViewportPointToRay(start);
                var endRay = cam.ViewportPointToRay(end);

                // Raise OnLineDrawnEvent
                OnLineDrawn?.Invoke(
                    startRay.GetPoint(cam.nearClipPlane),
                    endRay.GetPoint(cam.nearClipPlane),
                    endRay.direction.normalized);
            }
        }
        
    }


    /// <summary>
    /// Draws the line in viewport space using start and end variables
    /// </summary>
    private void PostRenderDrawLine(Camera cam)
    {
        if (dragging && lineMaterial)
        {
            GL.PushMatrix();
            lineMaterial.SetPass(0);
            GL.LoadOrtho();
            GL.Begin(GL.LINES);
            GL.Color(Color.black);
            GL.Vertex(start);
            GL.Vertex(end);
            GL.End();
            GL.PopMatrix();
        }
    }
}
