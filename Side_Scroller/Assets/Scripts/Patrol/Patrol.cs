using System.Collections;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] myPatrol_Target;
    [SerializeField] Vector3 direction;

    int curTarget = 0;

    //Stats
    public float speed = 1.5f;
    void Start()
    {
        GetDirection();
    }
    void GetDirection()
    {
        direction = Vector3.Normalize(myPatrol_Target[curTarget].position - transform.position); 
        Debug.Log(direction);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        //bWait = false;
        GetDirection();
    }
    void Update()
    {
       // if (bWait)
        {
            transform.position += new Vector3(direction.x * speed * Time.deltaTime, direction.y, 0f);

            if (Vector3.Distance(myPatrol_Target[curTarget].position, transform.position) <= 0.5f)
            {
                curTarget++;
                if (curTarget >= myPatrol_Target.Length) curTarget = 0;
                GetDirection();
                StartCoroutine(Wait());

            }
        }
        
    }
}
