using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    public Transform player;
    public float delay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        transform.LookAt(player);
        Invoke(nameof(del), delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void del()
    {
        Destroy(gameObject);
    }
}
