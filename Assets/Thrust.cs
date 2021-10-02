using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Thrust : MonoBehaviour
{
    public float power = 10f;

    public uint fuel = 100000;

    private float Damage;

    private float ground = 2200;

    Canvas HLS_Cnvs;
    Text[] texts;
    Rigidbody rgbdy, plyr;

    GameObject Btn_Obj;
    Button Btn;
    RectTransform Btn_Obj_Rect;

    Image Btn_Img;

    ParticleSystem[] Flames;

    private bool thrust_ready;


    // Start is called before the first frame update
    void Start()
    {
        thrust_ready = false;

        rgbdy = GetComponent<Rigidbody>();
        plyr = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();

        rgbdy.GetComponent<Eject_Player>().enabled = false;

        Flames = GetComponentsInChildren<ParticleSystem>();

        HLS_Cnvs = GetComponentInChildren<Canvas>();
        texts = HLS_Cnvs.GetComponentsInChildren<Text>();


        Btn_Obj = new GameObject();
        Btn_Obj.transform.parent = HLS_Cnvs.transform;
        Btn_Obj.AddComponent<Button>();
        Btn_Obj.AddComponent<Image>();

        Btn = Btn_Obj.GetComponent<Button>();
        Btn_Obj_Rect = Btn.GetComponent<RectTransform>();
        Btn_Obj_Rect.localPosition = new Vector3(0, 0, 0);
        Btn_Obj_Rect.sizeDelta = new Vector2(320, 60);

        Btn_Img = Btn_Obj.GetComponent<Image>();
        Btn_Img.sprite = Resources.Load<Sprite>("start");

        Cursor.lockState = CursorLockMode.None;

        Btn.onClick.AddListener(agreed);
    }

    void agreed()
    {
        //rgbdy.transform.Translate(new Vector3(0f, -1000f, 0f));

        rgbdy.isKinematic = false;
        rgbdy.transform.Translate(new Vector3(0f, 0f, -50f));
        rgbdy.useGravity = true;
        rgbdy.constraints = RigidbodyConstraints.None;
        rgbdy.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        Destroy(Btn_Obj);
        Cursor.lockState = CursorLockMode.Locked;
        thrust_ready = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Rigidbody>().velocity.magnitude > 10)
        {
            Damage = rgbdy.velocity.magnitude;
        }
    }

    private void p_flame()
    {
        foreach (ParticleSystem flame in Flames)
        {
            flame.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        texts[0].text = (transform.position.y - ground).ToString() + "m above";
        texts[1].text = "velocity : " + rgbdy.velocity.magnitude.ToString();
        texts[2].text = "fuel: " + fuel.ToString();
        texts[3].text = "Damage: " + Damage.ToString();

        if (Damage > 100)
        {
            texts[4].text = "HLS fully Damaged";
            Application.Quit();
            Destroy(gameObject);
        }


        if (fuel > 0 && thrust_ready)
        {

            if (Input.GetKey(KeyCode.T))
            {
                rgbdy.AddForce(Vector3.up * power, ForceMode.Acceleration);
                fuel--;
                p_flame();
            }
            else if (Input.GetKey(KeyCode.W))
            {
                rgbdy.AddForce(Vector3.forward * power, ForceMode.Acceleration);
                fuel--;
                p_flame();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                rgbdy.AddForce(Vector3.back * power, ForceMode.Acceleration);
                fuel--;
                p_flame();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                rgbdy.AddForce(Vector3.left * power, ForceMode.Acceleration);
                fuel--;
                p_flame();
            }
            else if (Input.GetKey(KeyCode.D))
            {
                rgbdy.AddForce(Vector3.right * power, ForceMode.Acceleration);
                fuel--;
                p_flame();
            }
            if ((Input.GetKeyUp(KeyCode.T)) || (Input.GetKeyUp(KeyCode.W)) || (Input.GetKeyUp(KeyCode.S)) || (Input.GetKeyUp(KeyCode.A)) || (Input.GetKeyUp(KeyCode.D)))
            {
                foreach (ParticleSystem flame in Flames)
                {
                    flame.Stop();
                }
            }

            else if (Input.GetKey(KeyCode.E))
            {
                //deactivate HLS
                texts[0].text = "";
                texts[1].text = "";
                texts[2].text = "";
                texts[3].text = "";
                texts[4].text = "";

                foreach (ParticleSystem flame in Flames)
                {
                    flame.Stop();
                }

                rgbdy.GetComponent<Eject_Player>().enabled = true;
            }
        }
        else if (fuel < 0)
        {
            Debug.Log("no fuel");
        }

    }
}
