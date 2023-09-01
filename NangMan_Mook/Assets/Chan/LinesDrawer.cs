using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class LinesDrawer : MonoBehaviour
{

    public GameObject linePrefab;
    public LayerMask[] cantDrawOverLayer;
    //int canDrawOverLayerIndex;

    [Space(30f)]
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;
    [SerializeField] private float MaxlineWidth = 0;
    [SerializeField] private float MinlineWidth = 0;

    public bool isPaused = false;
    public bool Drawing = false;
    protected float wheelInput;
    Line currentLine;

    private List<Line> lines;
    [SerializeField] private int dc = 5; 

    Camera cam;

    void Start()
    {
        cam = Camera.main;
        lines = new List<Line>();

        Line.OnAnyLineDestroy += currentLine_OnAnyLineDestroy;
        //canDrawOverLayerIndex = LayerMask.NameToLayer("Platform");
    }


    void Update()
    {
        if (isPaused)
        {
            DrawLine();
            wheelInput = Input.GetAxis("Mouse ScrollWheel");
        }
    }

    private void DrawLine()
    {
        if (dc > 0 && Input.GetMouseButtonDown(0))
        {
            BeginDraw();
        }
        if (currentLine != null)
        {
            Draw();
        }
        if (dc > 0 && Input.GetMouseButtonUp(0))
        {
            EndDraw();
        }

        if (!Drawing)
        {
            if (wheelInput > 0 && lineWidth <= MaxlineWidth)
            {
                lineWidth += 0.1f;
            }
            if (wheelInput < 0 && lineWidth >= MinlineWidth)
            {
                lineWidth -= 0.1f;
            }
        }
    }

    void BeginDraw()
    {
        Drawing = true;
        currentLine = Instantiate(linePrefab).GetComponent<Line>();

        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);
    }

    void Draw()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 5f, Vector2.zero, 0.5f, cantDrawOverLayer[0]);
        Debug.DrawRay(mousePosition, Vector2.down, new Color(1, 0, 0));

        if (hit || currentLine.circleCount > 50)
        {
            EndDraw();
        }
        else
        {
            currentLine.AddPoint(mousePosition);
        }

    }

    void EndDraw()
    {
        if (currentLine != null)
        {
            Drawing = false;
            if (currentLine.pointsCount < 15)
            {
                // If line has one Point
                Destroy(currentLine.gameObject);
            }
            else
            {
                //currentLine.gameObject.layer = canDrawOverLayerIndex;
                currentLine.Gravity(lineWidth);
                currentLine.Mass(lineWidth);
                currentLine.UsePhysics(true);
                currentLine = null;
                lines.Add(currentLine);
                --dc;
            }
        }
    }

    private void currentLine_OnAnyLineDestroy(object sender, EventArgs e)
    {
        Line line = sender as Line;
        
        lines.Remove(line);
        lines.Sort();
        ++dc;
    }

    public int GetLineCount()
    {
        return dc;
    }

    private void OnDisable()
    {
        lines.Clear();
        dc = 5;
        Line.OnAnyLineDestroy -= currentLine_OnAnyLineDestroy;
    }

}
