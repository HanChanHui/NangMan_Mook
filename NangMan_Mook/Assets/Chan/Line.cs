using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public static event EventHandler OnAnyLineDestroy;

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public PolygonCollider2D polygonCollider;
    public Rigidbody2D rigidBody;

    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;

    float pointsMinDistance = 50f;
    float rectangleWidth = 0.15f; // Width of the rectangles representing the line

    [SerializeField] private float DestroyTime;

    private void Start()
    {
        StartCoroutine("Destroy");
    }

    public void AddPoint(Vector2 newPoint)
    {
        if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
        {
            return;
        }

        points.Add(newPoint);
        pointsCount++;
        //circleCount++;

        // Line Renderer
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);

        // Update Edge Collider
        if (pointsCount > 1)
        {
            UpdateEdgeCollider();
            UpdatePolygonCollider();
        }
    }

    void UpdateEdgeCollider()
    {
        Vector2[] linePoints = points.ToArray();
        edgeCollider.points = linePoints;
    }

    void UpdatePolygonCollider()
    {
        // Clear previous points in the Polygon Collider
        polygonCollider.pathCount = 0;

        // Convert List<Vector2> to Vector2[]
        Vector2[] linePoints = points.ToArray();

        Vector2[] rectanglePoints = new Vector2[4];
        // Create a path for each rectangle between two consecutive points
        for (int i = 1; i < pointsCount; i++)
        {
            Vector2 point1 = linePoints[i - 1];
            Vector2 point2 = linePoints[i];

            Vector2 perpendicular = new Vector2(point2.y - point1.y, point1.x - point2.x).normalized;
            Vector2 offset = perpendicular * (rectangleWidth / 2f);

            rectanglePoints[0] = point1 - offset;
            rectanglePoints[1] = point1 + offset;
            rectanglePoints[2] = point2 + offset;
            rectanglePoints[3] = point2 - offset;

            polygonCollider.pathCount++;
            polygonCollider.SetPath(polygonCollider.pathCount - 1, rectanglePoints);
        }

    }

    Vector2 GetLastPoint()
    {
        return (Vector2)lineRenderer.GetPosition(pointsCount - 1);
    }

    public void UsePhysics(bool usePhysics)
    {
        rigidBody.isKinematic = !usePhysics;
    }

    public void SetLineColor(Gradient colorGradient)
    {
        lineRenderer.colorGradient = colorGradient;
    }

    public void SetPointsMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }

    public void SetRectangleWidth(float width)
    {
        rectangleWidth = width;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        //edgeCollider.edgeRadius = width / 2f;
    }

    /*
    public void Gravity(float width)
    {
        if (circleCount > 50)
        {
            rigidBody.gravityScale = (width * 3) + 2.5f;
        }
        else if (circleCount > 25)
        {
            rigidBody.gravityScale = (width * 2) + 2f;
        }
        else if (circleCount > 15)
        {
            rigidBody.gravityScale = (width * 1) + 1.5f;
        }
        else if (circleCount > 5 && width > 0.5f)
        {
            rigidBody.gravityScale = (width * 0.5f) + 1f;
        }
    }

    public void Mass(float width)
    {
        if (width <= 0.1f)
        {
            rigidBody.mass = 1f;
        }
        else if (width <= 0.2f)
        {
            rigidBody.mass = 2f;
        }
        else if (width <= 0.3f)
        {
            rigidBody.mass = 4f;
        }
        else if (width <= 0.4f)
        {
            rigidBody.mass = 8f;
        }
        else if (width <= 0.5f)
        {
            rigidBody.mass = 16f;
        }
        else if (width <= 0.6f)
        {
            rigidBody.mass = 32f;
        }
        else if (width >= 0.7f)
        {
            rigidBody.mass = 64f;
        }
    }
    */

    // ªË¡¶
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(DestroyTime);
        OnAnyLineDestroy?.Invoke(this, EventArgs.Empty);
        Destroy(this.gameObject);
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            Destroy(this.gameObject);
        }
    }
}
