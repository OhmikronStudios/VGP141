using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Player Mechanics
    [Header("Player")]
    [SerializeField] float thrust = 1.0f;
    
    //Items acted upon by the collectables
    MeshRenderer rend;
    [SerializeField] Material material2;
    [SerializeField] ParticleSystem particles;
   
    //inventory HUD items
    [Header ("Inventory HUD")]
    [SerializeField] GameObject inventoryTab;
    [SerializeField] GameObject sparkIMG;
    [SerializeField] GameObject colourIMG;
    [SerializeField] GameObject sizeIMG;

    //Linked List instantiation
    LinkedList<string> inventory = new LinkedList<string>();
    
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();
        particles = GetComponent<ParticleSystem>();
        particles.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ListInventory();
        ActivateItem();
    }

    private void Move()
    {
        float moveSpeed = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.forward * thrust * moveSpeed * Time.deltaTime);
        float sideSpeed = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * thrust * sideSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            LinkedListNode<string> linkedListNode = inventory.AddLast(collision.gameObject.name);
            Destroy(collision.gameObject);
            //print(linkedListNode.Value);

            //toggles for the Inventory HUD
            if (inventory.Contains("ParticlesCube")) { sparkIMG.SetActive(true); }
            if (inventory.Contains("ColourCube")) { colourIMG.SetActive(true); }
            if (inventory.Contains("SizeCube")) { sizeIMG.SetActive(true); }
        }
    }

    private void ListInventory()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //foreach (string i in inventory)  //{Debug.Log(i);}   // - Code is obsolete since the inventory HUD is in place, otherwise this allows console checking of the inventory items.
            inventoryTab.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            inventoryTab.SetActive(false);
        }
    }

    private void ActivateItem()   //Uses the same key press to activate the first item in the inventory
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (inventory.First.Value)
            {
                case "ColourCube":
                    ChangeColour();
                    inventory.RemoveFirst();
                    break;
                case "SizeCube":
                    ChangeSize();
                    inventory.RemoveFirst();
                    break;
                case "ParticlesCube":
                    SpawnParticles();
                    inventory.RemoveFirst();
                    break;
            }
        }
    }

    private void ChangeColour()
    {
        rend.material = material2;
        colourIMG.SetActive(false); 
    }

    private void ChangeSize()
    {
        transform.localScale = transform.localScale * 2;
        transform.Translate(0,1.5f,0);
        sizeIMG.SetActive(false);
    }

    private void SpawnParticles()
    {
        particles.Play();
        sparkIMG.SetActive(false);
    }

    

}


        
        
      
        
