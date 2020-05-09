
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    Queue<Pizza> queue = new Queue<Pizza>();
    bool baking;
    Animator anim;
    float pizzaCount = 0.0f;

    [SerializeField] GameObject cheesePizza;
    [SerializeField] GameObject pepPizza;
    [SerializeField] GameObject supPizza;
    [SerializeField] Transform pizzaPlate;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Bake()
    {
        anim.SetBool("Baking", true);
        baking = true;
        while (queue.Count > 0) 
        {
            Pizza pizza = queue.Dequeue();
            Debug.Log("pizza is in the oven");
            yield return new WaitForSeconds(pizza.getBakingTime());
            Debug.Log(pizza.getType() + " pizza was baked successfully");
            switch (pizza.getType())
            {
                case "Cheese": spawnCheesePizza(); break;
                case "Pepperoni": spawnPepPizza(); break;
                case "Supreme": spawnSupPizza();   break;
            }
            pizzaCount +=1;
        }
        anim.SetBool("Baking", false);
        baking = false;
    }

    public void AddCheesePizza()
    {
        queue.Enqueue(new Pizza("Cheese", 2.0f));
        if (!baking)  { StartCoroutine("Bake"); }
    }
    public void AddPepperoniPizza()
    {
        queue.Enqueue(new Pizza("Pepperoni", 4.0f));
        if (!baking) { StartCoroutine("Bake"); }
    }
    public void AddSupremePizza()
    {
        queue.Enqueue(new Pizza("Supreme", 8.0f));
        if (!baking) { StartCoroutine("Bake"); }
    }

    void spawnCheesePizza()
    {
        GameObject pizza = Instantiate(cheesePizza, pizzaPlate);
        pizza.transform.position = new Vector2(pizzaPlate.transform.position.x, pizzaPlate.transform.position.y + (pizzaCount*0.1f));
    }
    void spawnPepPizza()
    {
        GameObject pizza = Instantiate(pepPizza, pizzaPlate);
        pizza.transform.position = new Vector2(pizzaPlate.transform.position.x, pizzaPlate.transform.position.y + (pizzaCount * 0.1f));
    }
    void spawnSupPizza()
    {
        GameObject pizza = Instantiate(supPizza, pizzaPlate);
        pizza.transform.position = new Vector2(pizzaPlate.transform.position.x, pizzaPlate.transform.position.y + (pizzaCount * 0.1f));
    }
}
