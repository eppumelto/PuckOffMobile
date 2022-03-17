using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToRightPos : MonoBehaviour
{

    private Vector3 targetPos;       //positio johon vihu liikkuu
    //kertoo voiko vihua lyoda
    public static bool cantHit;    //boolia muokataan myos TakeDmg scriptissa
    

    public float speed;         //kertoo kuinka nopeasti vihu liikkuu
    public int distFromPlayer;
    public float korkeus;

    private Animator enemAnimator; //vihu animator

    private GameObject Player;


    private void Start()
    {
        cantHit = false;

        enemAnimator = GameObject.FindWithTag("Enemy").GetComponent<Animator>();    //otetaan animator

        Player = GameObject.FindWithTag("Player");

        targetPos = Player.transform.position + new Vector3(distFromPlayer, korkeus, 0);
        enemAnimator.SetTrigger("Walk");   // vihu animaatio

       


    }
    void Update()
    {
        //laittaa vihun liikkeelle oikeaan positioon
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        //jos vihu on oikeassa kohdassa se pysähtyy ja koodi poistetaan
        if(transform.position == targetPos)
        {
            cantHit = true;
            Destroy(gameObject.GetComponent<MoveToRightPos>());
            enemAnimator.SetTrigger("WalkStop");
            enemAnimator.Rebind();
        }

    }
}
