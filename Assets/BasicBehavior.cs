using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;

public class BasicBehavior : MonoBehaviour
{
    UnityArmatureComponent a;
    Vector2 _currentPosition;
    Vector2 _endPosition;
    string state;
    string currentDirection = "Left";
    public UnityEngine.Transform SightDistanceRight;
    public UnityEngine.Transform SightDistanceLeft;
    private UnityEngine.Transform SightDistance;
   
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

            if (target.CompareTag("Player"))   // игрок увидел противника
            {   
                state = "run";
                
                Walk();   
            }
        }
        if (hits.Length == 2)
        {
            chooseDirection(null);
            state = "idle";
        }

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
        if (currentDirection == "left")
        a.transform.Translate(Vector2.left*Time.deltaTime);
        else if (currentDirection == "right")
        a.transform.Translate(Vector2.right*Time.deltaTime);
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


    public void Attack(GameObject target)
    {
        state = "attack";
        playAnimationByState(state);
        var pb = target.GetComponent<PlayerBehaviour>();
        pb.StartCoroutine(pb.ReceiveDamage(1));
    }
}
