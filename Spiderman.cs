
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spiderman : MonoBehaviour
{
    //this is the location of the pivot point
    private Vector3 targetPos = new Vector3(0, 5, 0);

    //rope length
    private float lengthP;

    // Start is called before the first frame update
    void Start()
    {
        //set initial position of the target
        LineRenderer web = GetComponent<LineRenderer>();
        //the second position point in the web line is the same as Spiderman position
        web.SetPosition(0, transform.position);
        web.SetPosition(1, targetPos);

        //calculate the length of the rope
        Vector3 ropeVector = new Vector3(transform.position.x - targetPos.x, transform.position.y - targetPos.y, 0);
        lengthP = ropeVector.magnitude;
    }


    //component of a vector
    private static float component(Vector3 a, Vector3 b)
    {
        float proj;
        float aMag = a.magnitude;
        float bMag = b.magnitude;

        //Only perform the calculation if both vectors are valid
        if (aMag == 0.0f || bMag == 0.0f)
            proj = 0.0f;
        else
            proj = Vector3.Dot(a, b) / aMag;

        return proj;
    }
    //this is the length of the web


    //mass
    private const float mass = 1.0f;

    //gravity
    private const float g = -9.8f;

    //acceleration
    private Vector3 accel = new Vector3(0, 0, 0);

    //velocity
    private Vector3 velocity = new Vector3(0, 0, 0);

    //force
    private Vector3 force = new Vector3(0, 0, 0);

    void Update()
    {
        calculateForce();
        calculateAccel();
        calculateVelocity();
        calculatePosition();
    }

    private void calculateForce()
    {

        //To be implemented by you (great)
        //This section is for variables
        Vector3 displ = new Vector3(0, 0, 0);


        //End section

        //This is for calculations
        displ = transform.position - targetPos;
        Vector3 gravity = calculateGravity();
        Vector3 tension;
       
        if (calculateLength(displ) >= lengthP)
        {
            float angle = 0;
            tension = Vector3.Normalize(displ) * (mass * g * Mathf.Cos(angle)) + (mass * Mathf.Pow(calculateLength(velocity), 2)) / lengthP;  
        }

        else
        {
            tension = new Vector3(0, 0, 0);
        }

    }

    private float calculateLength(Vector3 vec)
    {
        float length;

        length = Mathf.Sqrt((Mathf.Pow(vec.x, 2) + Mathf.Pow(vec.y, 2)));

        return length;
    }
    private void calculateAccel()
    {
        accel = force * (1.0f / mass);
    }
    private void calculateVelocity()
    {
        velocity += (accel * Time.deltaTime);

    }

    private Vector3 calculateGravity() 
    {
	    return new Vector3(0, mass * g, 0);    
    }

    private void calculatePosition()
    {
        //These 2 lines we were supposed to uncomment lol!
        Vector3 pos = transform.position + velocity * Time.deltaTime; 
        transform.position = pos;


        //We are supposed to remove this lol!!!!!!!
       /* 
        float t = Time.time;

        //update Spiderman' position
        float x = 0.5f * lengthP * Mathf.Cos(t);
        float y = 0.5f * lengthP * Mathf.Sin(t);
        Debug.Log("y=" + y);
        if (y > 0) y = -y;
        transform.position = new Vector3(x, y, 0);
        
        */
        //update Spiderman's rotation
        float angle = -Mathf.Rad2Deg * Mathf.Atan((transform.position.x - targetPos.x) / (transform.position.y - targetPos.y));
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        
        //update rope
        LineRenderer web = GetComponent<LineRenderer>();
        //the second position point in the web line is the same as Spiderman position
        web.SetPosition(0, transform.position);
        
    }

}
