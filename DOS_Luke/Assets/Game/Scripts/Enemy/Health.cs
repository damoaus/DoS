using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public float health, maxhealth;
    private Image Healthbar2;
    bool Destroyed = false;
    public float goldAmount;
    public float basedamage;
    public GameObject ExplosionPrefab;

    private  ParticleSystem ExplosionParticles;

    public float Health1 { get { return health; } }

    void Awake()
    {

        ExplosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();
        ExplosionParticles.gameObject.SetActive(false);
    }

    void Start()
    {
        //Find the current health image (Redbar)
        Healthbar2 = transform.FindChild("Canvas").FindChild("maxHealth").FindChild("currentHealth").GetComponent<Image>();
    }

    /// <summary>
    /// Takes a int Damage and removes it from current Health and changes to current health image to display a percent of max health.
    /// If becomes (health =< 0 ) then gameobject is destoryed
    /// </summary>
    /// <param name="damage"></param>
    public void hit(float damage)
    {
       
        if (health > 0)
        {
            health -= damage;
            Healthbar2.fillAmount = (float)health / (float)maxhealth;
            Destroyed = false;
        }
        
        if (health <= 0)
        {
            if (!Destroyed)
            {

                Gobal.Gold += goldAmount;

                ExplosionParticles.transform.position = transform.position;
                ExplosionParticles.gameObject.SetActive(true);

                ExplosionParticles.Play();

                
                Destroy(ExplosionParticles.gameObject, ExplosionParticles.duration);
                Destroy(transform.parent.gameObject);
                Destroyed = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        // if gameobject reaches the base remove gobalhealth and destory gameobject
        if (other.gameObject.tag == "Base")
        {
            Gobal.Heath -= basedamage;
            Destroy(transform.parent.gameObject);
        }
    }
}
