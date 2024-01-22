using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCube : MonoBehaviour
{
    [SerializeField] private GameObject Monster;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject MonsterPoint;
    // Start is called before the first frame update
    void Start()
    {
        Monster.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Player.transform.position, MonsterPoint.transform.position) < 1)
        {
            Monster.SetActive(true);
        }
    }
}
