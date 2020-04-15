using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;

    private Transform target;
    public GameObject tar;
    // Start is called before the first frame update
    void Start()
    {
        target = tar.GetComponent<Transform>();
        GameMaster.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;

        
    }
    void OnUpgradeMenuToggle(bool active)
        {
            GetComponent<EnemyFollow>().enabled = !active;
            
        }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            //RotateTowards(target.position);
        }
    }

    void OnDestroy()
    {
        GameMaster.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }

    //private void RotateTowards(Vector2 target)
    //{
    //    var offset = -90f;
    //    Vector2 direction = target - (Vector2)transform.position;
    //    direction.Normalize();
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(Vector3.up * (angle + offset));
    //}
}
