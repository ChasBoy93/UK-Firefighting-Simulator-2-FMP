using UnityEngine;

public class PickupController : MonoBehaviour
{
    public Transform pickupPoint;

    void OnMouseDown()
    {
        transform.parent = pickupPoint.transform;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    void OnMouseUp()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
    }
}
