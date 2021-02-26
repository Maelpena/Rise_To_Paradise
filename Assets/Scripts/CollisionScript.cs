using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Vector2Event : UnityEvent<Vector2> { }
public class CollisionScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2Event EnewCollision = new Vector2Event();
    public Transform feetPos;
    public CharacterData charData;
    public List<ContactPoint2D> listContacts = new List<ContactPoint2D>();
    public List<Vector2> lvContacts = new List<Vector2>();
    public List<Collider2D> lvColliders = new List<Collider2D>();
    public LayerMask lMaskCollision;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void TryAddNewHitAndCallEvent(Vector2 newHit, Collider2D collider)
    {
        bool isNew = true;
        for (int i = 0; i < lvContacts.Count; i++)
        {
            if (lvContacts[i] == newHit){
                isNew = false;
                break;
            }
        }
        if (isNew)
        {
            lvContacts.Add(newHit);
            lvColliders.Add(collider); 
            EnewCollision.Invoke(newHit);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 newHit = new Vector2(0, 0);
        if (lMaskCollision == (lMaskCollision | (1 << collision.gameObject.layer))) {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                newHit = CheckThisContact(contact);

                if (newHit != new Vector2(0, 0))
                {
                    TryAddNewHitAndCallEvent(newHit, contact.collider);
                }
            }
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckForOldCollisions(collision);
        if (lMaskCollision == (lMaskCollision | (1 << collision.gameObject.layer)))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Vector2 zHit = CheckThisContact(contact);
                if (zHit != new Vector2(0, 0))
                {
                    bool isContactRegistered = false;
                    for (int i = 0; i < lvContacts.Count; i++)
                    {

                        {
                            if (Vector2.Angle(lvContacts[i], zHit) <= 0.1f)
                            {
                                isContactRegistered = true;
                            }
                        }

                    }

                    if (!isContactRegistered)
                    {
                        Vector2 newHit = CheckThisContact(contact);
                        if (newHit != new Vector2(0, 0))
                        {
                            TryAddNewHitAndCallEvent(newHit, contact.collider);

                        }
                    }
                }

            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (lMaskCollision == (lMaskCollision | (1 << collision.gameObject.layer)))
        {
            if (lvColliders.Count > 0)
            {
                for (int i = lvColliders.Count - 1; i >= 0; i--)
                {
                    if (collision.collider == lvColliders[i])
                    {
                        lvContacts.RemoveAt(i);
                        lvColliders.RemoveAt(i);
                    }
                }

            }
        }
            
    }
    private Vector2 CheckThisContact(ContactPoint2D contact)
    {
        Vector2 hit = new Vector2(0, 0);
        if (contact.collider.gameObject.tag.Equals("Floor") && (contact.normal == Vector2.right||Vector2.Angle(contact.normal,Vector2.right)<=0.1f ) || (contact.normal == -Vector2.right.normalized || Vector2.Angle(contact.normal, -Vector2.right) <= 0.1f))
        {
            if (contact.collider.gameObject.GetComponent<PlatformEffector2D>() == null)
            {
                if (!(contact.point.y < feetPos.position.y))
                {
                    //RaycastHit2D()
                    if (contact.normal == Vector2.right || Vector2.Angle(contact.normal, Vector2.right) <= 0.1f)
                    {
                        hit = Vector2.right;
                    }
                    else
                    {
                        hit = -Vector2.right;
                    }
                }
                
            }
        }
        else if ((contact.collider.gameObject.tag.Equals("Floor") && contact.point.y < feetPos.position.y) &&( contact.normal == Vector2.up || (Vector2.Angle(contact.normal, Vector2.up) == 0.0f)) && charData.velocity.y <= 0.0f)
        {
            if (!contact.collider.OverlapPoint(feetPos.position))
            {
                if(contact.collider.ClosestPoint(feetPos.position).y< feetPos.position.y)
                {
                    hit = Vector2.up;
                    Debug.DrawRay(contact.point, Vector2.up * 0.08f, Color.blue, 3.0f);
                }

                //contact.otherCollider.gameObject.transform.position = new Vector2(contact.otherCollider.gameObject.transform.position.x, contact.otherCollider.gameObject.transform.position.y + 0.005f);
            }
        }
        else if (contact.collider.gameObject.tag.Equals("Floor") && contact.normal == Vector2.down && contact.collider.gameObject.GetComponent<PlatformEffector2D>() == null)
        {
            hit = Vector2.down;

        }
        return hit;
    }

    void CheckForOldCollisions(Collision2D collision)
    {
        for (int i = 0; i < lvContacts.Count; i++)
        {
            if(lvColliders[i] == collision.collider)
            {
                bool IsStillColliding = false;
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    if (Vector2.Angle(contact.normal,lvContacts[i]) <= 0.1f)
                    {
                        IsStillColliding = true;                     
                    }
                }
                if (!IsStillColliding)
                {
                    //EoldCollision.Invoke(lvContacts[i]);
                    lvContacts.RemoveAt(i);
                    lvColliders.RemoveAt(i);

                }
            }           
        }
    }
}
