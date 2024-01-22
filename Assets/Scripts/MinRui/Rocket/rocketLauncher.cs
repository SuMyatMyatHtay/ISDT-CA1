using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketLauncher : MonoBehaviour
{
    [SerializeField] private Transform RocketPlayerEnter;
    [SerializeField] private List<GameObject> Rockets = new List<GameObject>();
    [SerializeField] private GameObject Player;
    private bool isRocketLaunched = false;
    private int rocketIndex = 0;
    private Animator _ani;
    private GameObject chosenRocket;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var rocket in Rockets)
        {
            rocket.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Player.transform.position, RocketPlayerEnter.position) < 2f)
        {
            if (isRocketLaunched == false)
            {
                changeRocket();
                isRocketLaunched = true;
            }
        }
    }

    public void changeRocket()
    {
        Rockets[rocketIndex].SetActive(false);
        if (rocketIndex == 0)
        {
            rocketIndex = 1;
        }
        else
        {
            rocketIndex = 0;
        }
        Rockets[rocketIndex].SetActive(true);
        _ani = Rockets[rocketIndex].GetComponent<Animator>();
        _ani.SetTrigger("LaunchForward");
    }

}
