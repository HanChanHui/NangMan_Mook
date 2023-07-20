using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D rigidBody;

    [HideInInspector] public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;

    float pointsMinDistance = 0.1f;
    float circleColliderRadius;
    public float circleCount = 0f;




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

        // Add Circle Collider to the Point
        CircleCollider2D circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newPoint;
        circleCollider.radius = circleColliderRadius;
        circleCount++;
        //Debug.Log(circleCount);

        // Line Renderer
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);

        // Edge Collider
        if (pointsCount > 1)
        {
            edgeCollider.points = points.ToArray();
        }

    }

    public Vector2 GetLastPoint()
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

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        if(width > 0.5)
        {
            circleColliderRadius = width / 3f;
            SetPointsMinDistance(0.6f);
        }
        else if(width > 0.2)
        {
            circleColliderRadius = width / 2.5f;
            SetPointsMinDistance(0.4f);
        }
        else
        {
            circleColliderRadius = width / 2f;
            SetPointsMinDistance(0.3f);
        }


        edgeCollider.edgeRadius = circleColliderRadius;
    }

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

    // ªË¡¶
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(DestroyTime);
        Destroy(this.gameObject);
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "DeathZone")
        {
            Destroy(this.gameObject);
        }
    }
}
