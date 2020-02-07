using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceArc : MonoBehaviour
{
    [SerializeField] int EnemyLayerID, GroundLayerID;

    LineRenderer lr;
    Rigidbody m_Body;

    [Header("Arc")]
    [SerializeField] private float travelSpeed;
    [SerializeField] private float height;
    [SerializeField] private int resolution = 25;

    [Header("Landing")]
    [SerializeField] private float minRadius, maxRadius;
    [SerializeField] private float landingRadius;

    [Range(0, 12)]
    [SerializeField] private int circlePosition;

    Vector3[] positions;

    Vector3 p0, p1, p2;

    float g = Physics.gravity.y;
    float radianAngle;
    int currentPosition = 1;

    Vector3 ArcStartPosition;
  

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();

        g = Mathf.Abs(Physics.gravity.y);

        positions = new Vector3[resolution];
    }

    private void OnValidate()
    {
        if (lr != null && Application.isPlaying)
        {
            FindArcPoints();
            DrawQuadraticCurve();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == EnemyLayerID)
    //    {
    //        m_Body.velocity = Vector3.zero;
    //        m_Body.isKinematic = true;
    //        ArcStartPosition = transform.position;

    //        FindArcPoints();
    //        DrawQuadraticCurve();
           
    //    }

    //    if (other.gameObject.layer == GroundLayerID)
    //    {
    //        m_Body.isKinematic = false;
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == EnemyLayerID)
        {
            m_Body.velocity = Vector3.zero;
            m_Body.isKinematic = true;
            ArcStartPosition = transform.position;

            FindArcPoints();
            DrawQuadraticCurve();

        }

        if (collision.gameObject.layer == GroundLayerID)
        {
            m_Body.isKinematic = false;
        }
    }

    private void FixedUpdate()
    {
        if (m_Body.isKinematic)
        {
            MoveAlongArc();
        }
    }

   

    //** lots of cool pointless math
    ////finds the appropriate velocity to travel along generated arc
    //void LaunchAlongArc()
    //{
    //    float distance = Vector3.Distance(new Vector3(p2.x, 0, p2.z), new Vector3(p0.x, 0, p0.z));
    //    float yOffset = p0.y - p2.y;

    //    float angle = Vector3.Angle(p0, p1) * Mathf.Deg2Rad;
    //    float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * g * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

    //    Vector3 Velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

    //    float angleBetweenObjects = Vector3.Angle(Vector3.forward, new Vector3(p2.x, 0, p2.z) - new Vector3(p0.x, 0, p0.z)) * (p2.x > transform.position.x ? 1 : -1);
    //    Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * Velocity;

    //    m_Body.velocity = finalVelocity;
    //}

    void MoveAlongArc()
    {
        m_Body.position = Vector3.MoveTowards(m_Body.position, positions[currentPosition], travelSpeed * g / m_Body.position.y  * Time.deltaTime);
        m_Body.rotation = Quaternion.LookRotation(positions[currentPosition] - transform.position, Vector3.up);

        if (m_Body.position == positions[currentPosition])
        {
            if (currentPosition < positions.Length - 1)
            {
                currentPosition++;
            }
            
        }
    }

    private void DrawQuadraticCurve()
    {
       
        for (int i = 1; i < resolution + 1; i++)
        {
            float t = i / (float)resolution;
            positions[i -1] = CalculateQuadraticBesierPoint(t, p0, p1, p2);
        }
        lr.positionCount = resolution;
        lr.SetPositions(positions);
    }

    Vector3 FindLandingPosition()
    {     
        float angle = (float)Random.Range((int)0, (int)12) * Mathf.PI * 2 / 12;
        float radius = Random.Range(minRadius, maxRadius);
        Vector3 landPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

        print(radius);
        
        return landPos;
    }

    void FindArcPoints()
    {
        p0 = transform.position;
        p2 = FindLandingPosition();
        p1 = p0 + (p2 - p0) / 2;
        float distance = Mathf.Abs(Vector3.Distance(p0, p2));
        p1.y = height - distance;
    }

    private Vector3 CalculateQuadraticBesierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }
}
