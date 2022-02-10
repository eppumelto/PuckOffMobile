using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRightPos : MonoBehaviour
{
    //Positio mihin liikutaan
    public Vector3 targetPos;
    public static bool cantHit; //boolia muokataan myos TakeDmg scriptissa
    private Vector3 newPos;

    public float speed;

    public Transform target;

    private Animator enemAnimator;
    //private Animator bAnimator;

    private void Start()
    {
        target = GameObject.Find("Target").transform;
        //enemAnimator = GameObject.Find("Enemy").GetComponent<Animator>();
        enemAnimator = GameObject.FindWithTag("Enemy").GetComponent<Animator>();


        enemAnimator.SetTrigger("Walk");
    
    }
    void Update()
    {

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);


        if(transform.position == target.position)
        {
            cantHit = true;
            Destroy(gameObject.GetComponent<MoveToRightPos>());
            enemAnimator.SetTrigger("WalkStop");
        }

    }
}
