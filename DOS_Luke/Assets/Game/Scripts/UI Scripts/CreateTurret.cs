using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CreateTurret : MonoBehaviour
{

    public GameObject[] temptower;
    private GameObject instemptower;
    public GameObject[] tower;
    public Material red;
    public Material green;
    // Is the tower ready to be placed
    bool canBuild = false;
    Ray mousepos;
    Vector3 towerpos;
    RaycastHit hit;
    public int cost;
    public int TowerNo;
    Transform canvas;

    public int[] Towercost;
    public GameObject TowerInfo;
    GameObject SelectedTower;
    int OldCost;

    bool buttonClicked = false;
    bool UIhit = false;
    bool paid = false;
    int boughtTowerNo;

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            TowerSelect();
        }
        if (canBuild)
        {

            // This places the temp tower on the terrain and updates as you move your mouse
            mousepos = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layer_mask = LayerMask.GetMask("Terrain");
            if (Physics.Raycast(mousepos, out hit, 700, layer_mask))
            {
            }
            towerpos = hit.point;
            Rigidbody instemptowerRB = instemptower.GetComponent<Rigidbody>();
            instemptowerRB.MovePosition(towerpos);

            // Instantiates the Main tower ready to attack
            if (Input.GetButtonDown("Fire1") && instemptower.GetComponentInChildren<collider>().canplace)
            {
                if (instemptower.activeSelf)
                {
                    DestroyImmediate(instemptower, true);
                    Instantiate(tower[boughtTowerNo], towerpos, Quaternion.identity);
                    buttonClicked = false;
                    canBuild = false;
                }
            }
            // Cancels Tower being built and the cost back to current gold
            if (Input.GetButtonDown("Fire2"))
            {
                if (instemptower.activeSelf)
                {


                    Gobal.Gold += cost;
                    DestroyImmediate(instemptower, true);
                    canBuild = false;
                    paid = false;
                    buttonClicked = false;
                }
            }

        }


    }

    private bool canplace()
    {
        // if there isnt instemptower then a tower can be placed
        if (instemptower == null)
        {

            return true;



        }
        else
        {

            return false;
        }
    }

    public void createTurret()
    {
        // Subtracts cost from current Gold and places a temp Tower on the map the follows the mouse 



        if (buttonClicked)
        {

            float TempGold = Gobal.Gold;
            TempGold += OldCost;

            if (cost <= TempGold)
            {
                Gobal.Gold += OldCost;
                Gobal.Gold -= cost;
                paid = true;
                instemptower = null;
                canBuild = false;
            }else
            {
                paid = false;
            }

        }

        if (canplace())

        {
            if (cost <= Gobal.Gold || paid)
            {
                boughtTowerNo = TowerNo;

                if (!paid)
                {
                    Gobal.Gold -= cost;
                }

                mousepos = Camera.main.ScreenPointToRay(Input.mousePosition);

                instemptower = (GameObject)Instantiate(temptower[TowerNo], hit.point, Quaternion.identity);
                instemptower.SetActive(false);
                canBuild = true;
                buttonClicked = true;
                paid = true;
            }
        }



    }


    /// <summary>
    /// if instemptower is active when cursor is over button disable it
    /// </summary>
    public void ButtonHover()
    {
        if (instemptower != null)
        {
            if (instemptower.activeSelf)
            {
                instemptower.SetActive(false);
            }
        }
    }
    /// <summary>
    /// if instemptower is disable but has been instantiate when cursor is remove from button reactive it
    /// </summary>
    public void ButtonHoverexit()
    {
        if (instemptower != null)
        {
            if (!instemptower.activeSelf)
            {
                instemptower.SetActive(true);
            }
        }
    }
    /// <summary>
    /// Determines the cost of the Tower
    /// </summary>
    /// <param name="tower"></param>
    public void TowerNoCost(int tower)
    {

        if (paid)
        {
            OldCost = cost;
        }
        cost = Towercost[tower];
        TowerNo = tower;

    }

    /// <summary>
    /// Determines the cost of the Tower
    /// </summary>
    /// <param name="tower"></param>
    public int Towerinfo(int tower)
    {

        int costMain = Towercost[tower];
        return costMain;



    }

    /// <summary>
    /// Creates a ring around the tower when the user right clicks on the Tower
    /// </summary>
    void TowerSelect()
    {
        Ray mousepos2;
        //if (SelectedTower != null)
        //{
        //    canvas.gameObject.SetActive(false);
        //    TowerInfo.SetActive(false);
        //    SelectedTower = null;
        //}




        mousepos2 = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layer_mask = LayerMask.GetMask("Tower");

        bool Towerhit = (Physics.Raycast(mousepos2, out hit, 700, layer_mask));
        if (Towerhit)
        {


            if (hit.transform.gameObject.tag == "Tower")
            {
                if (canvas != null)
                {
                    SelectedTower = null;
                    canvas.gameObject.SetActive(false);
                    TowerInfo.SetActive(false);
                }
                Transform Tower = hit.transform;
                Transform[] children = Tower.GetComponentsInChildren<Transform>(true);
                foreach (Transform child in children)
                {


                    if (child.CompareTag("WorldUI"))
                    {
                        SelectedTower = hit.transform.gameObject;
                        canvas = child;
                        canvas.gameObject.SetActive(true);
                        TowerInfo.SetActive(true);

                    }

                }


            }

        }
        else
        {

            if (!UIhit)
            {

                if (canvas != null)
                {
                    SelectedTower = null;
                    canvas.gameObject.SetActive(false);
                    TowerInfo.SetActive(false);
                }
            }
        }
    }

    public void SellTower()
    {
        if (SelectedTower != null)
        {
            int towerNum = SelectedTower.GetComponent<TowerNum>().Number;
            Gobal.Gold += Towercost[towerNum] / 2;
            Destroy(SelectedTower);
            TowerInfo.SetActive(false);
        }
    }

    public void OverUI()
    {
        UIhit = true;
    }

    public void LeftUI()
    {
        UIhit = false;
    }
}

