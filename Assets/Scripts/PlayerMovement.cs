using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float speed = 12f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundDistance;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Vector3 velocity;
    [SerializeField] bool isGrounded;

    [SerializeField] GameObject nearObjectCheck;
    [SerializeField] float itemDistance;
    [SerializeField] LayerMask itemMask;
    [SerializeField] Collider[] nearItem;
    [SerializeField] Camera camera;

    [SerializeField] GameObject equippedItem;
    GameObject heldItem;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        nearItem = Physics.OverlapSphere(nearObjectCheck.transform.position, itemDistance, itemMask);

        if(nearItem.Length > 0 && equippedItem.transform.childCount == 0)
        {
            if(Input.GetButtonDown("Interact"))
            {
                foreach(var item in nearItem)
                {
                    //TODO: Hey you should only pick up an item if you're looking at it AND near it. idiot.
                    // var ray = camera.ScreenPointToRay(Input.mousePosition);
                    heldItem = Instantiate(item.transform.gameObject, new Vector3(equippedItem.transform.position.x, equippedItem.transform.position.y, equippedItem.transform.position.z), Quaternion.identity);
                    equippedItem.transform.Rotate(0f,0f,0f);
                    heldItem.transform.parent = equippedItem.transform;
                    heldItem.transform.localPosition = new Vector3(0.9f, -0.6f, 0f);
                    heldItem.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    Destroy(heldItem.GetComponent<MeshCollider>());
                }
            }
        }

        if(heldItem != null) 
        {
            var animation = heldItem.GetComponent<Animation>();

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                animation.Play();
            }

            if(Input.GetKeyDown(KeyCode.Q))
            {
                GameObject dropObj = heldItem;
                
            }
        }
        
        /**
        *  Crouch Controls
        */
        if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            gameObject.transform.localScale = new Vector3(1f, 0.5f ,1f);
        }

        /**
        *  Crouch Controls
        */
        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);

        }

        /**
        * Sprint controls
        */
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            speed *= 1.5f;
        } 
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            speed = 12f;
        } 


        if(isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

}
