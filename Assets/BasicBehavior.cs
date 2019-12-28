using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class BasicBehavior : MonoBehaviour
{
    UnityArmatureComponent a;
    bool isAttacking = false;
    public int Health = 5;
    int speed = 0;
    Vector2 _currentPosition;
    Vector2 _endPosition;
    string state;
    string currentDirection = "Left";
    public UnityEngine.Transform SightDistanceRight;
    public UnityEngine.Transform SightDistanceLeft;
    private UnityEngine.Transform SightDistance;
    PlayerBehaviour pb;
    // Start is called before the first frame update
    void Start()
    {
        
       a  = GetComponent(typeof(UnityArmatureComponent)) as UnityArmatureComponent;
       a.animation.Play("Idle_Animation");
       chooseDirection(null);
    }

    // Update is called once per frame
    void Update()
    {

        _currentPosition = new Vector2(transform.position.x, SightDistance.position.y);
        _endPosition = new Vector2(SightDistance.position.x, SightDistance.position.y);

        var hits = Physics2D.LinecastAll(_currentPosition, _endPosition);
        foreach (var obj in hits)
        {
            var target = obj.collider.gameObject;

            if (target.CompareTag("Player"))   // противник увидел игрока
            {   
                if (!isAttacking)
                    Walk();       
            }
        }
        if (hits.Length == 3)
            Idle();
        playAnimationByState(state);
        
    }

    void playAnimationByState(string state)
    {
        switch (state) 
        {
            case "idle":
            if (a.animation.lastAnimationName != "Idle_Animation")
                a.animation.Play("Idle_Animation");
            break;
            case "run":
            if (a.animation.lastAnimationName != "Walk_Animation")
                a.animation.Play("Walk_Animation");
            break;
            case "attack":
            if (a.animation.lastAnimationName != "Attack_Animation")
                a.animation.Play("Attack_Animation");
            break;
        }
    }

    void Walk()
    {   
        var rb = GetComponent(typeof (Rigidbody2D)) as Rigidbody2D;
        rb.constraints = RigidbodyConstraints2D.None;
        state = "run";
        
        if (currentDirection == "left")
        a.transform.Translate(Vector2.left*Time.deltaTime);
        else if (currentDirection == "right")
        a.transform.Translate(Vector2.right*Time.deltaTime);
    }

    public void Idle()
    {
        
        var rb = GetComponent(typeof (Rigidbody2D)) as Rigidbody2D;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        isAttacking = false;
        state = "idle";
        chooseDirection(null);
        playAnimationByState(state);
    }

    public void chooseDirection(GameObject target)
    {
        if (target != null && target.transform.position.x <= a.transform.position.x)
        currentDirection = "left";
        else if (target == null)
        currentDirection = "right";

        if (currentDirection == "left")
        {
            SightDistance = SightDistanceLeft;
            a.armature.flipX = true;
        }
        else if (currentDirection == "right")
        {
            SightDistance = SightDistanceRight;
            a.armature.flipX = false; 
        }
    }

    public IEnumerator ReceiveDamage(int amount)
    {
        Health -= amount;
        

        Debug.Log("Enemy got hit!");
        
        yield return null;

        yield return new WaitForSeconds(.2f);    

        state = "idle";
        playAnimationByState(state);

        if (gameObject != null && Health <= 0)
        {
            Destroy(gameObject);
        }

        yield return null;
    }
    
    public IEnumerator Attack(GameObject target)
    {
        isAttacking = true;
        state = "attack";
        yield return null;
        pb = target.GetComponent(typeof (PlayerBehaviour)) as PlayerBehaviour;
        pb.StartCoroutine(pb.ReceiveDamage(1));
        yield return new WaitForSeconds(1000.0f);
        isAttacking = false;
        state = "idle";
        yield return null;
    }
}
