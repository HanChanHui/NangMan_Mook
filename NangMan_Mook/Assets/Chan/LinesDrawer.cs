using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Camera cam;

    void Start()
    {
        cam = Camera.main;
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
        if (Input.GetMouseButtonDown(0))
        {
            BeginDraw();
        }
        if (currentLine != null)
        {
            Draw();
        }
        if (Input.GetMouseButtonUp(0))
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
            }
        }
    }

}
