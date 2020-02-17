using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    public Player player;

    public GameObject libee;
    public SortSelectLibee m_LibeeSorter;

    Rigidbody m_Body;
    Animator m_StateMachine;
    GrowShrinkMechanic m_GrowMechanic;
    PlayerTongue m_Tongue;
    PlayerMotor m_Motor;
    Health m_Health;
    Shoot m_Shoot;

    Vector3 direction;
    public Vector3 lastHeldDirection;

    [SerializeField] private int hazardLayerID, healthID, libeeLayerID;

    public float rotationSpeed;

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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == healthID)
        {
            m_Health.Heal(1);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!m_StateMachine.GetBool("isShooting"))
        {
            if (collision.gameObject.layer == libeeLayerID)
            {
                collision.gameObject.transform.parent = null;
                m_GrowMechanic.currentScale = transform.localScale;
               
                Rigidbody libeeBody = collision.gameObject.GetComponent<Rigidbody>();
                collision.gameObject.transform.position = m_LibeeSorter.CapturedLibees.position;
                collision.gameObject.transform.parent = m_LibeeSorter.CapturedLibees;

                libeeBody.useGravity = false;
                libeeBody.velocity = Vector3.zero;
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

    public bool GroundCheck()
    {
        RaycastHit hit;

        if ((Physics.Raycast(transform.position, Vector3.down, out hit, GetComponent<CapsuleCollider>().height / 2, LayerMask.GetMask("Ground"))))
        {
            return true;
        }
        else return false;
    }
}
