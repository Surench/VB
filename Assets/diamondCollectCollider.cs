using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamondCollectCollider : MonoBehaviour {

    [SerializeField]
    private DiamondController diamondController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            diamondController.CollectDiamond();
        }
    }
}
