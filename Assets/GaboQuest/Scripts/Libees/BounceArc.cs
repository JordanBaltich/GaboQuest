﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceArc : MonoBehaviour
{

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
    [SerializeField] GameObject LandingTarget;
    public GameObject currentLandingTarget;

    public Vector3[] positions;

    Vector3 p0, p1, p2;

    float g = Physics.gravity.y;
    float radianAngle;
    public int currentPosition = 1;

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

    public void BounceOffTarget(Vector3 hitTarget)
    {
        m_Body.velocity = Vector3.zero;
        m_Body.rotation = Quaternion.Euler(Vector3.zero);
        ArcStartPosition = new Vector3(hitTarget.x, 0, hitTarget.z);

        FindArcPoints();
        DrawQuadraticCurve();

        currentLandingTarget = Instantiate(LandingTarget, p2 + (Vector3.up / 2), Quaternion.identity);

    }

    public void MoveAlongArc()
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
        Vector3 circlePos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

        Vector3 landPos = ArcStartPosition + circlePos;
        landPos.y = FindGroundLevel(landPos);

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

    float FindGroundLevel(Vector3 point)
    {
        float currentGroundLevel;

        RaycastHit hit;

        if ((Physics.Raycast(point + (Physics.gravity * -1), Vector3.down, out hit, 25f, LayerMask.GetMask("Ground"))))
        {
            currentGroundLevel = hit.point.y;
            Debug.DrawRay(point, Vector3.down, Color.red, 1f);
        }
        else
        {
            Debug.DrawRay(point, Vector3.down, Color.red);
            currentGroundLevel = 0;
            p2 = FindLandingPosition();
        }


        return currentGroundLevel;
    }
}
