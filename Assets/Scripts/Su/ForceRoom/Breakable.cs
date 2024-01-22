using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject _replacement;
    [SerializeField] private float _breakForce = 2;
    [SerializeField] private float _collisionMultiplier = 100;
    [SerializeField] private bool _broken;

    public GameObject triggerCheckGO;
    public AudioSource boxBreakAudioSource;
    public AudioSource HeadPadAudioSource; 
    //public GameObject HeatPadGO;

    void OnCollisionEnter(Collision collision)
    {
        if (_broken) return;
        //Debug.Log(collision.gameObject.name + "name"); 
        if(collision.gameObject.name.Contains("Cannon Ball"))
        {
            //Debug.Log("Yo hyes yes ");
            if (collision.relativeVelocity.magnitude >= _breakForce)
            {
                if(gameObject.name == "MysteryCube" + triggerCheckGO.GetComponent<TriggerCheck>().HeatPadInt1)
                {
                    triggerCheckGO.GetComponent<TriggerCheck>().HeatPadGO.transform.position = gameObject.transform.position;
                    triggerCheckGO.GetComponent<TriggerCheck>().HeatPadGO.SetActive(true);
                    HeadPadAudioSource.Play(); 
                    //HeatPadGO.transform.position = collision.gameObject.transform.position;
                    ///Debug.Log("Bulls Eye. Pad is discovered");
                }
                _broken = true;
                var replacement = Instantiate(_replacement, transform.position, transform.rotation);

                var rbs = replacement.GetComponentsInChildren<Rigidbody>();
                foreach (var rb in rbs)
                {
                    rb.AddExplosionForce(collision.relativeVelocity.magnitude * _collisionMultiplier, collision.contacts[0].point, 2);
                }

                Destroy(gameObject);
                boxBreakAudioSource.Play(); 
                //ScriptGO.GetComponent<GameSceneScript>().GravityDir = -1; 
                triggerCheckGO.GetComponent<TriggerCheck>().CheckingHeatCube();
                //gameObject.GetComponent<ChangeGravityDir>().GravityCharge(); 
                Destroy(replacement, 5f);
            }
        }
        
    }
}