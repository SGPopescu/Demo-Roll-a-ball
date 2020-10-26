using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_control : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 1.0f;
    private int score;
    private float moveHorizontal, moveVertical;
    public Text scoreText,timeText,endText;
    public GameObject prefab;
    private GameObject spawn = null;
    public float timeRemaining; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        endText.text = "";
        UpdateScore();
        UpdateTime();

    }

    // Update is called once per frame
    private void Update()
    {
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            Destroy(spawn);
            endText.text = "GAME ENDED" + "\n Your score: " + score;
            scoreText.text = timeText.text = "";
        }
    }

    private void FixedUpdate()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        if (Input.GetMouseButton(0))
        {
            moveHorizontal = Input.GetAxis("Mouse X");
            moveVertical = Input.GetAxis("Mouse Y");
        }


        if (Input.GetKey("space"))
            rb.velocity *= 0.9f;
        rb.AddForce(Vector3.ClampMagnitude(new Vector3(moveHorizontal, 0, moveVertical), 1) * speed);

        timeRemaining -= Time.fixedDeltaTime;
        UpdateTime();

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            score++;
            other.gameObject.SetActive(false);
            UpdateScore();

        }
    }
    
    void UpdateScore()
    { 
        
        scoreText.text = "Your Score " + score.ToString();
        if(spawn==null)
            spawn =GameObject.Instantiate<GameObject>(prefab, GetSpawnPosition(),Quaternion.identity);
        else
        {
            spawn.transform.position = GetSpawnPosition();
            spawn.SetActive(true);
        }
     
       
    }

    void UpdateTime()
    {
        timeText.text = "Time:  " + timeRemaining.ToString("0.00");
    }

    public Vector3 GetSpawnPosition()
    {
        float x, z;
        x = GetPossibleSpwanParam();
        z = GetPossibleSpwanParam();
        while (Vector3.Distance(transform.position,new Vector3(x,1,z))<2.0f)
        {
            x = GetPossibleSpwanParam();
            z = GetPossibleSpwanParam();   
        }

        return new Vector3(x, 1, z);
    }

    public float GetPossibleSpwanParam()
    {
        return UnityEngine.Random.Range(-9.0f, 9.0f);
    }
}