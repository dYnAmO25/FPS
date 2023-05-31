using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] ParticleSystem shootParticle;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject BulletParticle;
    [SerializeField] GameObject barrel;
    [SerializeField] float fResetTime;
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource sourceHit;
    [SerializeField] GameObject hitMarker;
    [SerializeField] float fHitmarkerTime;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject Player2;


    private Animation ani;

    private bool bJustShoot;

    void Start()
    {
        ani = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.activeSelf)
        {
            if (!Player.GetComponent<Movement>().bEscapeMenu)
            {
                if (!bJustShoot)
                {
                    Shooting();
                }
            }
        }
        else if (Player2.activeSelf)
        {
            if (!Player.GetComponent<NewMovement>().bEscapeMenu)
            {
                if (!bJustShoot)
                {
                    Shooting();
                }
            }
        }
        
    }

    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;

            Physics.Raycast(cam.transform.position + cam.transform.forward, -transform.forward, out hit, Mathf.Infinity);

            GameObject go = Instantiate(BulletParticle, barrel.transform.position, Quaternion.identity);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Enemy")
                {
                    sourceHit.Play();
                    hitMarker.SetActive(true);
                    Invoke("ResetHitmarker", fHitmarkerTime);
                    Destroy(hit.collider.gameObject);
                }
                
                
                go.GetComponent<ShootEffect>().destination = hit.point;
            }
            else
            {
                go.GetComponent<ShootEffect>().destination = -transform.forward * 100;
            }
            
            ani.Play();
            shootParticle.Play();
            source.Play();

            bJustShoot = true;

            Invoke("ResetShot", fResetTime);
        }
    }

    private void ResetShot()
    {
        bJustShoot = false;
    }

    private void ResetHitmarker()
    {
        hitMarker.SetActive(false);
    }
}
