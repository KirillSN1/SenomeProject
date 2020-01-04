using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersOfGeneration : MonoBehaviour
{
    public AnimationCurve flyingMove;
    [Range(0, 1)]
    public float flyingTrace;
    private bool gotOut = false;
    private GameObject[] targets = new GameObject[2];
    int nextItem = 0;
    int positionNext;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in targets)
        {
            flyingTrace = flyingMove.Evaluate(positionNext);
            if (item != null)
            item.GetComponent<Rigidbody2D>().velocity = Vector2.up; 
        }
    }

    public void ArtefactCreated(GameObject prefub, GameObject parent){
    
    if (nextItem != this.targets.Length)
        {   
            var newArt = Instantiate(prefub,new Vector3(parent.transform.position.x, parent.transform.position.y+0.9f, parent.transform.position.z),Quaternion.Euler(new Vector3(0,0,0)));
            newArt.SetActive(true);
            newArt.transform.parent = parent.transform;
            this.targets[nextItem++] = newArt;
            newArt.AddComponent<Rigidbody2D>();
        }
    else
        Debug.Log("It is all");     
    }
}
