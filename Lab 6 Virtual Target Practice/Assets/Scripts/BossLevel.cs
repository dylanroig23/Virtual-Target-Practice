using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collidedObject)
    {
        if (collidedObject.tag == "Boss Wall")
        {
            Debug.Log("Entered Boss Room");
        }
    }
}
