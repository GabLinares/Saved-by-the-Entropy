using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Computation : MonoBehaviour
{
    // Temporary values for the number of molecules N and the sigma of the interaction
    public int N;
    public double sigma = 1;
    public double m = 1;
    public double epsilon = 1;

    public double carCp = 1;

    public double[,] r0;
    public double[,] v0;
    public double[,] f0;
    public double[] V1;
    public double dt;
    public int iteration = 0;
    public double collisionlimit;
    public int collision = 0;
    public float initialspeed;
    public int leftcount;
    public int rightcount;
    public bool Tunlock;
    public double totalvr;
    public double Tleft;
    public double Tright;
    public double Et;
    public double Tt;
    public double Vt;
    public double csvtimer;
    public string csvname;

    public int numx = 0;
    public int numy = 0;
    public int maxfila;
    public int dist;
    public int xsize = 50;

    public float minDelta = 0.2f;
    public bool allowedDelta;





    [SerializeField] private Renderer particrend;

    public GameObject particleprefab;
    GameObject pref;


    //The Force function uses the initial position of every particle and returns an Nx2 array with the total forces acting on each one of them
    
    void Start()
    {
        //Those r0 and v0 arrays, containing respectively the positions and velocities of all the molecules, are set to 1 just for test purposes

        r0 = new double[N, 2];
        v0 = new double[N, 2];
        f0 = new double[N, 2];
        V1 = new double[N];
        

        //int ysize = 30;
        int xsize = 50;
        maxfila = (xsize - 2) / dist;
        Tunlock = false;
        csvtimer = 0;
        allowedDelta = false;


        ParticleStarter(numx, numy, maxfila, dist, out r0, out v0);

        Force(r0, out f0, out V1);

        //int nboxmuller = 1000;

        /*BoxMuller(nboxmuller, out x, out y);

        float[] xnums = new float[1];

        for(int i = 0; i < nboxmuller; i++)
        {
            xnums[0] = x[i];
            WriteCSV("normaldist", xnums);
        }*/


        for (int i = 0; i < N; i++)
        {
            pref = Instantiate(particleprefab, new Vector2(Convert.ToSingle(r0[i, 0]), Convert.ToSingle(r0[i, 1])), Quaternion.identity);
            pref.name = i.ToString();
        }
        /*csvname = "test";
        string[] names = { "iteration", "Ekin", "Epot", "Etot" };
        WriteCSV(csvname, names);*/
    }



    void FixedUpdate()
    {
        //Here we fix the time step to 0.1 seconds, but again it won't be its final value, since it relies strongly on 
        // how many times the update function is called (once per every frame).
        double[,] f1 = new double[N, 2];
        double[,] r1 = new double[N, 2];
        double[,] v1 = new double[N, 2];
        GameObject partic;
        double vrleft;
        double vrright;

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                r1[i, j] = r0[i, j] + dt * v0[i, j] + 0.5 * Math.Pow(dt, 2) * f0[i, j] / m;

            }
            partic = GameObject.Find(i.ToString());
            partic.transform.position = new Vector2(Convert.ToSingle(r1[i, 0]), Convert.ToSingle(r1[i, 1]));
        }

        Force(r1, out f1, out V1);



        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                v1[i, j] = v0[i, j] + 0.5 * dt / m * (f1[i, j] + f0[i, j]);
            }
        }

        f0 = f1;
        r0 = r1;
        v0 = v1;

        TempCalc(v0, out Tleft, out Tright, out vrleft, out vrright);
        EnergyCalc(v0, V1, out Vt, out Tt, out Et);

        if ((Math.Abs(Tleft - Tright)/Math.Max(Tleft,Tright) > minDelta) && (collision > collisionlimit))
        {
            allowedDelta = true;
  
        }
        else
        {
            allowedDelta = false;
        }

        /*csvtimer = csvtimer + Time.deltaTime;

        if(csvtimer >= 0.5)
        {
            double[] values = { iteration,Tt, Vt, Et };
            WriteCSV(csvname, values);
            csvtimer = 0;
        }*/

        iteration++;


    }

    public void Force(double[,] r0, out double[,] force, out double[] potential)
    {
        //Here we initialize the arrays storing the position's differences (dr) and their modulus (dr2)
        //Fx and Fy are the variables used to store the net forces applied on each axys, whose values are saved in the force array
        // and reset after every for loop
        double[,,] dr = new double[N, N, 2];
        double[,] dr2 = new double[N, N];
        double Fx;
        double Fy;
        double V;
        force = new double[N, 2];
        potential = new double[N];

        //Here we compute the distance vector between every i j pair of molecules in a NxNx2 array
        for (int i = 0; i < N; i++)
        {
            //Old way: it was without considering the array's simmetry. Left for testing the performance.
            /*for (int j = 0; j < N; j++)
            {
                dr[i, j, 0] = r0[i, 0] - r0[j, 0];
                dr[i, j, 1] = r0[i, 1] - r0[j, 1];
                dr2[i, j] = Math.Pow(dr[i, j, 0], 2) + Math.Pow(dr[i, j, 1], 2);


            }*/

            for (int j = i+1 ; j < N; j++)
            {
                dr[i, j, 0] = r0[i, 0] - r0[j, 0];
                dr[i, j, 1] = r0[i, 1] - r0[j, 1];
                dr2[i, j] = Math.Pow(dr[i, j, 0], 2) + Math.Pow(dr[i, j, 1], 2);

                
                dr[j, i, 0] = (-1d)*dr[i, j, 0];
                dr[j, i, 1] = (-1d)*dr[i, j, 1];
                dr2[j, i] = dr2[i, j];
                

            }
        }


        //And here we compute the force with the distances determined in the previous step, derived form the WCA potential
        double lim = 1.12;
        //int count = 0;


        for (int i = 0; i < N; i++)
        {
            Fx = 0;
            Fy = 0;
            V = 0;



            for (int j = 0; j < N; j++)
            {

                if (i != j)
                {

                    if (Math.Sqrt(dr2[i, j]) < lim * sigma)
                    {

                        Fx = Fx + (48 * dr[i, j, 0] / dr2[i, j]) * (Math.Pow((Math.Pow(sigma, 2) / dr2[i, j]), 6) - 0.5 * Math.Pow(Math.Pow(sigma, 2) / dr2[i, j], 3));
                        Fy = Fy + (48 * dr[i, j, 1] / dr2[i, j]) * (Math.Pow((Math.Pow(sigma, 2) / dr2[i, j]), 6) - 0.5 * Math.Pow(Math.Pow(sigma, 2) / dr2[i, j], 3));
                        V = V + 4 * epsilon * (Math.Pow((Math.Pow(sigma, 2) / dr2[i, j]), 6) - Math.Pow(Math.Pow(sigma, 2) / dr2[i, j], 3)) + epsilon;
                        collision++;


                    }


                }
            }
            force[i, 0] = Fx;
            force[i, 1] = Fy;
            potential[i] = V;


        }

        
    }

    public void ParticleStarter(int numx, int numy, int maxfila, double dist, out double[,] r0, out double[,] v0)
    {
        r0 = new double[N, 2];
        v0 = new double[N, 2];
        for (int i = 0; i < N; i++)
        {
            r0[i, 0] = -25 + (numx + 1) * dist;
            r0[i, 1] = 7 + numy / maxfila * dist;
            numx++;
            numy++;
            if (numx == maxfila)
            {
                numx = 0;
            }

        }

        for (int i = 0; i < N; i++)
        {
            int signx = 1;
            int signy = 1;
            if (UnityEngine.Random.value <= 0.5)
            {
                signx = -1;
            }
            if (UnityEngine.Random.value <= 0.5)
            {
                signy = -1;
            }
            v0[i, 0] = initialspeed * signx;
            v0[i, 1] = initialspeed * signy;
        }

    }

    public void ParticlePosStarter(int numx, int numy, int maxfila, double dist, out double[,] r0)
    {
        r0 = new double[N, 2];
        for (int i = 0; i < N; i++)
        {
            r0[i, 0] = -25 + (numx + 1) * dist;
            r0[i, 1] = 3 + numy / maxfila * dist;
            numx++;
            numy++;
            if (numx == maxfila)
            {
                numx = 0;
            }

        }

    }

    void TempCalc(double[,] v0, out double Tleft, out double Tright, out double vrleft, out double vrright)
    {

        GameObject[] leftpart;
        GameObject[] rightpart;
        leftpart = GameObject.FindGameObjectsWithTag("L");
        rightpart = GameObject.FindGameObjectsWithTag("R");
        int Nleft = leftpart.Length;
        int Nright = rightpart.Length;
        double[,] vleft = new double[Nleft, 2];
        double[,] vright = new double[Nright, 2];
        int partn;
        vrleft = 0;
        vrright = 0;
        double vrightsquare = 0;
        double vleftsquare = 0;
        double singlevr = 0;


        Tleft = 0;
        Tright = 0;




        for (int i = 0; i < Nleft; i++)
        {
            partn = System.Convert.ToInt32(leftpart[i].name);
            for (int k = 0; k < 2; k++)
            {
                vleftsquare = vleftsquare + Math.Pow(v0[partn, k], 2);

            }
        }
        vrleft = Math.Sqrt(vleftsquare / Nleft);


        for (int i = 0; i < Nright; i++)
        {
            partn = System.Convert.ToInt32(rightpart[i].name);
            for (int k = 0; k < 2; k++)
            {
                vrightsquare = vrightsquare + Math.Pow(v0[partn, k], 2);
            }
        }
        vrright = Math.Sqrt(vrightsquare / Nright);

        if (collision > collisionlimit)
        {
            Tunlock = true;
        }
        else
        {
            Tunlock = false;
        }

        if (Tunlock == false)
        {
            totalvr = Math.Sqrt((vleftsquare + vrightsquare) / (Nleft + Nright));

        }


        //totalvr = Math.Sqrt((vrightsquare + vleftsquare) / N, 2);

        Tleft = 0.5 * Math.Pow(vrleft, 2);
        Tright = 0.5 * Math.Pow(vrright, 2);

        if(Tunlock == true)
        {
            for (int i = 0; i < Nleft; i++)
            {

                singlevr = 0;

                particrend = leftpart[i].GetComponent<Renderer>();

                partn = System.Convert.ToInt32(leftpart[i].name);


                for (int k = 0; k < 2; k++)
                {
                    singlevr = singlevr + Math.Pow(v0[partn, k], 2);
                }

                singlevr = Math.Sqrt(singlevr);

                if (singlevr < totalvr)
                {
                    particrend.material.color = Color.blue;
                }
                else
                {
                    particrend.material.color = Color.red;
                }




            }

            for (int i = 0; i < Nright; i++)
            {

                singlevr = 0;

                particrend = rightpart[i].GetComponent<Renderer>();

                partn = System.Convert.ToInt32(rightpart[i].name);


                for (int k = 0; k < 2; k++)
                {
                    singlevr = singlevr + Math.Pow(v0[partn, k], 2);
                }

                singlevr = Math.Sqrt(singlevr);

                if (singlevr < totalvr)
                {
                    particrend.material.color = Color.blue;

                }
                else
                {
                    particrend.material.color = Color.red;

                }

            }
        }

        

    }
    
    void EnergyCalc(double[,] v0, double[] V, out double Vtot, out double Ttot, out double Etot)
    {
        Vtot = 0;
        Ttot = 0;
        Etot = 0;


        for(int i = 0; i < N; i++)
        {
            for(int k = 0; k < 2; k++)
            {
                Ttot = Ttot + 0.5 * m * Math.Pow(v0[i, k], 2);
            }

            Vtot = Vtot + V[i];
        }

        Vtot = Vtot*0.5;

        Etot = Ttot + Vtot;
    }

    public void ComputeWork(out double work, out double Tfinal)
    {
        double mA = m * leftcount;
        double mB = m * rightcount;

        work = carCp * (N * m * Math.Exp((mA * Math.Log(Tright) + mB * Math.Log(Tleft)) / (N * m)) - mA * Tleft - mB * Tright);
        Tfinal = Math.Exp((mA * Math.Log(Tright) + mB * Math.Log(Tleft)) / (N * m));

        Debug.Log("Tleft: " + Tleft);
        Debug.Log("Tright: " + Tright);
    }

    void WriteCSV(string filename, float[] values, bool newfile = true)
    {
        TextWriter tw = new StreamWriter(Application.dataPath+"/"+filename, newfile);

        int nval = values.Length;

        for (int i = 0; i < nval; i++)
        {
            tw.Write(values[i]);
            if (i < nval - 1)
            {
                tw.Write(";");
            }
        }

        tw.WriteLine();
        tw.Close();
    }

    void WriteCSV(string filename, string[] names, bool newfile = false)
    {
        TextWriter tw = new StreamWriter(Application.dataPath + "/" + filename, newfile);

        int nval = names.Length;

        for (int i = 0; i < nval; i++)
        {
            tw.Write(names[i]);
            if (i < nval - 1)
            {
                tw.Write(";");
            }
        }
        tw.WriteLine();
        tw.Close();
    }

    public void BoxMuller(int nums, double var, double mean, out double[] x, out double[] y)
    {
        double ux;
        double uy;

        x = new double[nums];
        y = new double[nums];

        for (int i = 0; i < nums; i++)
        {
            ux = 0;
            uy = 0;
            while (ux == 0 | uy == 0)
            {
                ux = UnityEngine.Random.value;
                uy = UnityEngine.Random.value;
            }
            
            x[i] = mean + var*Math.Sqrt(-2 * Math.Log(ux)) * Math.Cos(2 * Math.PI * uy);
            y[i] = mean + var*Math.Sqrt(-2 * Math.Log(ux)) * Math.Sin(2 * Math.PI * uy);
        }
    }

    
}



