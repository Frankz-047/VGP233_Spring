using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPackControl : MonoBehaviour
{
    private SpawnManager sm;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        --sm.totalAmmpPack;
    }
}
