using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour
{

    public float power;
    public GameObject Explosion;
    private ParticleSystem ExplosionParticles;
    int layer_Enemy;

    void Awake()
    {
        if (Explosion != null)
        {
            ExplosionParticles = Instantiate(Explosion).GetComponent<ParticleSystem>();
            ExplosionParticles.gameObject.SetActive(false);
            layer_Enemy = LayerMask.GetMask("Enemy");
        }
    }

    // Use on bullets to set the amount of damage it does to enemy
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (Explosion == null)
            {
                other.gameObject.GetComponent<Health>().hit(power);
                Destroy(gameObject);
            }
            else
            {
                ExplosionParticles.transform.position = transform.position;
                ExplosionParticles.gameObject.SetActive(true);

                ExplosionDamage(transform.position, 10f, layer_Enemy);
                ExplosionDamage(transform.position, 2f, layer_Enemy);

                ExplosionParticles.Play();
                Destroy(ExplosionParticles.gameObject, ExplosionParticles.duration);
                Destroy(gameObject);
            }
        }
        if (other.gameObject.tag == "Terrain")
        {
            ExplosionParticles.transform.position = transform.position;
            ExplosionParticles.gameObject.SetActive(true);

            ExplosionDamage(transform.position, 10f, layer_Enemy);
            ExplosionDamage(transform.position, 2f, layer_Enemy);

            ExplosionParticles.Play();
            Destroy(ExplosionParticles.gameObject, ExplosionParticles.duration);
            Destroy(gameObject);

        }
    }


    void ExplosionDamage(Vector3 center, float radius, int layer)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, layer);
        int i = 0;
        while (i < hitColliders.Length)
        {

            hitColliders[i].GetComponent<Health>().hit(power / 2);
            i++;
        }
    }
}
