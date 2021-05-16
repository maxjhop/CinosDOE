using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    
    public Camera cam;
    private AudioSource whoosh;
    public GameObject spell;
    public Transform firepoint;
    public float projectileSpeed = 30;
    private Vector3 destination;
    public GameObject wand;
    Animator otherAnimator;
    public float fireRate = 0.1f;
    public float burstRate = 5.0f;
    private float nextFire = 0.0f;
    private float nextBurst = 0.0f;
    private int numBurstShots = 0;
    private float nextShot = 0.0f;
   


    // Start is called before the first frame update
    void Start()
    {
        otherAnimator = wand.GetComponent<Animator>();
        whoosh = wand.GetComponent<AudioSource>();
        //AbilityTracker.Instance.AddAbility("Burst");
    }

    void ShootMain()
    {
        nextFire = Time.time + fireRate;
        whoosh.Play();


        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);

        var projectileObj = Instantiate(spell, firepoint.position, Quaternion.identity) as GameObject;
        otherAnimator.SetTrigger("Fire");
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firepoint.position).normalized * projectileSpeed;

    }

    public IEnumerator Burst()
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("In burst");
            ShootMain();
            yield return new WaitForSeconds(0.1f);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //animation logic
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        otherAnimator.SetBool("IsWalking", isWalking);
        
        

        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            ShootMain();

        }

       
        
        if (Input.GetKeyDown("b") && AbilityTracker.Instance.HasAbility("Burst") && Time.time > nextBurst)
        {
            Debug.Log("burst!");
            nextBurst = Time.time + burstRate;
            StartCoroutine(Burst());
        }
        else if (Input.GetKeyDown("b"))
        {
            Debug.Log("Can't use burst!");
        }
        

    }
}
