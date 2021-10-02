using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed = 12f;

    public float MouseSensitivity = 5f;

    private float keyx, keyy, keyz, mouseX, mouseY;

    float camX = 0f;

    Camera cam;
    Rigidbody rgbdy;

    Canvas cnvs;
    Text state;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("HLS").GetComponent<Thrust>().enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        rgbdy = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        cnvs = GetComponentInChildren<Canvas>();
        state = cnvs.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        state.text = transform.eulerAngles.ToString();

        keyx = Input.GetAxis("X");
        keyy = Input.GetAxis("Y");
        keyz = Input.GetAxis("Z");

        mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        camX -= mouseY;
        camX = Mathf.Clamp(camX, -90f, 75f);

        if (Input.GetKey(KeyCode.R))
        {
            rgbdy.constraints = RigidbodyConstraints.None;
            Vector3 rtt = new Vector3(keyz, keyy, keyx) * speed * Time.deltaTime;
            rgbdy.transform.localEulerAngles += rtt;
            rgbdy.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            Vector3 mv = new Vector3(keyx, keyy, keyz) * speed * Time.deltaTime;
            transform.Translate(mv);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            rgbdy.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            rgbdy.constraints = RigidbodyConstraints.None;
        }

        cam.transform.localRotation = Quaternion.Euler(camX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

    }
}