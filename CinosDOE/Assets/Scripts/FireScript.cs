using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using MilkShake;

public class FireScript : MonoBehaviour
{
    
    public Camera cam;
    public GameObject player;
    public GameObject Cooldowns;
    public ShakePreset ShakePreset;
    private Shaker camShaker;
    private AudioSource whoosh;
    private PlayerStats playerStats;
    private PlayerController playerController;
    public GameObject spell;
    public GameObject freeze;
    public GameObject AOEeffect;
    public Transform firepoint;
    public float projectileSpeed = 30;
    private Vector3 destination;
    public GameObject wand;
    Animator otherAnimator;
    Animator cameraAnimator;
    public float fireRate = 0.1f;
    public float burstRate = 5.0f;
    public float AOERate = 7.0f;
    public float freezeCooldown = 7.0f;
    public float swingRate;
    private float nextFire = 0.0f;
    private float nextBurst = 0.0f;
    private float nextSwing = 0.0f;
    private float nextAOE = 0.0f;
    private float nextFreeze = 0.0f;
    private float movementCooldown = 0.0f;
    private float explosionTime = 0.0f;
    private float explCooldown = 0f;
    private bool inAOE = false;
    private Text burstText;
    private Text AOEText;
    private Text FreezeText;
    private bool burstTextSelected = false;
    private bool aoeTextSelected = false;
    private bool freezeTextSelected = false;
    

   


    // Start is called before the first frame update
    void Start()
    {
        otherAnimator = wand.GetComponent<Animator>();
        cameraAnimator = cam.GetComponent<Animator>();
        whoosh = wand.GetComponent<AudioSource>();
        playerStats = player.GetComponent<PlayerStats>();
        playerController = player.GetComponent<PlayerController>();
        camShaker = cam.GetComponent<Shaker>();
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

    void Melee()
    {
        nextSwing = Time.time + swingRate;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        whoosh.Play();
        otherAnimator.SetTrigger("Swing");

        if (Physics.Raycast(ray, out hit, 8))
        {
            if(hit.collider.tag == "Enemy" || hit.collider.transform.parent.gameObject.tag == "Enemy" )
            {
                Enemy enemy = hit.collider.gameObject.transform.GetComponent<Enemy>();
                if(enemy == null)
                {
                    enemy = hit.collider.transform.parent.GetComponent<Enemy>();
                }
                enemy.TakeDamage(50);
                Vector3 dir = firepoint.position - enemy.transform.position;
                dir.Normalize();
                dir.x = dir.x * -2;
                dir.z = dir.z * -2;
                enemy.GetComponent<Rigidbody>().AddForce((dir)* 500000);
            }
        }
    }

    void FreezeAbility()
    {
        whoosh.Play();

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            destination = hit.point;
        else
            destination = ray.GetPoint(1000);

        otherAnimator.SetTrigger("Fire");
        var projectileObj = Instantiate(freeze, firepoint.position, Quaternion.identity) as GameObject;
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
        
        

        if (Input.GetButtonDown("Fire1") && Time.time > nextSwing)
        {
            
            Melee();
            
        }

        if (Input.GetButtonDown("Fire2") && Time.time > nextFire)
        {
            if (playerStats.mana >= 20) 
            { 
                ShootMain();
                playerStats.SpendMana(20);
            }
            
        }



        if (Input.GetKeyDown("b") && AbilityTracker.Instance.HasAbility("Burst") && Time.time > nextBurst)
        {
            if (playerStats.mana >= 50)
            {
                Debug.Log("burst!");
                playerStats.SpendMana(50);
                nextBurst = Time.time + burstRate;
                StartCoroutine(Burst());
            }
        }

        else if (Input.GetKeyDown("b"))
        {
            Debug.Log("Can't use burst!");
        }

        //This is the AOE ability code
        if (Input.GetKeyDown("q") && AbilityTracker.Instance.HasAbility("AOE") && Time.time > nextAOE && playerController.isGrounded)
        {
            if (playerStats.mana >= 50)
            {
                nextAOE = Time.time + AOERate;
                playerStats.SpendMana(50);
                otherAnimator.SetTrigger("AOE");
                cameraAnimator.SetTrigger("AOE");
                explosionTime = Time.time + 0.75f;
                playerController.MovementSpeed = 0f;
                movementCooldown = Time.time + 1f;
                inAOE = true;
            }
            

        }
        if (Time.time >= explosionTime && inAOE)
        {

            camShaker.Shake(ShakePreset);
            if (Time.time >= explCooldown)
            {
                var aoeexpl = Instantiate(AOEeffect, player.transform.position, Quaternion.identity) as GameObject;
                Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, 10);
                foreach (var collider in hitColliders)
                {
                    if (collider.tag == "Enemy")
                    {
                        Enemy enemy = collider.gameObject.transform.GetComponent<Enemy>();
                        enemy.TakeDamage(50);
                        Vector3 dir = firepoint.position - enemy.transform.position;
                        dir.Normalize();
                        dir.x = dir.x * -2;
                        dir.z = dir.z * -2;
                        enemy.GetComponent<Rigidbody>().AddForce((dir) * 500000);
                    }
                }
            }
            explCooldown = Time.time + 1f;
           
        }

        if (Time.time >= movementCooldown && inAOE)
        {
            playerController.MovementSpeed = 10f;
            inAOE = false;
            explCooldown = 0f;
        }
        //AOE ability code ends here

        //cooldown displays
        if (nextBurst - Time.time > 0)
        {
            if (!burstTextSelected)
            {
                foreach (Transform child in Cooldowns.transform)
                {
                    
                    if (child.GetComponent<Text>().text == "")
                    {
                        burstText = child.GetComponent<Text>();
                        burstTextSelected = true;
                        break;
                    }

                }
            }
            float burstCooldown = (nextBurst - Time.time);
            burstText.text = "Burst cooldown: " + Math.Round(burstCooldown, 2).ToString();
            if (burstCooldown <= 0.1)
            {
                burstText.text = "";
                burstTextSelected = false;

            }
        }

        if (nextAOE - Time.time > 0)
        {
            if (!aoeTextSelected)
            {
                foreach (Transform child in Cooldowns.transform)
                {
                    
                    if (child.GetComponent<Text>().text == "")
                    {
                        AOEText = child.GetComponent<Text>();
                        aoeTextSelected = true;
                        break;
                    }

                }
            }
            float aoeCooldown = (nextAOE - Time.time);
            AOEText.text = "AOE cooldown: " + Math.Round(aoeCooldown, 2).ToString();
            if (aoeCooldown <= 0.1)
            {
                AOEText.text = "";
                aoeTextSelected = false;

            }
        }

        if (nextFreeze - Time.time > 0)
        {
            if (!freezeTextSelected)
            {
                foreach (Transform child in Cooldowns.transform)
                {

                    if (child.GetComponent<Text>().text == "")
                    {
                        FreezeText = child.GetComponent<Text>();
                        freezeTextSelected = true;
                        break;
                    }

                }
            }
            float freezeCD = (nextFreeze - Time.time);
            FreezeText.text = "Freeze cooldown: " + Math.Round(freezeCD, 2).ToString();
            if (freezeCD <= 0.1)
            {
                FreezeText.text = "";
                freezeTextSelected = false;

            }
        }
        //end of cooldown displays

        //if (Input.GetKeyDown("r") && AbilityTracker.Instance.HasAbility("Freeze") && Time.time > nextFreeze)
        //if (Input.GetKeyDown("r") && Time.time > nextFreeze)
        if (Input.GetKeyDown("r") && AbilityTracker.Instance.HasAbility("Freeze") && Time.time > nextFreeze)
        {
            if (playerStats.mana >= 20)
            {
                FreezeAbility();
                nextFreeze = Time.time + freezeCooldown;
                playerStats.SpendMana(20);
            }

        }

    }

}
