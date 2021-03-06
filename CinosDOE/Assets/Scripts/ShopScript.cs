using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopScript : MonoBehaviour
{
    public GameObject player;
    public GameObject shopMenu;
    public GameObject gameplayMenu;
    public TMP_Text ExperienceAvailable;
    public Button BurstSpellBuyButton;
    public Button AOESpellBuyButton;
    public Button FreezeSpellBuyButton;
    public Text EnterShop;
    public bool inProx = false;
    public bool inShop = false;
    private bool inCombat = false;
    public GameObject cam;
    private MouseMovement mmscript;
    private float hCache;
    private float vCache;
    private float experience;
    



    void Start()
    {
        //cam = Camera.main;
        mmscript = cam.GetComponent<MouseMovement>();

    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && inShop)
        {
            CloseShop();
            inShop = false;
        }

        else if (inProx && Input.GetButtonDown("Interact") && !inShop)
        {
            OpenShop();
            inShop = true;
        }

        if (!inProx && inShop)
        {
            inShop = false;
            CloseShop();
        }
    }
    void OpenShop()
    {
        Debug.Log("OpenShop");
        experience = ExpManager.Instance.GetExperience();
        shopMenu.SetActive(true);
        //ExperienceAvailable = GameObject.Find("TotalExp").GetComponent<Text>();
        ExperienceAvailable.text = "Total Experience: " + experience.ToString();
        gameplayMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        hCache = mmscript.horizontalSpeed;
        vCache = mmscript.verticalSpeed;
        mmscript.horizontalSpeed = 0f;
        mmscript.verticalSpeed = 0f;

    }

    void CloseShop() 
    {
        Debug.Log("ClosingShop");
        shopMenu.SetActive(false);
        gameplayMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        mmscript.horizontalSpeed = hCache;
        mmscript.verticalSpeed = vCache;
    }

    void OnTriggerEnter(Collider other) 
    {

        if (other.gameObject == player && !inCombat) 
        {
            EnterShop.text = "Prese [E] to enter shop.";
            inProx = true;
            Debug.Log("INPROX");
        }    
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            EnterShop.text = "";
            inProx = false;
        }
        
    }

    public void BurstSpellUnlock()
    {
        if (experience < 45)
            Debug.Log("Not Enough");

        else
        {
            BurstSpellBuyButton.interactable = false;
            TMP_Text buttonText = BurstSpellBuyButton.transform.GetChild(1).GetComponent<TMP_Text>();
            buttonText.text = "Purchased!";
            ExpManager.Instance.SubExperience(45);
            experience = ExpManager.Instance.GetExperience();
            ExperienceAvailable.text = "Total Experience: " + experience.ToString();

            AbilityTracker.Instance.AddAbility("Burst");
        }
    }

    public void AOESpellUnlock()
    {
        if (experience < 70)
            Debug.Log("Not enough experience for AOE");
        else
        {
            AOESpellBuyButton.interactable = false;
            TMP_Text buttonText = AOESpellBuyButton.transform.GetChild(1).GetComponent<TMP_Text>();
            buttonText.text = "Purchased!";
            ExpManager.Instance.SubExperience(70);
            experience = ExpManager.Instance.GetExperience();
            ExperienceAvailable.text = "Total Experience: " + experience.ToString();

            AbilityTracker.Instance.AddAbility("AOE");
        }
    }

    public void FreezeSpellUnlock()
    {
        if (experience < 50)
            Debug.Log("Not enough experience for freeze");
        else
        {
            FreezeSpellBuyButton.interactable = false;
            TMP_Text buttonText = FreezeSpellBuyButton.transform.GetChild(1).GetComponent<TMP_Text>();
            buttonText.text = "Purchased!";
            ExpManager.Instance.SubExperience(50);
            experience = ExpManager.Instance.GetExperience();
            ExperienceAvailable.text = "Total Experience: " + experience.ToString();

            AbilityTracker.Instance.AddAbility("Freeze");
        }
    }
}
