using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    
    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    bool broken = true;
   
     Animator animator;

     public ParticleSystem smokeEffect;

     private GameObject player;

     bool playerInSight;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        player = GameObject.Find("ruby");
    }


    void Update()
    {
       
       if(!broken)
       {
         return;
       }
       
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        

        

    }

    
    void FixedUpdate()
    {
       
       if(!broken)
       {
          return;
       }

       if(playerInSight == true)
        {
            Vector2 lookDirection = (player.transform.position - transform.position).normalized;
            rigidbody2D.AddForce(lookDirection * speed);
        }else
        {
            Vector2 position = rigidbody2D.position;
        
            if (vertical)
            {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
                animator.SetFloat("Move Y", direction);
            }
            else
            {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
                animator.SetFloat("Move Y", 0);
            }
        
            rigidbody2D.MovePosition(position);

        }
       
       
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController >();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerInSight = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerInSight = false;
        }
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
    }

}







