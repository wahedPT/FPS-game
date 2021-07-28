using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;
    public CharacterController character;
    Vector3 moveinput;
    public Transform cameraTransform;
    public float mouseSenstivity;
    public bool invertX, invertY;
    Vector2 mouseInput;
    public float gravity;
    public float jumpForce;
    bool canJump;
    public Transform groundCheck;
    public LayerMask groundMask;
    bool canDoubleJump;
    public float runSpeed;
    public Animator anim;
    public Transform firePoint;
    public GameObject prefab;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //moveinput.x = Input.GetAxis("Horizontal")*playerSpeed;
        //moveinput.z = Input.GetAxis("Vertical")*playerSpeed;
        Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");
        Vector3 verMove = transform.forward * Input.GetAxis("Vertical");
        moveinput = horiMove + verMove;
        
        if (Input.GetKey(KeyCode.E))
        {
            moveinput = moveinput * runSpeed;
        }
        else
        {
            moveinput *= moveSpeed ;
        }
        moveinput.y += Physics.gravity.y * gravity * Time.deltaTime;
        //jump Code
        canJump = Physics.OverlapSphere(groundCheck.position, 0.25f, groundMask).Length > 0;
        //Debug.Log(canJump);
        if (canJump)
        {
            canDoubleJump=false;
        }
        if (Input.GetKeyDown(KeyCode.Q)&&canJump)
        {
            moveinput.y = jumpForce;
            canDoubleJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && canDoubleJump)
        {
            moveinput.y = jumpForce;
            canDoubleJump = false;
        }

        character.Move(moveinput * Time.deltaTime);
        //camera rotation using mouseinput
        mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))*mouseSenstivity;
        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }
        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        
        
        //camera rotation
        cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.eulerAngles+new Vector3(mouseInput.y,0,0));
        
        
        //Animation
        anim.SetFloat("moveSpeed", moveinput.magnitude);
        anim.SetBool("onGround",canJump);
        
        
        //shoot bullet
        if (Input.GetMouseButtonDown(0))
        {
            //raycast
            RaycastHit hit;
            if(Physics.Raycast(cameraTransform.position,cameraTransform.forward,out hit, 50f))
            {
                firePoint.LookAt(hit.point);
            }
            else
            {
                if (Vector3.Distance(cameraTransform.position, hit.point) > 2f)
                {
                    firePoint.LookAt(cameraTransform.position + (cameraTransform.forward * 30f));
                }
                
            }

            Instantiate(prefab, firePoint.position, firePoint.rotation);
        }
    }
}
