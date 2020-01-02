using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMover : MonoBehaviour
{

    // Directions to use for movement
    protected enum Direction {
        Left,
        Right,
        Up,
        Down,
        None
    }

    // Useful dictionaries for use with directions
    protected Dictionary<Direction, Vector2> directions = new Dictionary<Direction, Vector2> {
        { Direction.Left, new Vector2(-1.0f, 0.0f) },
        { Direction.Right, new Vector2(1.0f, 0.0f) },
        { Direction.Up, new Vector2(0.0f, 1.0f) },
        { Direction.Down, new Vector2(0.0f, -1.0f) },
        { Direction.None, new Vector2(0.0f, 0.0f) }
    };
    protected Dictionary<Direction, Direction> opposite = new Dictionary<Direction, Direction> {
        { Direction.Left, Direction.Right },
        { Direction.Right, Direction.Left },
        { Direction.Up, Direction.Down },
        { Direction.Down, Direction.Up },
        { Direction.None, Direction.None }
    };

    // Used for movement
    private Rigidbody2D body;
    private Direction movingDirection;
    private Vector2 cursor;

    // Movement speed
    //[SerializeField] float moveSpeed = 0.08f;
    protected float moveSpeed = 0.08f;

    // Called when the object reaches its cursor position
    public virtual void ReachedCursorAction() { }

    // Called when the object is created
    public virtual void ChildStart() { }

    // Start is called before the first frame update
    public void Start() {
        body = GetComponent<Rigidbody2D>();
        movingDirection = Direction.None;
        cursor = new Vector2(transform.position.x, transform.position.y);
        ChildStart();
    }

    // Update is called once per frame
    public void FixedUpdate() {

        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        
        // First case is if we are not currently moving
        if (movingDirection == Direction.None) {
            
            // Return if we shouldn't be moving
            if (cursor.x == pos.x && cursor.y == pos.y) {
                return;
            }

            // Set moving direction accordingly
            if (cursor.x != pos.x) {
                movingDirection = (cursor.x > pos.x) ? Direction.Right : Direction.Left;
            } else {
                movingDirection = (cursor.y > pos.y) ? Direction.Up : Direction.Down;
            }

        }

        // We should move in the direction of the movingDirection
        Vector3 cursorProj = Vector3.Project(cursor - pos, directions[movingDirection]);
        Vector2 remainingDelta = new Vector2(cursorProj.x, cursorProj.y);
        Vector2 moveDelta = moveSpeed * directions[movingDirection] * Time.deltaTime;

        // Perform the movement
        if (moveDelta.magnitude < remainingDelta.magnitude) {
            body.MovePosition(pos + moveDelta);
        } else {
            body.MovePosition(pos + remainingDelta);
            movingDirection = Direction.None;
            if (pos + remainingDelta == cursor) {
                ReachedCursorAction();
            }
        }

    }

    // Updates the location of the cursor by transforming it the given distance
    // Stops if a wall is hit
    public bool MoveCursor(Vector2 movement) {

        //Debug.Log("Before");
        // Reset cursor to be the closest integral position in direction of travel
        if (movingDirection != Direction.None) {
            cursor = new Vector2(transform.position.x, transform.position.y);
            cursor += 0.5f * directions[movingDirection];
            cursor = new Vector2(Mathf.Round(cursor.x), Mathf.Round(cursor.y));
        }
        //Debug.Log("After");

        // Add the change to the cursor so long as no walls are collided with
        bool movedCursor = false;
        while (movement.magnitude > 0.1f) {
            Vector2 unitVec = movement.normalized;
            if (Physics2D.OverlapPoint(cursor + unitVec)) {
                break;
            }
            movedCursor = true;
            cursor += unitVec;
            movement -= unitVec;
        }
        return movedCursor;
    }

}
