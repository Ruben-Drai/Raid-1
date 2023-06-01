using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private GameObject origin;
    [SerializeField] private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(target.transform.position.x - origin.transform.position.x, 2) + Mathf.Pow(target.transform.position.y - origin.transform.position.y, 2));
        transform.position = new Vector3((origin.transform.position.x + target.transform.position.x)/ 2, (origin.transform.position.y + target.transform.position.y )/ 2, 0);
        transform.localScale = new Vector3(0.5f, distance*1.7f, 0);
    }
}
