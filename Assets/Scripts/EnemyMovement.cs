using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float Normalspeed;
    public int dir = 1;
    public Rigidbody2D rb;
    public LayerMask lmForFloor;
    public float OffsetY;
    public float YToFeet;
    public float OffsetX;
    public Animator anim;
    public enum STATES { Walk, Jump, Fall, Attack, Idle };
    string[] StatesSTab = new string[] { "Walk", "Jump","Attack","Idle"};
    public STATES myState;
    public STATES myLastState;
    public int myLastDir;
    public bool isAlive = true;
    private int life = 1;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CollisionScript>().EnewCollision.AddListener(ApplyCollision);
        anim = GetComponentInChildren<Animator>();
        myState = STATES.Walk;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Move();
        }
    }       
    void Update()
    {
        if (isAlive)
        {
            Animate();
        }
    }

    void Move()
    {
        rb.MovePosition(new Vector2(speed * dir * Time.deltaTime  + transform.position.x , transform.position.y));
        CheckForGround();

    }


    void ChangeDirection()
    {
        dir = dir * -1;
    }

    private void ApplyCollision(Vector2 normalCollision,Collider2D collision)
    {
        if (normalCollision == Vector2.right || normalCollision == -Vector2.right)
        {
            if(collision.gameObject.layer == 11)
            {
                if (collision.gameObject.layer == 11 && collision.gameObject.GetComponent<EnemyMovement>().dir != dir)
                {
                    collision.gameObject.GetComponent<EnemyMovement>().ChangeDirection();
                    ChangeDirection();
                }
                else if (collision.gameObject.layer == 11 && collision.gameObject.GetComponent<EnemyMovement>().speed > speed)
                {
                    collision.gameObject.GetComponent<EnemyMovement>().ChangeDirection();
                }
                else
                {
                    ChangeDirection();
                }
            }       
            else
            {
                ChangeDirection();
            }
            
            
        }
    }

    void CheckForGround()
    {
        RaycastHit2D rc1 = Physics2D.Raycast(new Vector2(rb.position.x - OffsetX, rb.position.y - YToFeet), -Vector2.up, OffsetY, lmForFloor);
        RaycastHit2D rc2 = Physics2D.Raycast(new Vector2(rb.position.x + OffsetX, rb.position.y - YToFeet), -Vector2.up, OffsetY, lmForFloor);

        if (!rc1 && dir == -1 || !rc2 && dir == 1)
        {
            ChangeDirection();
        }
        else
        {
        }

  
        Debug.DrawRay(new Vector2(rb.position.x - OffsetX, rb.position.y - YToFeet), new Vector3(0, -OffsetY, 0), Color.red, 0.0f);
        Debug.DrawRay(new Vector2(rb.position.x + OffsetX, rb.position.y - YToFeet), new Vector3(0, -OffsetY, 0), Color.yellow, 0.0f);
    }



    public void PlayDie()
    {
        CameraController.shakeCoolDown = 0.2f;
        isAlive = false;
        GetComponent<BoxCollider2D>().enabled = false;
        if (dir > 0)
        {
            anim.Play("Die" + "_r");
        }
        else
        {
            anim.Play("Die" + "_l");
        }
    }
    private void Animate()
    {
        if (myLastState != myState || myLastDir != dir)
        {
            bool canAnim = true;
            string etat = null;
            myLastState = myState;
            myLastDir = dir;
            switch (myState)
            {
                case STATES.Walk:
                    etat = "Walk";
                    break;
                case STATES.Jump:
                    etat = "Jump";
                    break;
                case STATES.Fall:
                    etat = "Fall";
                    break;
                case STATES.Attack:
                    etat = "Attack";
                    canAnim = false;
                    break;
                case STATES.Idle:
                    etat = "Idle";
                    break;
                default:
                    break;
            }
            if (canAnim)
            {
                if (dir > 0)
                {
                    anim.Play(etat + "_r");
                }
                else
                {
                    anim.Play(etat + "_l");
                }
            }
            

        }
    }
}
