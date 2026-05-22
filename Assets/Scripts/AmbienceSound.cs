using UnityEngine;

public class AmbienceSound : MonoBehaviour
{
    public Collider area;
    public GameObject player;

    void Update()
    {
        Vector3 closestPoint = area.ClosestPoint(player.transform.position);
        transform.position = closestPoint;
    }
}
