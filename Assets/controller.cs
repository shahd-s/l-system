using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class controller : MonoBehaviour
{
    [Serializable]
    public struct LSystem
    {
        
        public string axiom;
        public L_System.Rule[] rules;
        public double angle;
        public double d;
        public int n;
    }

    public LSystem[] lsystems;
    private LSystem[] lSystemsinit;

    private bool toggle = true;

    private bool probability = false;
    [Range(0.0001f, 0.1f)]
    public float delay = 0.01f;


    private GameObject g;
    private GameObject T;
    public float lengthStep = 0.005f;
    public int LSysIndex=0;
    public int startingit = 0;
    public float angleStep = 5;

    void Start()
    {
        g = Instantiate((GameObject)Resources.Load("Prefab/LSys"));
        T = Instantiate((GameObject)Resources.Load("Prefab/Txt"));

        g.GetComponent<L_System>().n = startingit;
        g.GetComponent<L_System>().axiom = lsystems[LSysIndex].axiom;
        g.GetComponent<L_System>().rules = lsystems[LSysIndex].rules;
        g.GetComponent<L_System>().angle = lsystems[LSysIndex].angle;
        g.GetComponent<L_System>().d = lsystems[LSysIndex].d;
        lSystemsinit = new List<LSystem>(lsystems).ToArray();  

    }
    void instan()
    {
        Destroy(g);
        g = Instantiate((GameObject)Resources.Load("Prefab/LSys"));
        g.GetComponent<L_System>().probability = probability;
        g.GetComponent<L_System>().n = startingit;
        g.GetComponent<L_System>().axiom = lsystems[LSysIndex].axiom;
        g.GetComponent<L_System>().rules = lsystems[LSysIndex].rules;
        g.GetComponent<L_System>().angle = lsystems[LSysIndex].angle;
        g.GetComponent<L_System>().d = lsystems[LSysIndex].d;
    }
    void Update()
    {
        probability = (LSysIndex == 8);
        //incriment iteration
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (startingit + 1 <= lsystems[LSysIndex].n)
            {
                startingit++;
                instan();
            }
        }
        else
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            
            if (startingit - 1 >= 0)
                {
                    startingit--;
                     instan();
                   
                }

            
        }
        else
            if(Input.GetKeyDown(KeyCode.C))
        {
            if(lsystems[LSysIndex].d -lengthStep >= 0)
            {
                lsystems[LSysIndex].d -= lengthStep;
                instan();
            }
        }
        else
            if (Input.GetKeyDown(KeyCode.D))
        {

            lsystems[LSysIndex].d += lengthStep;
                instan();
            
        }
        else
            if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            lsystems[LSysIndex].n++;
            instan();

        }
        else
            if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (lsystems[LSysIndex].n - 1 >= 0)
            {
                lsystems[LSysIndex].n--;
                startingit--;
            }
            instan();

        }
        else
            if (Input.GetKeyDown(KeyCode.A))
        {

            lsystems[LSysIndex].angle += angleStep;
            instan();

        }
        else
            if (Input.GetKeyDown(KeyCode.Z))
        {

            lsystems[LSysIndex].angle -= angleStep;
            instan();

        }
        else
            if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            LSysIndex = 0;
            startingit = 0;
            toggle = true;
            instan();

        }
        else
            if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            toggle = true;
            LSysIndex = 1;
            startingit = 0;
            instan();

        }
        else
            if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            toggle = true;
            LSysIndex = 2;
            startingit = 0;
            instan();

        }
        else
            if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            toggle = true;
            LSysIndex = 3;
            startingit = 0;
            instan();

        }
        else
            if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            toggle = true;
            LSysIndex = 4;
            startingit = 0;
            instan();

        }
        else
            if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            toggle = true;
            LSysIndex = 5;
            startingit = 0;
            instan();

        }
        else
            if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            toggle = true;
            LSysIndex = 6;
            startingit = 0;
            instan();

        }
        else
            if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            toggle = true;
            LSysIndex = 7;
            startingit = 0;
            instan();

        }
        else
            if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            //probability one.
            toggle = true;
            LSysIndex = 8;
            startingit = 0;
            probability = true;
            instan();

            
        }
        else
            if (Input.GetKeyDown(KeyCode.Space))
        {
            if (toggle)
         
                startingit = lsystems[LSysIndex].n;

           
            else
                startingit = 0;

         
            lsystems = new List<LSystem>(lSystemsinit).ToArray();
            instan();
            toggle = !toggle;
        }
        else
            if (Input.GetKeyDown(KeyCode.S))
        {
            Destroy(g);
            g = Instantiate((GameObject)Resources.Load("Prefab/LSys"));

            g.GetComponent<L_System>().n = lsystems[LSysIndex].n;
            g.GetComponent<L_System>().axiom = lsystems[LSysIndex].axiom;
            g.GetComponent<L_System>().rules = lsystems[LSysIndex].rules;
            g.GetComponent<L_System>().angle = lsystems[LSysIndex].angle;
            g.GetComponent<L_System>().d = lsystems[LSysIndex].d;
            g.GetComponent<L_System>().yes = false;
            g.GetComponent<L_System>().delay = delay ;
        }
        string p0 = "DISPLAYING L-SYSTEM " + (LSysIndex + 1).ToString() + '\n' + '\n';
        string p1= "n=" + lsystems[LSysIndex].n.ToString() + ",δ=" + lsystems[LSysIndex].angle.ToString()
            + '\n' + lsystems[LSysIndex].axiom + '\n';

        string p2 = "";
        for (int i = 0; i < lsystems[LSysIndex].rules.Length; i++)
        {
            p2 += lsystems[LSysIndex].rules[i].name + "→" + lsystems[LSysIndex].rules[i].rule + '\n';
        }
        p2 += "d= " + lsystems[LSysIndex].d.ToString()+'\n';
        p2 += "i= " + startingit.ToString();
        T.GetComponent<TMP_Text>().text =p0+ p1 + p2;

    }
}
