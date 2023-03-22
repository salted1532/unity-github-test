using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class attackpoint : MonoBehaviour
{
    public Transform myObject;
    public float onofftimer;
    public float deltimer;
    private MeshRenderer myRenderer;
    tunnelAi TAi;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        TAi = GameObject.Find("TunnelAi").GetComponent<tunnelAi>();
        TAi.OnGroundpoint = new Vector3(myObject.position.x, myObject.position.y, myObject.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(objectonoff), onofftimer);
        Invoke(nameof(del), deltimer);
    }

    void objectonoff()
    {
        myRenderer.enabled = !myRenderer.enabled;
    }

    void del()
    {
        Destroy(gameObject,.5f);
    }
}
