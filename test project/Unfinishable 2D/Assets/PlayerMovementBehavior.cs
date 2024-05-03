using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        Debug.Log("Horizontal Input: " + horizontalInput);
        Debug.Log("Vertical Input: " + verticalInput);
    }
}
