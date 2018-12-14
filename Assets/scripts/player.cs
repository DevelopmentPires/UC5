using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    private int facingRight = -1;
    private bool playerOnGround = true;
    private float axis;
    private float jumpForce = 250;
    private bool jump = false;
    private float maxVelocity = 10;
    private Vector2 velocity;
    private Transform groundCheck;
    private bool specialWeapon = false;

    public Rigidbody2D rb;
    public Animator animin;
    public Collider collider;
    public GameObject prefabShot;
    public GameObject prefabShotSpecial;
    public GameObject shootPosition;
    
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animin = GetComponent<Animator>();
        collider = GetComponent<Collider>();

        groundCheck = gameObject.transform.Find("groundCheck");

        GameObject.Find("normalWeapon").GetComponent<Image>().enabled = true;
        GameObject.Find("specialWeapon").GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        playerOnGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("ground"));

        if (Input.GetKeyDown(KeyCode.UpArrow) && playerOnGround)
        {
            jump = true;
            animin.SetTrigger("jump");
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
            specialWeapon = !specialWeapon;

        if (specialWeapon)
        {
            GameObject.Find("weaponText").GetComponent<Text>().text = "Special Weapon";
            GameObject.Find("normalWeapon").GetComponent<Image>().enabled = false;
            GameObject.Find("specialWeapon").GetComponent<Image>().enabled = true;

        }
        else if (!specialWeapon)
        {
            GameObject.Find("weaponText").GetComponent<Text>().text = "Normal Weapon";
            GameObject.Find("normalWeapon").GetComponent<Image>().enabled = true;
            GameObject.Find("specialWeapon").GetComponent<Image>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {

            if (!specialWeapon)
            {
                GameObject newShot = Instantiate(prefabShot, shootPosition.transform.position, Quaternion.identity);
                newShot.GetComponent<Rigidbody2D>().velocity = transform.right * 15 * facingRight * (-1);

               
             //   if (newShot.GetComponent<BoxCollider>().gameObject.tag == "enemy")
               


            }
            else if (specialWeapon)
            {
                GameObject newShot = Instantiate(prefabShotSpecial, shootPosition.transform.position, Quaternion.identity);
                newShot.GetComponent<Rigidbody2D>().velocity = transform.right * 30 * facingRight * (-1);

             //   if (newShot.GetComponent<Collider>().tag == "enemy")
               
            }
            
        }
               

    }

    private void FixedUpdate()
    {

        axis = Input.GetAxis("Horizontal");
        animin.SetFloat("run", Mathf.Abs(axis));

        if (axis > 0 && facingRight == 1)
            Flip();
        else if (axis < 0 && facingRight == -1)
            Flip();

        velocity = new Vector2(axis * maxVelocity, rb.velocity.y);
        rb.velocity = velocity;


        if (jump)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            jump = false;
        }


    }

    void Flip()
    {
        facingRight = facingRight * (-1);
        Vector2 newScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        transform.localScale = newScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "enemy")
        {

            Destroy(gameObject);
        }
    }

}
