using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement instance;
    public Rigidbody2D theRigidbody;

    public float moveSpeed;
    public Animator animator;

    public float pickupRange = 1.5f;


    public List<Weapon> unnassignedWeapons, assignedWeapons;

    public int maxWeapons = 3;

    [HideInInspector]
    public List<Weapon> fullyLevelweapons = new List<Weapon>();


    [SerializeField] private InputActionReference moveActionToUse;
    private void Awake()
    {
        instance = this;

        theRigidbody = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }



    void Start()
    {
        if (assignedWeapons.Count == 0)
        {
            AddWeapon(Random.Range(0, unnassignedWeapons.Count));
        }

        moveSpeed = PlayerStatController.instance.moveSpeed[0].value;
        pickupRange = PlayerStatController.instance.pickUpRange[0].value;
        maxWeapons = Mathf.RoundToInt(PlayerStatController.instance.maxWeapons[0].value);


    }


    void Update()
    {


        Vector2 moveDirection = moveActionToUse.action.ReadValue<Vector2>();

        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        Vector2 finalDirection = moveDirection + new Vector2(moveInput.x, moveInput.y);
        finalDirection.Normalize();

        theRigidbody.velocity = finalDirection * moveSpeed;

        if (finalDirection != Vector2.zero)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }
    }

    public void AddWeapon(int weaponNumber)
    {
        if (weaponNumber < unnassignedWeapons.Count)
        {
            assignedWeapons.Add(unnassignedWeapons[weaponNumber]);

            unnassignedWeapons[weaponNumber].gameObject.SetActive(true);

            unnassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);

        assignedWeapons.Add(weaponToAdd);
        unnassignedWeapons.Remove(weaponToAdd);
    }

}


