using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;

    [SerializeField]
    private Transform character;
    [SerializeField]
    private Transform cameraArm;

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rigid;

    public LayerMask groudMask;
    [SerializeField] private bool isGrounded = false;

    [SerializeField] private Transform weaponPosition;
    [SerializeField] public bool isMouse = false;
    [SerializeField] private NavMeshAgent meshAgent;
    private Vector3 destination;

    // Start is called before the first frame update
    private void Awake()
    {
        animator = character.GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        isMouse = false;
    }

    [System.Obsolete]
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("d");
            rigid.AddForce(Vector3.up * 250f);
            isGrounded = false;
            animator.SetBool("isGrounded", false);
            animator.SetTrigger("Jump");
        }

        if(Input.GetMouseButton(0) && !isMouse)
        {
            float attackAni = (float)Random.Range(0,2)%10f;
            animator.SetFloat("AttackAni", attackAni);
            animator.SetTrigger("Attack");
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isMouse = !isMouse;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isMouse) Camera();
        else;
        Move();

        if(isGrounded)
        {
            animator.SetBool("isGrounded", true);    
        }

        isGrounded = Physics.CheckSphere(transform.position, 0.2f, groudMask);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Weapon"))
        {
            other.transform.SetParent(weaponPosition);
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;
            animator.SetLayerWeight(1, 1f);
        }
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0;

        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            animator.SetFloat("Speed", speed);

            character.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * 5f;
        }
        else animator.SetFloat("Speed", 0f);
    }

    private void Camera()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        float x = camAngle.x - mouseDelta.y;

        if(x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    public void SetDestination(Vector3 position)
    {
        destination = position;
        if(isMouse) meshAgent.SetDestination(destination);
        float speed = meshAgent.desiredVelocity.magnitude;
        animator.SetFloat("Speed", speed);
    }
}
