using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRightPos : MonoBehaviour
{
    //Positio mihin liikutaan
    public Vector3 targetPos;
    private Vector3 newPos;

    public float speed;


    private void Start()
    {
        newPos = Vector3.MoveTowards(gameObject.transform.position, targetPos,50);
    }

    void Update()
    {

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);


        if(transform.position == targetPos)
        {
            Destroy(gameObject.GetComponent<MoveToRightPos>());
        }
    }
}
