using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    public Player player;

    public GameObject libee;
    public SortSelectLibee m_LibeeSorter;

    public Transform AimPoint;

    Rigidbody m_Body;
    Animator m_StateMachine;
    GrowShrinkMechanic m_GrowMechanic;
    PlayerTongue m_Tongue;
    PlayerMotor m_Motor;
    Health m_Health;
    Shoot m_Shoot;

    Vector3 direction;

    internal bool CanMove = true;

    [SerializeField] private int hazardLayerID, healthID, libeeLayerID, enemyLayerID;

    public float rotationSpeed;

    [SerializeField] float slopeForce;
    [SerializeField] float slopeRayLength;

    public Vector3 lastHitDirection;
    public float knockbackForce;

    public int weight;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
        m_StateMachine = GetComponent<Animator>();

        m_Shoot = GetComponent<Shoot>();
        m_Motor = GetComponent<PlayerMotor>();
        m_Health = GetComponent<Health>();
        m_GrowMechanic = GetComponent<GrowShrinkMechanic>();
        m_Tongue = GetComponentInChildren<PlayerTongue>();

        player = ReInput.players.GetPlayer(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Tongue.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetButton("Aim"))
        {
            m_StateMachine.SetBool("isShooting" ,true);

            StartCoroutine(m_GrowMechanic.Shrink(m_LibeeSorter.Normal.Count));
        }
        if (player.GetButtonUp("Aim")) 
        {
            m_StateMachine.SetBool("isShooting", false);
        }

        if (!m_StateMachine.GetBool("isShooting"))
        {
            if (player.GetButtonDown("Tongue"))
            {
                m_Tongue.gameObject.SetActive(true);
                m_StateMachine.SetBool("isTongueOut", true);
            }
        }
        
    }

    private void FixedUpdate()
    {
        m_Motor.MaxSpeedValue((float)m_LibeeSorter.Normal.Count);

       
    }

    public void MovePlayer()
    {
        //convert direction from Vector2 to Vector3
        Vector3 moveDirection = new Vector3(Direction().x, 0, Direction().y);

        //accelerate when input direction is past given threshold, rotate towards input direction
        if (Direction().sqrMagnitude > 0.15f || Direction().sqrMagnitude < -0.15f)
        {

            m_Body.velocity = new Vector3(moveDirection.x * m_Motor.Accelerate(), m_Body.velocity.y, moveDirection.z * m_Motor.Accelerate());

            m_Body.rotation = Quaternion.LookRotation((moveDirection * rotationSpeed * Time.deltaTime));


            if (OnSlope())
            {
                m_Body.AddForce(Vector3.down * (Mathf.Abs(Physics.gravity.y * slopeForce)));
            }

        }
        else
        {
            // decelerate when no input is given
            m_Motor.Decelerate();
            if (m_Body.velocity.sqrMagnitude > 0.15f || m_Body.velocity.sqrMagnitude < -0.15f)
            {
                m_Body.velocity = new Vector3(moveDirection.x * m_Motor.Decelerate(), m_Body.velocity.y, moveDirection.z * m_Motor.Decelerate());


            }
        }

        m_StateMachine.SetFloat("Input", moveDirection.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == healthID)
        {
            //m_Health.Heal(1);
            //Destroy(other.gameObject);
        }

        if (other.gameObject.layer == enemyLayerID)
        {
            if (other.gameObject.tag == "HitBox")
            {
                if (m_Health.currentHealth > 0)
                {
                    m_Health.TakeDamage(1);
                    lastHitDirection = GetHitDirection(other.ClosestPointOnBounds(m_Body.position));
                    m_StateMachine.SetBool("isHit", true);
                   
                }
                else
                {
                    m_StateMachine.SetBool("isDead", true);
                }
            }         
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!m_StateMachine.GetBool("isShooting"))
        {
            if (collision.gameObject.layer == libeeLayerID)
            {
                collision.gameObject.GetComponent<LibeeController>().ResetTriggers();
                collision.gameObject.GetComponent<Animator>().SetTrigger("isPickedUp");              

                m_GrowMechanic.currentScale = transform.localScale;
               
                collision.gameObject.transform.position = m_LibeeSorter.CapturedLibees.position;
                collision.gameObject.transform.SetParent(m_LibeeSorter.CapturedLibees);
                m_LibeeSorter.SortLibee();

                StartCoroutine(m_GrowMechanic.Grow(m_LibeeSorter.Normal.Count));
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == hazardLayerID)
        {
            m_Health.TakeDamage(1);
            m_Body.AddForce(Vector3.up * 3, ForceMode.Impulse);
        }
    }

    public Vector2 Direction()
    {
        float hor = player.GetAxis("L_Horizontal");
        float vert = player.GetAxis("L_Vertical");

        Vector2 direction = new Vector2(hor, vert);

        if (direction.magnitude > 1)
        {
            direction = direction / direction.magnitude;
        }

        return direction;
    }

    public bool OnSlope()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, GetComponent<CapsuleCollider>().height / 2 * slopeRayLength, LayerMask.GetMask("Ground")))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }
        }

        return false;
    }


    public Vector3 GetHitDirection(Vector3 enemyPos)
    {
        Vector3 direction = enemyPos - transform.position;

        direction.y = transform.position.y;

        return direction.normalized;
    }
}
