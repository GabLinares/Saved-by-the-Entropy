using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CarControl : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject cam;
    Computation script;
    GameOverControl gameoverscript;



    public double cp;
    public double carMass = 1.0f;
    public double velx;
    public float[] railposition;
    public int currentrail;
    public int tilenumber;
    public int maxtile;

    public GameObject tileprefab;
    public GameObject tileendprefab;
    public GameObject coinprefab;
    public GameObject[] obstacleprefabs;
    public GameObject ObstacleLiquid;
    public GameObject ObstacleBroken;
    public float tilewidth;

    public int score;
    public int difficulty;

    public float mufriction;
    public float inimufriction;
    public bool MonsterStart;

    public Sprite spritegason;
    public Sprite spritegasoff;

    float oscil;
    int oscilcount;

    float timestart;
    float timeend;
    public int finalscore;


    Rigidbody2D carRb;

    Transform carTransform;

    GameObject gasbutton;
    Image theimagegas;
    AudioSource audiosource;

    SpriteRenderer carImage;
    public Sprite skin0sprite;
    public Sprite skin1sprite;
    public Sprite skin2sprite;
    public Sprite skin3sprite;
    public Sprite skin4sprite;
    public Sprite skin5sprite;

    public AudioClip barrelsound;
    public AudioClip coinsound;
    public AudioClip tombsound;
    public AudioClip monsterstartsound;
    public AudioClip monstereatssound;
    public AudioClip youwinsound;
    public AudioClip youlosesound;
    

    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");
        script = cam.GetComponent<Computation>();
        gameoverscript = cam.GetComponent<GameOverControl>();
        carRb = GetComponent<Rigidbody2D>();
        carTransform = GetComponent<Transform>();
        currentrail = 1;
        tilenumber = 1;
        tilewidth = 50;
        score = 0;
        finalscore = 0;
        inimufriction = mufriction;
        MonsterStart = false;

        gasbutton = GameObject.FindGameObjectWithTag("GasButton");
        theimagegas = gasbutton.GetComponent<Image>();

        theimagegas.sprite = spritegasoff;

        oscilcount = 0;

        audiosource = GetComponent<AudioSource>();

        carImage = GetComponent<SpriteRenderer>();

        ApplyCurrentSkin();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        double vf = 0;

        velx = carRb.velocity.x;

        if (velx > 0f)
        {

            if (velx > 50f)
            {
                vf = 50f;
            }
            else if (velx > 40f)
            {
                vf = velx - 4f * mufriction * Time.deltaTime;
            }
            else if (velx > 30f)
            {
                vf = velx - 2f * mufriction * Time.deltaTime;
            }
            else if (velx > 20f)
            {
                vf = velx - 1f * mufriction * Time.deltaTime;
            }
            else if (velx > 12f)
            {
                vf = velx - 0.15f * mufriction * Time.deltaTime;
            }
            else
            {
                vf = velx - mufriction * Time.deltaTime;
            }



            if (vf < 0f)
            {
                vf = 0f;
            }
            carRb.velocity = new Vector2(Convert.ToSingle(vf), 0);
        }

        if(script.allowedDelta == true && script.collision > script.collisionlimit)
        {
            theimagegas.sprite = spritegason;
        }
        else if(script.allowedDelta == false)
        {
            theimagegas.sprite = spritegasoff;
        }

        if(oscilcount > 359)
        {
            oscilcount = 0;
        }

        if (velx > 7f)
        {
            float xpos = transform.position.x;
            float zpos = transform.position.z;
            float ypos = transform.position.y;

            oscil = 0.15f*Mathf.Sin(6f*oscilcount*2f*Mathf.PI/360f);

            Vector3 newpos = new Vector3(xpos, railposition[currentrail] + oscil, zpos);

            transform.position = newpos;

            oscilcount++;
        }


    }

    public void GiveImpulse()
    {
        
        double vi = carRb.velocity.x;
        double vf = 0;
        double work = 0;
        double Tfinal = 0;
        //double mu = 0;
        //double var = 0;
        double[] vx = new double[script.N];
        double[] vy = new double[script.N];
        //double a = 0;
        GameObject pref;
        //int maxfila;
        GameObject partic;


        if (script.allowedDelta == true)
        {
            script.ComputeWork(out work, out Tfinal);

            //Debug.Log("alloweddelta");
 


            vf = Math.Sqrt((2 * Math.Abs(work)) / carMass + Math.Pow(vi, 2));

            Debug.Log("Vf: "+vf);

            if(PlayerPrefs.GetInt("MaxSpeed") < Convert.ToInt32(vf))
            {
                PlayerPrefs.SetInt("MaxSpeed", Convert.ToInt32(vf));
                PlayerPrefs.Save();
            }

            carRb.velocity = new Vector2(Convert.ToSingle(vf), 0);

            //var = Math.Sqrt(script.epsilon * Tfinal / script.m);


            for (int i = 0; i < script.N; i++)
            {
                partic = GameObject.Find(i.ToString());
                Destroy(partic);
            }

            /*script.BoxMuller(script.N, var, mu, out vx, out vy);

            for (int i = 0; i < script.N; i++)
            {
                script.v0[i, 0] = vx[i];
                script.v0[i, 1] = vy[i];
            }
            */
            //maxfila = (script.xsize - 2) / script.dist;

            script.ParticleStarter(script.numx, script.numy, script.maxfila, script.dist, out script.r0, out script.v0);

            script.Force(script.r0, out script.f0, out script.V1);



            for (int i = 0; i < script.N; i++)
            {
                pref = Instantiate(script.particleprefab, new Vector2(Convert.ToSingle(script.r0[i, 0]), Convert.ToSingle(script.r0[i, 1])), Quaternion.identity);
                pref.name = i.ToString();
            }

            script.collision = 0;

            script.allowedDelta = false;

            GameObject gasbutton;
            gasbutton = GameObject.FindGameObjectWithTag("GasButton");
            Image theimage = gasbutton.GetComponent<Image>();

            theimage.sprite = spritegasoff;
        }

    }

    public void FalseGiveImpulse()
    {
        double vi = carRb.velocity.x;
        double vf = 0;
        double work = 0;
        double Tfinal = 0;
        //double mu = 0;
        double var = 0;
        double[] vx = new double[script.N];
        double[] vy = new double[script.N];
        //double a = 0;
        GameObject pref;
        //int maxfila;
        GameObject partic;


            script.ComputeWork(out work, out Tfinal);



            vf = 10+Math.Sqrt((2 * Math.Abs(work)) / carMass + Math.Pow(vi, 2));

            carRb.velocity = new Vector2(Convert.ToSingle(vf), 0);

            var = Math.Sqrt(script.epsilon * Tfinal / script.m);


            for (int i = 0; i < script.N; i++)
            {
                partic = GameObject.Find(i.ToString());
                Destroy(partic);
            }

            /*script.BoxMuller(script.N, var, mu, out vx, out vy);

            for (int i = 0; i < script.N; i++)
            {
                script.v0[i, 0] = vx[i];
                script.v0[i, 1] = vy[i];
            }
            maxfila = (script.xsize - 2) / script.dist;*/

            script.ParticleStarter(script.numx, script.numy, script.maxfila, script.dist, out script.r0, out script.v0);

            script.Force(script.r0, out script.f0, out script.V1);



            for (int i = 0; i < script.N; i++)
            {
                pref = Instantiate(script.particleprefab, new Vector2(Convert.ToSingle(script.r0[i, 0]), Convert.ToSingle(script.r0[i, 1])), Quaternion.identity);
                pref.name = i.ToString();
            }

            script.collision = 0;

            //script.allowedDelta = false;
        

    }

    public void RailUp()
    {


        if (currentrail > 0)
        {
            currentrail--;

            float xpos = transform.position.x;
            float zpos = transform.position.z;

            Vector3 newpos = new Vector3(xpos, railposition[currentrail], zpos);

            transform.position = newpos;


        }
    }

    public void RailDown()
    {
        if (currentrail < 2)
        {
            currentrail++;

            float xpos = transform.position.x;
            float zpos = transform.position.z;

            Vector3 newpos = new Vector3(xpos, railposition[currentrail], zpos);

            transform.position = newpos;


        }
    }

    void ChangeScore(int amount)
    {
        score = score + amount;
        //Here we can add more things that could happen when you modify the score, like animations, unlock skins, etc.
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //In this 'if' we spawn the different objects

        if (other.tag == "TileManager" & tilenumber < maxtile)
        {
            Instantiate(coinprefab, new Vector3(tilewidth * tilenumber + 25, railposition[UnityEngine.Random.Range(0, 3)], -2), Quaternion.identity);

            for (int i = 0; i < difficulty; i++)
            {
                Instantiate(obstacleprefabs[UnityEngine.Random.Range(0,obstacleprefabs.Length)], new Vector3(tilewidth * tilenumber + tilewidth/difficulty*(i+1), railposition[UnityEngine.Random.Range(0, 3)],-1.5f),Quaternion.identity);
            }
        }

        if(other.tag == "Coin")
        {
            ChangeScore(100);

            Destroy(other.gameObject);

            audiosource.PlayOneShot(coinsound, 0.6f);
        }
        else if(other.tag == "Obstacle")
        {
            double xvel = carRb.velocity.x * 0.85;


            carRb.velocity = new Vector2(Convert.ToSingle(xvel), 0);

            Instantiate(ObstacleBroken, new Vector3(other.transform.position.x, other.transform.position.y, -1.99f), Quaternion.identity);

            Destroy(other.gameObject);

            audiosource.PlayOneShot(tombsound, 1f);


            if (score -25 >= 0)
            {
                ChangeScore(-25);
            }
            else
            {
                score = 0;

            }
        }
        else if (other.tag == "Obstacle2")
        {
            if (score - 25 >= 0)
            {
                ChangeScore(-25);

            }
            else
            {
                score = 0;

            }
            audiosource.PlayOneShot(barrelsound, 1f);
            Instantiate(ObstacleLiquid, new Vector3(carTransform.position.x,carTransform.position.y,-2.1f), Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.tag == "ObstacleLiquid")
        {
            mufriction = mufriction * 6f;
        }
        //And in this one we control the spawn of the tiles conforming the circuit

        if (other.tag == "TileManager" & tilenumber < (maxtile - 1))
        {
            Instantiate(tileprefab, new Vector2(tilewidth * tilenumber, -100), Quaternion.identity);
            tilenumber++;
            
        }
        else if (other.tag == "TileManager" & tilenumber == (maxtile - 1))
        {

            Instantiate(tileendprefab, new Vector2(tilewidth * tilenumber, -100), Quaternion.identity);

            tilenumber++;

            Instantiate(tileprefab, new Vector2(tilewidth * tilenumber, -100), Quaternion.identity);


        }
        else if (other.tag == "EndTile")
        {
            timeend = Time.time;
            GameOver();
            gameoverscript.YouWin(score, timestart, timeend, out finalscore);

            audiosource.clip = youwinsound;
            audiosource.PlayDelayed(1.5f);

            Debug.Log("Final score: "+finalscore);
            
        }
        else if (other.tag == "MonsterStarter")
        {
            MonsterStart = true;
            Debug.Log("Monster");
            audiosource.PlayOneShot(monsterstartsound, 1f);

        }
        else if (other.tag == "Monster")
        {
            GameOver();
            gameoverscript.GameOver();
            audiosource.PlayOneShot(monstereatssound, 1f);
            audiosource.clip = youlosesound;
            audiosource.PlayDelayed(3f);

        }
        else if (other.tag == "TimeStarter")
        {
            timestart = Time.time;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "ObstacleLiquid")
        {
            mufriction = inimufriction;

        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        MonsterStart = false;
        carRb.velocity = new Vector2(0, 0);
    }

    void ApplyCurrentSkin()
    {
        Sprite[] carsprites = new Sprite[] { skin0sprite, skin1sprite, skin2sprite, skin3sprite, skin4sprite, skin5sprite };

        carImage = GetComponent<SpriteRenderer>();

        carImage.sprite = carsprites[PlayerPrefs.GetInt("CurrentSkin")];
    }
}
