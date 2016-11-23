using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cost : MonoBehaviour
{

    public int tower;
    int amount;
    public Text Amount;
    GameObject manager;
    Color Original;


    void Start()
    {
        //find gamemanger
        manager = GameObject.FindGameObjectWithTag("MrManager");
        // Gets cost of tower and sets it to Text UI
        getAmount(tower);
        Amount.text = amount.ToString();
        Original = GetComponent<Image>().color;
    }

    void Update()
    {

        // if the tower cost more then current gold amount change color to red. else change to gold
        if (amount > Gobal.Gold)
        {
            Amount.color = Color.red;
            //GetComponent<Image>().color = new Color(27, 169, 57);

        }
        else
        {
            Amount.color = Color.yellow;
            //GetComponent<Button>().colors.normalColor = Original;

        }
    }
    /// <summary>
    /// Gets the Cost of the Tower from CreateTurret Script
    /// </summary>
    /// <param name="towerNo"></param>
    public void getAmount(int towerNo)
    {
        manager.GetComponent<CreateTurret>().TowerNoCost(tower);
        amount = manager.GetComponent<CreateTurret>().cost;



    }

}
