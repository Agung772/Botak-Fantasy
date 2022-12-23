using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public Transform targetTeleport;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerContronller>())
        {
            other.GetComponent<PlayerContronller>().transform.position = targetTeleport.position;
            other.GetComponent<PlayerContronller>().transform.eulerAngles += new Vector3(0, 180, 0);
            print("Teleport");
        }
    }
}
