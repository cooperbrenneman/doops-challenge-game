using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.05f;
    public LayerMask blockingLayer;
    public LayerMask slippingLayer;
    public LayerMask swimmingLayer;
    public bool DoneMoving;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        DoneMoving = true;
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            // Set DoneMoving to false to signify object is still moving
            DoneMoving = false;
            // Get new position based on deltaTime
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);
            // Move the rigidbody of the object to the new position
            rb2D.MovePosition(newPosition);
            // Calculate remaining distance
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            yield return null;
        }

        Debug.Log("Done Moving");
        // Complete movement
        DoneMoving = true;
    }

    protected abstract void OnCantMove<T>(T component) where T : Component;

    protected abstract bool Move(int xDir, int yDir, out RaycastHit2D hit, out Vector2 destination);

    public virtual void AttemptMove<T>(int xDir, int yDir) where T : Component
    {
        RaycastHit2D hit;
        Vector2 destination;

        // See if the player can move
        bool canMove = Move(xDir, yDir, out hit, out destination);

        // If the player did not hit anything, do nothing
        if (hit.transform == null)
        {
            return;
        }

        T hitComponent = hit.transform.GetComponent<T>();

        // If cannot move and the component that was hit was not null
        if (!canMove && hitComponent != null)
        {
            OnCantMove(hitComponent);
        }
    }

}
