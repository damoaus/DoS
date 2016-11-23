using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Go_to_tutorial : MonoBehaviour
{


    Color Original;
    bool hasEntered;
    GameObject Manager;



    public void Start()
    {

        // find GameManager Object
        if (!gameObject.CompareTag("MrManager"))
        {
            Original = new Color(.14f, .19f, .23f, 1);
            GetComponent<Image>().color = Original;

        }

        Manager = GameObject.FindGameObjectWithTag("MrManager");


    }

    void OnEnable()
    {
        if (!gameObject.CompareTag("MrManager"))
        {
            Original = new Color(.14f, .19f, .23f, 1);
            GetComponent<Image>().color = Original;
        }
    }

    /// <summary>
    /// Load Tutorial though Loadingscreen
    /// </summary>
    public void LukeWearsHats()
    {
        if (hasEntered)
        {
            Gobal.Heath = 100;
            Gobal.Gold = 100;
            Manager.GetComponent<Loadingscreen>().Nextscene = true;
            Manager.GetComponent<Loadingscreen>().loadlevel = "Tutorial";
        }

    }

    /// <summary>
    /// Load Meain Menu though Loadingscreen when mouse is over the button
    /// </summary>
    public void Main_menu()
    {
        if (hasEntered)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main_menu");

            //with Loading screen
            //Manager.GetComponent<Loadingscreen>().Nextscene = true;
            //Manager.GetComponent<Loadingscreen>().loadlevel = "Main_menu";

        }
    }
    /// <summary>
    /// Used for Hover on button. Changes the buttons colour and the mouse is over the button
    /// </summary>
    public void ChangeColor()
    {
        Text Amount;
        hasEntered = true;
        GetComponent<Image>().color = new Color(.53f, .53f, 53f, 1);
        GameObject TowerDes = Manager.GetComponent<Gobal>().TowerDescription;
        if (GetComponent<cost>() != null)
        {
            int TowerNo = GetComponent<cost>().tower;

            
            int amount = Manager.GetComponent<CreateTurret>().Towerinfo(TowerNo);
            float Ypos = transform.position.y;

            TowerDes.GetComponent<RectTransform>().anchoredPosition = new Vector3(-223, Ypos, 0);

            switch (TowerNo)
            {
                case 0:
                    TowerDes.transform.FindChild("Amount").GetComponent<Text>().text = amount.ToString();
                    TowerDes.transform.FindChild("Description").GetComponent<Text>().text = "Shoots two bullets at a single enemy for low amount of damage.";
                    TowerDes.transform.FindChild("Tower Name").GetComponent<Text>().text = " Hydra";

                    Amount = TowerDes.transform.FindChild("Amount").GetComponent<Text>();
                    if (amount > Gobal.Gold)
                    {
                        Amount.color = Color.red;


                    }
                    else
                    {
                        Amount.color = Color.yellow;

                    }
                    break;

                case 1:
                    TowerDes.transform.FindChild("Amount").GetComponent<Text>().text = amount.ToString();
                    TowerDes.transform.FindChild("Description").GetComponent<Text>().text = "Big balls flys onto enemies killing them and their friends around them. What more could you want.";
                    TowerDes.transform.FindChild("Tower Name").GetComponent<Text>().text = "Launch dem Balls";
                    Amount = TowerDes.transform.FindChild("Amount").GetComponent<Text>();
                    if (amount > Gobal.Gold)
                    {
                        Amount.color = Color.red;


                    }
                    else
                    {
                        Amount.color = Color.yellow;

                    }
                    break;

                case 2:
                    TowerDes.transform.FindChild("Amount").GetComponent<Text>().text = amount.ToString();
                    TowerDes.transform.FindChild("Description").GetComponent<Text>().text = "Wait that doesn't seem right. Telsa! Tesla it's a tesla coil made by ... Nikola not nick. Silly Foriegn names. This Tower does damage to all enemies in its radius.";
                    TowerDes.transform.FindChild("Tower Name").GetComponent<Text>().text = "Nicholas";
                     Amount = TowerDes.transform.FindChild("Amount").GetComponent<Text>();
                    if (amount > Gobal.Gold)
                    {
                        Amount.color = Color.red;


                    }
                    else
                    {
                        Amount.color = Color.yellow;

                    }
                    break;
                case 3:
                    
                    TowerDes.transform.FindChild("Amount").GetComponent<Text>().text = amount.ToString();
                    TowerDes.transform.FindChild("Description").GetComponent<Text>().text = "This Tower has a large radius and does high amounts of damage. ";
                    TowerDes.transform.FindChild("Tower Name").GetComponent<Text>().text = "No Scope";
                     Amount = TowerDes.transform.FindChild("Amount").GetComponent<Text>();
                    if (amount > Gobal.Gold)
                    {
                        Amount.color = Color.red;


                    }
                    else
                    {
                        Amount.color = Color.yellow;

                    }
                    break;
            }

            TowerDes.SetActive(true);

        }

    }

    /// <summary>
    /// Used for HoverExit on button. Changes the buttons colour to original colour and the mouse is not over the button
    /// </summary>
    public void OriginalColor()
    {
        GetComponent<Image>().color = Original;
        hasEntered = false;
        GameObject TowerDes = Manager.GetComponent<Gobal>().TowerDescription;
        TowerDes.SetActive(false);
    }

    /// <summary>
    /// Only work is mouse is over the button. Removes pause screen and sets gamestate to playing
    /// </summary>
    public void Resume()
    {
        if (hasEntered)
        {

            transform.parent.parent.gameObject.SetActive(false);
            if (Manager.GetComponent<Gobal>().fromStart)
            {
                Time.timeScale = 1;
                Gobal.myState = Gobal.gameState.Start;
            }
            else
            {
                Time.timeScale = 1;
                Gobal.myState = Gobal.gameState.Playing;
            }
        }
    }
    /// <summary>
    /// quits the Game
    /// </summary>
    public void GameQuit()
    {
        if (hasEntered)
        {
            Application.Quit();
        }
    }

    public void TowerInfo()
    {
        if (hasEntered)
        {
            Manager.GetComponent<MenuManager>().Towerinfo.SetActive(true);
        }
    }

    public void Back()
    {
        if (hasEntered)
        {
            GetComponent<Image>().color = Original;
            Manager.GetComponent<MenuManager>().Towerinfo.SetActive(false);
        }
    }
}
