using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipOnDir : MonoBehaviour
{
    public EnemyMovement em;
    public int lastPos = 1;
    public BoxCollider2D shieldHitBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(em.dir != lastPos)
        {
            lastPos = em.dir;
            FlipIt();
        }
    }

    void FlipIt()
    {
        gameObject.transform.localPosition = new Vector2( gameObject.transform.localPosition.x * -1, gameObject.transform.localPosition.y);
    }
}
