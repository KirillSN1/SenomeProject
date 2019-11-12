using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float KnockBackSpeed = 5.0f;
    public float KnockTime = 0.5f;


    public void HitSomeObject(GameObject other)
    {
       var objToKnock = other.GetComponent<Rigidbody2D>();

        if (objToKnock != null)
        {
            if(objToKnock.CompareTag("Player"))
            {
             //   objToKnock.GetComponent<Renderer>().material.color = Color.red;
                
                objToKnock.AddForce(transform.up * KnockBackSpeed, ForceMode2D.Impulse);

                StartCoroutine(KnockBackLastForPlayer(objToKnock));
             //   objToKnock.GetComponent<Renderer>().material.color = Color.white;
            }
            else
            {
               Vector2 difference = objToKnock.transform.position - transform.position;
               difference = difference.normalized * KnockBackSpeed;

               objToKnock.AddForce(difference, ForceMode2D.Impulse);

               StartCoroutine(KnockBackLast(objToKnock));
            } 
        }
    }

    public IEnumerator KnockBackLast(Rigidbody2D objToKnock)
    {
        yield return new WaitForSeconds(KnockTime);
        if (objToKnock != null)
        {
            objToKnock.velocity = Vector2.zero;
        }
    }

    public IEnumerator KnockBackLastForPlayer(Rigidbody2D objToKnock)
    {
      //  objToKnock.GetComponent<Renderer>().material.color = Color.red;

        yield return new WaitForSeconds(KnockTime);
        if (objToKnock != null)
        {
            objToKnock.velocity = Vector2.zero;
        }

     //   objToKnock.GetComponent<Renderer>().material.color = Color.white;
    }
}
