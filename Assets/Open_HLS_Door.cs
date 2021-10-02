using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_HLS_Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && other.tag == "Player")
        {
            transform.parent.localEulerAngles = new Vector3(0f, 0f, 42f);
            transform.localEulerAngles = new Vector3(0f, 0f, -21f);
        }
        else if (!Input.GetKey(KeyCode.E))
        {
            transform.parent.localEulerAngles = new Vector3(0f, 0f, 0f);
            transform.localEulerAngles = new Vector3(0f, 0f, 21f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && other.tag == "Player")
        {
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
