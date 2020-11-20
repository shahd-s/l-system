using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class L_System : MonoBehaviour
{
    public LineRenderer tree;//= new LineRenderer();
    public List<Vector3> verts = new List<Vector3>();
    public string axiom;
    public bool yes = true;
    public bool probability = false;
    public float delay = 0.1f;
    private string initaxiom;

    [Serializable]
    public struct Rule
    {
        public string name;
        public string rule;
    }
    public Rule[] rules;

    private int programindex = 0;
    private Dictionary<string, string> prodRules = new Dictionary<string, string>();
    private double toRad = Math.PI / 180.0;
    public double angle = 25.7;
    private double alpha = 90;
    public int n = 5;
    public double d = 1;

    private struct StackElement
    {
        public int ind;
        public double ang;
        public StackElement(int x, double y)
        {
            this.ind = x;
            this.ang = y;
        }
    }

    private Stack<StackElement> stack = new Stack<StackElement>();
    // Start is called before the first frame update

      public IEnumerator
    
     FillVertsStepWise(int nn, float delay)
    {
        for (  int i = 0; i < nn; i++)
        {
            for (int j = 0; j < axiom.Length; j++)
            {
                switch (axiom[j])
                {
                    case 'F':
                        verts.Add(new Vector3((float)(verts[verts.Count - 1].x + d * Math.Cos(alpha * toRad)), (float)(verts[verts.Count - 1].y + d * Math.Sin(alpha * toRad)), 0));
                       yield return new WaitForSeconds(delay);
                        break;
                    case '+':
                        alpha += angle;
                        break;
                    case '-':
                        alpha -= angle;
                        break;
                    case '[':
                        stack.Push(new StackElement(verts.Count - 1, alpha));
                        break;
                    case ']':
                        StackElement t = stack.Pop();
                        alpha = t.ang;
                        for (int x = verts.Count - 1; x >= t.ind; x--)
                             verts.Add(verts[x]);
                        break;

                }
                int index = verts.Count - 1;
               // Debug.Log("Done with: " + axiom[j] + "Angle is: " + alpha.ToString() + " and pos is: " + verts[index].x + ", " + verts[index].y + " index is: " + index.ToString());
                // yield return new WaitForSeconds(0f);


            }

           // yield return new WaitForSeconds(0.5f);
            //replace ax
            // axiom=axiom.Replace()
           // Debug.Log(i.ToString() + " ax is: " + axiom);

            //for (int t=0; t<axiom.Length; t++)
            //{
            //    Debug.Log("rep" + axiom[t]);
            //    if(prodRules.ContainsKey (axiom[t].ToString()))
            //    axiom = axiom.Replace(axiom[t].ToString(), prodRules[axiom[t].ToString()]);
            //}
            
        }
    }


    void
    FillVerts(int nn)
    {
        for (int i = 0; i < nn; i++)
        {
            for (int j = 0; j < axiom.Length; j++)
            {
                switch (axiom[j])
                {
                    case 'F':
                        verts.Add(new Vector3((float)(verts[verts.Count - 1].x + d * Math.Cos(alpha * toRad)),
                            (float)(verts[verts.Count - 1].y + d * Math.Sin(alpha * toRad)),
                            0));
                        break;
                    case '+':
                        alpha += angle;
                        break;
                    case '-':
                        alpha -= angle;
                        break;
                    case '[':
                        stack.Push(new StackElement(verts.Count - 1, alpha));
                        break;
                    case ']':
                        StackElement t = stack.Pop();
                        alpha = t.ang;
                        for (int x = verts.Count - 1; x >= t.ind; x--)
                            verts.Add(verts[x]);
                        break;

                }

                int index = verts.Count - 1;  
            }
             
        }
    }

    void Start()
    {
        Debug.Log("probability is " + probability);
        initaxiom = axiom;
        tree.startColor = Color.white;
        tree.endColor = Color.white;
       
        foreach (Rule r in rules)
        {
            if(!prodRules.ContainsKey(r.name))
            prodRules.Add(r.name, r.rule);
        }

        verts.Add(new Vector3(0, 0, 0));

        if(!probability)
        for (int i = 0; i < n; i++)
        {

            foreach (string key in prodRules.Keys)
            {
                axiom = axiom.Replace(key, prodRules[key]);
            }



        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                System.Random rand = new System.Random();
                int index = rand.Next(rules.Length);
                axiom = axiom.Replace(rules[index].name, rules[index].rule);
                 
            }
        }
        if (yes)
            FillVerts(1);
        else
            StartCoroutine(FillVertsStepWise(1, delay));
    }
    // Update is called once per frame
    void Update()
    {

         

        tree.positionCount = verts.Count;
        tree.SetPositions(verts.ToArray());


    }
}
