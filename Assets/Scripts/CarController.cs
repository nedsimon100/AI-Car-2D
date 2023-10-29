using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NNet))]
public class CarController : MonoBehaviour
{
    public float rotateSpeed;
    public float AccelMult;
    public Rigidbody2D rb;
    NNet net;
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    public float speed;
    public float topSpeed;
    public float MinSpeed;
    public bool dead;

    // fitness values
    [Header("fitness")]
    public float highspeed;
    public float avespeed;
    public float timeAlive;
    public float distTraveled;
    public float fitness;
    //--------------------------------------------------------------------------------------------------------------------
    //--------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        StartCoroutine("notMoving");
        dead = false;
    }
    //--------------------------------------------------------------------------------------------------------------------

    private void FixedUpdate()
    {
        if(dead == true)
        {
            kill();
        }
        getInputs();
        timeAlive += 1;
    }
    //--------------------------------------------------------------------------------------------------------------------

    public void getInputs()
    {
        Vector2 dirA = (-transform.right);
        Vector2 dirB = (-transform.right + transform.up);
        Vector2 dirC = (transform.up);
        Vector2 dirD = (transform.right + transform.up);
        Vector2 dirE = (transform.right);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, dirA);
        this.GetComponent<NNet>().inputLayer[0] = 1;
        if (hit.distance < 20)
        {
            this.GetComponent<NNet>().inputLayer[0] = hit.distance / 20;
        }
        hit = Physics2D.Raycast(transform.position, dirB);
        this.GetComponent<NNet>().inputLayer[1] = 1;
        if (hit.distance < 20)
        {
            this.GetComponent<NNet>().inputLayer[1] = hit.distance / 20;
        }
        hit = Physics2D.Raycast(transform.position, dirC);
        this.GetComponent<NNet>().inputLayer[2] = 1;
        if (hit.distance < 20)
        {
            this.GetComponent<NNet>().inputLayer[2] = hit.distance / 20;
        }
        hit = Physics2D.Raycast(transform.position, dirD);
        this.GetComponent<NNet>().inputLayer[3] = 1;
        if (hit.distance < 20)
        {
            this.GetComponent<NNet>().inputLayer[3] = hit.distance / 20;
        }
        hit = Physics2D.Raycast(transform.position, dirE);
        this.GetComponent<NNet>().inputLayer[4] = 1;
        if (hit.distance < 20)
        {
            this.GetComponent<NNet>().inputLayer[4] = hit.distance / 20;
        }
        this.GetComponent<NNet>().runNetwork();
    }
    //--------------------------------------------------------------------------------------------------------------------

    public void useOutputs(float acceleration, float rotation)
    {
        
        speed += acceleration * AccelMult *Time.deltaTime;
        speed = Mathf.Clamp(speed, MinSpeed, topSpeed);
        transform.position += transform.up * speed * Time.deltaTime;
        if (rotation < 0.2f && rotation > -0.2f)
        {
            distTraveled += (transform.up * speed * Time.deltaTime).magnitude;
        }
        transform.Rotate(new Vector3(0, 0, (rotation * rotateSpeed)));
       
        if (speed > highspeed)
        {
            highspeed = speed;
        }
    }
    //--------------------------------------------------------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        kill();
    }
    //--------------------------------------------------------------------------------------------------------------------
    IEnumerator notMoving()
    {
        while(dead == false)
        {
            yield return new WaitForSeconds(10);
            if ((transform.position - initialPosition).magnitude < 1)
            {
           //     kill();
            }
        }
    }
    //--------------------------------------------------------------------------------------------------------------------

    public void kill()
    {

        fitness = highspeed * 3 + distTraveled * 100 + highspeed * distTraveled * 10;
        Debug.Log("Fitness of last agent: "+fitness);
        speed = 0;
        transform.position = initialPosition;
        transform.rotation = initialRotation;
        rb.velocity = transform.up*0;
        dead = true;
    }
}
