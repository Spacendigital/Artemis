using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eject_Player : MonoBehaviour
{
    Rigidbody plyr;
    // Start is called before the first frame update
    void Start()
    {

        plyr = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

        GetComponent<Thrust>().enabled = false;

        //activate Player
        plyr.velocity = Vector3.zero;
        plyr.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        plyr.isKinematic = false;
        plyr.useGravity = true;
        plyr.GetComponentInChildren<Camera>().enabled = true;
        plyr.GetComponentInChildren<Canvas>().enabled = true;
        plyr.transform.parent = null;
        plyr.transform.localPosition = transform.position + (new Vector3(2f, 2f, 2f));

        plyr.GetComponent<Movement>().enabled = true;
    }
}
