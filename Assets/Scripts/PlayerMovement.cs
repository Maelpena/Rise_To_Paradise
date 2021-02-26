using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour
{
    private bool isJumpingUp = false;
    public int dir = 1;
    public float speed = 2.85f;
    public float maxSpeed = 2.85f;
    public float jumpSpeed = 8.95f;
    public float maxWallSlideSpeed = 2.50f;
    public Rigidbody2D rb;
    public bool isGrounded;
    public bool isWallSlide;
    private float gravity = -55.0f;
    private float Normalgravity = -55.0f;
    private float Jumpgravity = -23.0f;
    private float maxGravitySpeed = -8.5f;
    private float modifier = 1.0f;
    private int nbJumpMax = 2;
    private int nbJumpLeft = 0;
    private float OffsetY = 0.20f;
    private float OffsetX = 0.38f;
    private float OffsetXWall = 0.2f;
    private float OffsetYWall = 0.55f;
    public LayerMask lmForWalls;
    public LayerMask lmForFloor;
    public Animator anim;
    public Transform feetPos;
    private CharacterData charData;
    public float powerUpCoolDown = 0;
    private bool isPowerUp = false;
    public bool isShield = false;
    public bool isInvincible = false;


    public enum STATES {Walk, Jump,Fall, WallSlide, SecondJump };
    string[] StatesSTab = new string[] { "Walk", "Jump", "Fall", "WallSlide","SecondJump" };
    public STATES myState;
    public STATES myLastState;
    public int myLastDir;
    public bool isAlive = true;
    // Start is called before the first frame update

    void Start()
    {
        charData = GetComponent<CharacterData>();
        anim = GetComponentInChildren<Animator>();
        GetComponent<CollisionScript>().EnewCollision.AddListener(ApplyCollision);
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpCoolDown > 0)
        {
            powerUpCoolDown -= Time.deltaTime;
        } else if (powerUpCoolDown < 0)
        {
            powerUpCoolDown = 0;
            resetPowerUp();
        }

        if (isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PerformAction();
            }
            if (myState == STATES.Jump || (myState == STATES.SecondJump))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    gravity = Jumpgravity;
                }
                else
                {
                    gravity = Normalgravity;
                }
            }
            else
            {
                gravity = Normalgravity;
            }
            
                
            Animate();
        }      
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            MovePlayer();
            CheckForAction();
        }

    }


    private void MovePlayer()
    {

        charData.velocity.x = dir * speed;
        if (!isGrounded)
        {
            charData.velocity.y = charData.velocity.y + gravity * Time.deltaTime * modifier;
            charData.velocity.y = Mathf.Max(charData.velocity.y, maxGravitySpeed);
            if (isWallSlide)
            {
                if (charData.velocity.y < -maxWallSlideSpeed)
                {

                    if (charData.velocity.y < -maxWallSlideSpeed)
                    {
                        charData.velocity.y = -maxWallSlideSpeed;
                    }
                }
            }

        }
        else
        {
            charData.velocity.y = 0;
        }

        rb.MovePosition(new Vector2(charData.velocity.x * Time.deltaTime + transform.position.x, charData.velocity.y * Time.deltaTime + transform.position.y));
        if (charData.velocity.y == 0) CheckForGround();
        if (isWallSlide) CheckForEndOfWall();
    }

    private void CheckForAction()
    {

    }
    private void ApplyCollision(Vector2 normalCollision )
    {
        if (normalCollision == Vector2.right || normalCollision == -Vector2.right)
        {
            CollideSide();
        }else if (normalCollision == Vector2.up)
        {
            CollideFloor();
        }else if (normalCollision == Vector2.down)
        {
            CollideSealing();
        }
    }
    private void CollideSide()
    {
        if (!isGrounded)
        {
            MakeWallSlide();
        }
        else
        {
            ChangeDirection();
        }
    }

    private void CollideFloor()
    {
        MakeGrounded();
    }

    private void CollideSealing()
    {
        charData.velocity.y = 0;
    }

    private void NoMoreSideWall()
    {
        if (isWallSlide)
        {
            isWallSlide = false;
            speed = maxSpeed;
            myState = STATES.Fall;
        }
    }

    private void Fall()
    {
        if (isGrounded)
        {
            myState = STATES.Fall;
        }
    }


    void CheckForGround()
    {
        RaycastHit2D rc1 = Physics2D.Raycast(new Vector2(rb.position.x - OffsetX, rb.position.y - 0.40f), -Vector2.up, OffsetY, lmForFloor);
        RaycastHit2D rc2 = Physics2D.Raycast(new Vector2(rb.position.x + OffsetX, rb.position.y- 0.40f), -Vector2.up, OffsetY, lmForFloor);
        if (rc1)
        {

            if (rc1.collider.gameObject.tag.Equals("Floor") && rc1.distance <= OffsetY - OffsetY / 4)
            {

                MakeGrounded();
            }
        }
        else if (rc2)
        {

            if (rc2.collider.gameObject.tag.Equals("Floor") && rc2.distance <= OffsetY - OffsetY / 4)
            {

                MakeGrounded();
            }
        }
        else
        {

            isGrounded = false;
        }
        Debug.DrawRay(new Vector2(rb.position.x - OffsetX, rb.position.y - 0.40f), new Vector3(0,-OffsetY,0),Color.yellow,0.0f);
        Debug.DrawRay(new Vector2(rb.position.x + OffsetX, rb.position.y - 0.40f), new Vector3(0,-OffsetY,0),Color.yellow,0.0f);
    }

    void ChangeDirection()
    {
        dir = dir * -1;
    }
    void MakeGrounded()
    {
        nbJumpLeft = nbJumpMax;
        isGrounded = true;
        if (isWallSlide)
        {
            modifier = 1.0f;
            speed = maxSpeed;
            dir = dir * -1;
            isWallSlide = false;
        }
        myState = STATES.Walk;
    }

    void MakeWallSlide()
    {
        modifier = 1f;
        isWallSlide = true;
        speed = 0.0f;
        nbJumpLeft = 2;
        myState = STATES.WallSlide;
    }

    void CheckForEndOfWall()
    {
        RaycastHit2D rc1 = Physics2D.Raycast(new Vector2(rb.position.x + OffsetX * dir, rb.position.y + OffsetYWall), Vector2.right * dir, OffsetXWall, lmForWalls);
        RaycastHit2D rc2 = Physics2D.Raycast(new Vector2(rb.position.x + OffsetX * dir, rb.position.y - OffsetYWall), Vector2.right * dir, OffsetXWall, lmForWalls);
        RaycastHit2D rc3 = Physics2D.Raycast(new Vector2(rb.position.x + OffsetX * dir, rb.position.y), Vector2.right * dir, OffsetXWall, lmForWalls);
        Debug.DrawRay(new Vector2(rb.position.x + OffsetX * dir, rb.position.y + OffsetYWall), new Vector3(OffsetXWall * dir, 0, 0), Color.green, 0.0f);
        Debug.DrawRay(new Vector2(rb.position.x + OffsetX * dir, rb.position.y - OffsetYWall), new Vector3(OffsetXWall * dir, 0, 0), Color.green, 0.0f);
        Debug.DrawRay(new Vector2(rb.position.x + OffsetX * dir, rb.position.y), new Vector3(OffsetXWall * dir, 0, 0), Color.green, 0.0f);
        if (!(rc1 || rc2 || rc3))
        {
            NoMoreSideWall();
        }
    }

    void PerformAction()
    {
        if (MakeJump())
        {
            gameObject.GetComponent<WeaponManager>().UseWeapon();
        }
        
    }

    bool MakeJump()
    {
        if (nbJumpLeft > 0)
        {
            charData.velocity.y = jumpSpeed;
            if (!isGrounded)
            {
                myState = STATES.SecondJump;
            }
            else
            {
                myState = STATES.Jump;
            }
            isGrounded = false;
            if (isWallSlide)
            {
                speed = maxSpeed;
                charData.velocity.y = jumpSpeed;
                dir = dir * -1;
                modifier = 1.0f;
                isWallSlide = false;
            }
            nbJumpLeft--;
            
            
            return true;
        }
        return false;
    }



    public void Die()
    {
        isAlive = false;
        if (dir > 0)
        {
            anim.Play("Die" + "_r");
        }
        else
        {
            anim.Play("Die" + "_l");
        }
    }

    public void PickUpPowerUp(string type, int amount, float time)
    {
        Debug.Log("Power Up");
        switch (type)
        {
            case "bullet_speed":
                resetPowerUp();
                GetComponent<WeaponManager>().ActualWeapon.setSpeedBonus(amount);
                break;
            case "bullet_size":
                resetPowerUp();
                GetComponent<WeaponManager>().ActualWeapon.setSizeBonus(amount);
                break;
            case "shield":
                resetPowerUp();
                isShield = true;
                break;
            case "double_bullet":
                resetPowerUp();
                GetComponent<WeaponManager>().ActualWeapon.setDoubleBullet(true);
                break;
            case "invincible":
                resetPowerUp();
                isInvincible = true;
                break;
            default:
                break;
        }
        powerUpCoolDown = time;
    }

    public void resetPowerUp()
    {
        GetComponent<WeaponManager>().ActualWeapon.setSpeedBonus(0);
        GetComponent<WeaponManager>().ActualWeapon.setSizeBonus(0);
        isShield = false;
        GetComponent<WeaponManager>().ActualWeapon.setDoubleBullet(false);
        isInvincible = false;


    }

    private void Animate()
    {
        if(!isWallSlide && !isGrounded && charData.velocity.y < 0)
        {
            myState = STATES.Fall;
        }
        if(myLastState != myState || myLastDir != dir)
        {
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
                case STATES.WallSlide:
                    etat = "WallSlide";
                    break;
                case STATES.SecondJump:
                    etat = "SecondJump";
                    break;
                default:
                    break;
            }
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
   
