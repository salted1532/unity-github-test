using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargePoint : MonoBehaviour
{
    public Transform myposition;

    ChargeAi chargeai;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        chargeai = GameObject.Find("Chargeai").GetComponent<ChargeAi>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            chargeai.isinwall = true;
            chargeai.chargepoint = new Vector3(myposition.position.x, myposition.position.y, myposition.position.z);
            Debug.Log("º®¹ÚÇÔ");
            rb.isKinematic = true;
            Destroy(gameObject, .5f);
        }
    }
}
