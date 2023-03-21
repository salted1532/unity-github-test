using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackpoint : MonoBehaviour
{
    public GameObject myObject;
    public float onofftimer;
    private MeshRenderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = myObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke(nameof(objectonoff), onofftimer);
    }

    void objectonoff()
    {
        myRenderer.enabled = !myRenderer.enabled;
    }
}
