using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject libee;

    Rigidbody m_Body;
    Animator m_StateMachine;

    PlayerMotor m_Motor;
    Health m_Health;
    Shoot m_Shoot;

    Vector3 direction;


    [SerializeField] private int hazardLayerID, healthID;

    public float rotationSpeed;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody>();
        m_StateMachine = GetComponent<Animator>();

        m_Shoot = GetComponent<Shoot>();
        m_Motor = GetComponent<PlayerMotor>();
        m_Health = GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            m_StateMachine.SetBool("isShooting" ,true);

        }
        if (Input.GetButtonUp("Jump"))
        {
            m_StateMachine.SetBool("isShooting", false);

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
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

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
