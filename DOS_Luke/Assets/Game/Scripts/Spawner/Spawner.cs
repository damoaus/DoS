using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct Wave
    {
        public float SpawnInterval;
        public int[] enemies;
    }

    //public float SpawnInterval;
    // flipping Enemies
    public GameObject MinionG1;
    public GameObject MinionG2;
    public GameObject MinionG3;
    public GameObject MinionG4;

    GameObject SpawnStart;

    public GameObject WaveSkip;
    GameObject MiddleUI;
    public Wave[] waveNo;
    int[] wavestyle;
    int[] totalEnemiesInLvl;
    int groupNumber = 0;

    public int enemyMinion;
    public int currentPoint = 0;
    bool Startwave = true;
    public bool CompletedLevel = false;
    bool wavefinished = false;
    float BonusGold = 20;

    void Start()
    {
        SpawnStart = gameObject.GetComponent<Gobal>().Pathway[0];
        wavestyle = new int[waveNo[currentPoint].enemies.Length];
        waveNo[currentPoint].enemies.CopyTo(wavestyle, 0);
        MiddleUI = gameObject.GetComponent<Gobal>().MiddleUI;
    }
    // Update is called once per frame
    void Update()
    {
        if (Gobal.myState == Gobal.gameState.Playing)
        {
            if (wavefinished)
            {
                if (currentPoint == waveNo.Length - 1)
                {
                    if (!GameObject.FindGameObjectWithTag("Enemy"))
                    {
                        MiddleUI.SetActive(true);
                        MiddleUI.GetComponent<Text>().text = "Waves Cleared";
                    }
                }
            }

            // timer on spawning

            if (Startwave)
            {
                StartCoroutine(unitwaitspawn());
                Startwave = false;

            }

            if (enemyMinion >= waveNo[currentPoint].enemies.Length)
            {
                if (currentPoint != waveNo.Length - 1)
                {
                    WaveSkip.SetActive(true);
                    WaveSkip.GetComponent<Text>().text = "Start Next Wave";
                }
                else
                {
                    
                    WaveSkip.SetActive(false);
                  
                }
            }
            else
            {
                WaveSkip.SetActive(false);
            }

            if (Input.GetKeyUp(KeyCode.Return))
            {
                if (currentPoint < waveNo.Length)
                {
                    if (enemyMinion >= waveNo[currentPoint].enemies.Length)
                    {

                        // changes the wave when click middle mouse
                        //if (!GameObject.FindGameObjectWithTag("Enemy"))
                        //{
                            Startwave = true;

                            if (currentPoint != waveNo.Length - 1)
                            {
                                wavefinished = false;
                                currentPoint++;
                                wavestyle = new int[waveNo[currentPoint].enemies.Length];
                                waveNo[currentPoint].enemies.CopyTo(wavestyle, 0);
                                enemyMinion = 0;
                                Gobal.Gold += BonusGold;
                                BonusGold += 10;
                            }
                            else
                            {
                                currentPoint++;
                                CompletedLevel = true;
                                Startwave = false;


                            }
                        //}
                        


                    }
                 
                }
               


            }


        }

    }

    IEnumerator unitwaitspawn()
    {
        
        for (int j = 0; j < wavestyle.Length; j++)
        {
            groupNumber = wavestyle[j];
            switch (groupNumber)
            {
                case 0:


                    yield return new WaitForSeconds(waveNo[currentPoint].SpawnInterval);

                    Instantiate(MinionG1, SpawnStart.transform.position, Quaternion.identity);
                    enemyMinion++;


                    break;

                case 1:

                    yield return new WaitForSeconds(waveNo[currentPoint].SpawnInterval);

                    Instantiate(MinionG2, SpawnStart.transform.position, Quaternion.identity);
                    enemyMinion++;


                    break;

                case 2:

                    yield return new WaitForSeconds(waveNo[currentPoint].SpawnInterval);

                    Instantiate(MinionG3, SpawnStart.transform.position, Quaternion.identity);
                    enemyMinion++;


                    break;

                case 3:

                    yield return new WaitForSeconds(waveNo[currentPoint].SpawnInterval);

                    Instantiate(MinionG4, SpawnStart.transform.position, Quaternion.identity);
                    enemyMinion++;


                    break;
            }
            

        }

        wavefinished = true;



    }



}