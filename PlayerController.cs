using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;

    public float speed;

    private float horizontalInput;

    private float forwardInput;

    public float jumpForce = 5.0f;

    public Text heartsLeftCount;

    private int scoreCount, heartsCount;

    public Text scoreCountText;

    public Text winText, loseText;

    bool gameOver = false;

    public void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        heartsCount = 5;
        scoreCount = 0;
        SetScoreCountText();
        SetHeartsCounter();
        winText.text = "";
        loseText.text = "";
    }

    //Called Once per frame
    void Update() 
    {
        //get player input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //move player forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        //let player move forward
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            scoreCount += 1;
            SetScoreCountText();
        } 
        else if (other.gameObject.CompareTag("Enemy"))
        {
            heartsCount -= 1;
            SetHeartsCounter();
        }
    }

    void SetScoreCountText()
    {
        scoreCountText.text = "Count: " + scoreCount.ToString();
        if (scoreCount >= 6) 
        {
            winText.text = "You Win!";
        }
    }

    void SetHeartsCounter()
    {
        heartsLeftCount.text = "Hearts Remaining: " + heartsCount.ToString();

        if (heartsCount == 0) 
        {
            loseText.text = "You Lose! Game Over!";
        }
    }

}
