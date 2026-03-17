using GogoGaga.OptimizedRopesAndCables;
using UnityEngine;

public class FireHoseSlack : MonoBehaviour
{
    public Rope rope;
    public Transform hoseStart;
    public Transform hoseEnd;

    public float maxHoseLength = 20f;
    public float slackAmount = 2f;

    void Update()
    {
        float distance = Vector3.Distance(hoseStart.position, hoseEnd.position);

        float targetLength = Mathf.Clamp(distance + slackAmount, 0, maxHoseLength);

        rope.ropeLength = targetLength;
    }
}