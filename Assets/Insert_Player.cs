using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insert_Player : MonoBehaviour
{
    GameObject HLS;
    // Start is called before the first frame update
    void Start()
    {
        HLS = transform.parent.parent.gameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && other.tag == "Player")
        {
            //deactivate Player
            other.GetComponent<Movement>().enabled = false;
            other.attachedRigidbody.velocity = Vector3.zero;
            other.attachedRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            other.attachedRigidbody.isKinematic = true;
            other.GetComponentInChildren<Camera>().enabled = false;
            other.GetComponentInChildren<Canvas>().enabled = false;
            other.transform.parent = HLS.transform;
            other.transform.localPosition = new Vector3(0f, 280f, 0f);

            //activate Thrust
            HLS.GetComponent<Thrust>().enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
