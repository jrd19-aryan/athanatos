using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class playermovement : MonoBehaviour
{
    CharacterController controller;

    public int levelno;
    public float explosionForce; 
    public float speed;
    public float blastradius;
    public float killdelay;
    public GameObject cam;
    public GameObject explosioneffect;
    public NavMeshSurface surf;
    public GameObject collectible;
    public int collectcount;
    public int timesdied;
    public GameObject countdisp;
    public GameObject JoyLeft;
    public GameObject JoyRight;
    public camerashake cshake;

    private SettingsScr settingSet;
    private Joystick joystick;
    private JoybuttonScript Joybutton;

    public Vector3 spawnpos;
    float initfov;
    bool hit;
    float timer;
    bool exploded;
    Rigidbody rb;
    float actspeed;
    

    public float MX;
    float MY;
    public float MZ;
    float movex;
    float movez;

    AudioManager soundplayer;


    void Start()
    {
        settingSet = GameObject.Find("Settings").GetComponent<SettingsScr>();
        speed += 3*settingSet.GameSpeed;
        if(settingSet.JoyOnLeft)
        {
            JoyRight.SetActive(false);
            joystick = JoyLeft.transform.GetChild(0).GetComponent<Joystick>();
            Joybutton = JoyLeft.transform.GetChild(1).GetComponent<JoybuttonScript>();
        }
        else
        {
            JoyLeft.SetActive(false);
            joystick = JoyRight.transform.GetChild(0).GetComponent<Joystick>();
            Joybutton = JoyRight.transform.GetChild(1).GetComponent<JoybuttonScript>();
        }

        soundplayer = FindObjectOfType<AudioManager>();
        timer = killdelay;
        controller = GetComponent<CharacterController>();
        spawnpos = gameObject.transform.position;
        initfov = cam.GetComponent<Camera>().fieldOfView;
        hit = false;
        exploded = false;
        actspeed = speed;

        rb = GetComponent<Rigidbody>();
        
        if(levelno == 1)
        {
            collectcount = GameObject.Find("ScoreManagement").GetComponent<ScoreManagement>().lvl1StartCrates;
            countdisp.GetComponent<TextMeshProUGUI>().text = collectcount.ToString();
        }
        if (levelno != 1)
        {
            collectcount = GameObject.Find("ScoreManagement").GetComponent<ScoreManagement>().currCrates;
            countdisp.GetComponent<TextMeshProUGUI>().text = collectcount.ToString();
        }

        timesdied = GameObject.Find("ScoreManagement").GetComponent<ScoreManagement>().timesplayerdied;
    }

    IEnumerator killplayer()
    {
        timesdied++;
        cshake.shakecamera();
        
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            cam.GetComponent<Camera>().fieldOfView -= 10 * Time.deltaTime;
            yield return null;
        }

        gameObject.transform.position = spawnpos;
        surf.BuildNavMesh();
        cam.GetComponent<Camera>().fieldOfView = initfov;
        hit = false;
        exploded = false;
        timer = killdelay;
        actspeed = speed;
    }

    private void explode()
    {
        soundplayer.Play("explosion");

        Vector3 currpos = transform.position;

        Instantiate(explosioneffect, currpos, transform.rotation);

        Collider[] nearby = Physics.OverlapSphere(currpos, blastradius);

        foreach (Collider obj in nearby)
        {
            if (obj.CompareTag("enemy"))
            {
                obj.GetComponent<enemymove>().kill();
            }

            if(obj.CompareTag("wall"))
            {
                obj.GetComponent<dividewall>().divide();
            }
        }

        nearby = Physics.OverlapSphere(currpos, blastradius);

        foreach (Collider obj in nearby)
        {
            if (obj.CompareTag("deadenemy"))
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, currpos, blastradius);
                }
            }
            if (obj.CompareTag("dividewall"))
            {
                obj.GetComponent<destroywall>().Dest();
            }
        }

        Vector3 spawnpos;
        float r = 1.5f;
        for (int i=0;i<collectcount;i++)
        {
            spawnpos.x = currpos.x + Random.Range(-r, r);
            spawnpos.y = currpos.y + 2;
            spawnpos.z = currpos.z + Random.Range(-r, r);

            Instantiate(collectible, spawnpos, transform.rotation);
        }
        collectcount = 0;
        countdisp.GetComponent<TextMeshProUGUI>().text = collectcount.ToString();

        nearby = Physics.OverlapSphere(currpos, blastradius);

        foreach (Collider obj in nearby)
        {
            if (obj.CompareTag("brokenwall"))
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, currpos, blastradius/2);
                }
            }
            if (obj.CompareTag("collectible"))
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce/2, currpos, blastradius);
                }
            }
        }
    }

    private void Update()
    {
        if(transform.position.y == spawnpos.y)
        {
            MY = 0;
        }
        else
        {
            MY = -10;
        }

        //movex = Input.GetAxis("Horizontal");
        //movez = Input.GetAxis("Vertical");

        movex = joystick.Horizontal;
        movez = joystick.Vertical;

        if(movex==0 && movez==0)
        {
            MX = 0;
            MZ = 0;
        }
        else
        {
            MX = movex / (Mathf.Sqrt(movex * movex + movez * movez));
            MZ = movez / (Mathf.Sqrt(movex * movex + movez * movez));
        }
        

        Vector3 movement = new Vector3(MX, MY*Time.deltaTime, MZ);

        //rb.AddForce(movement * speed);
        controller.Move(movement * actspeed * Time.deltaTime);

        if(Joybutton.Pressed && exploded == false)
        {
            exploded = true;
            actspeed = 0;
            explode();

            if (hit == false)
            {
                StartCoroutine(killplayer());
            }
            else
            {
                timer += 1;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("enemy") && exploded == false)
        {
            soundplayer.Play("dead");
            Debug.Log("dead");
            if(hit == false)
            {
                hit = true;
                StartCoroutine(killplayer());
            }
        }

        if (other.gameObject.CompareTag("finish"))
        {
            Debug.Log("finish");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("collectible"))
        {
            soundplayer.Play("pickup");
            Debug.Log("pickup");
            Destroy(other.gameObject);

            collectcount++;
            countdisp.GetComponent<TextMeshProUGUI>().text = collectcount.ToString();
            Debug.Log(collectcount);
        }
    }
}
