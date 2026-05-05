using UnityEngine;

public class DragRagdoll : MonoBehaviour
{
    public Transform grabPoint;          // Where hands are (empty object in front of player)
    public float grabRange = 2f;
    public float dragForce = 500f;
    public LayerMask ragdollLayer;

    private Rigidbody grabbedBody;
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isDragging)
                TryGrab();
            else
                Release();
        }
    }

    void FixedUpdate()
    {
        if (isDragging && grabbedBody != null)
        {
            Vector3 direction = grabPoint.position - grabbedBody.position;
            grabbedBody.linearVelocity = direction * 10f;

            // Optional: keep rotation stable
            grabbedBody.angularVelocity = Vector3.zero;
        }
    }

    void TryGrab()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange, ragdollLayer))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                grabbedBody = rb;
                isDragging = true;

                // Reduce resistance for smoother dragging
                grabbedBody.linearDamping = 5f;
                grabbedBody.angularDamping = 5f;
            }
        }
    }

    void Release()
    {
        if (grabbedBody != null)
        {
            grabbedBody.linearDamping = 0f;
            grabbedBody.angularDamping = 0.05f;
        }

        grabbedBody = null;
        isDragging = false;
    }
}