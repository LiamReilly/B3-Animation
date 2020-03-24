using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentCreater : MonoBehaviour
{
    public static HashSet<GameObject> CreatedAgents = new HashSet<GameObject>();
    public HashSet<GameObject> CreatedAd = new HashSet<GameObject>();
    public float radius;
    public GameObject agent;
    //public GameObject Adversary;
    public int agentCount;

    Vector3 location;
    ObjectSelection Selectionmanager;
    //public Material SelectedColor;
    //public Material DeselectedColor;
    private int Count;
    //private int AdCount;


    // Start is called before the first frame update
    void Start()
    {
        Selectionmanager = GameObject.Find("Camera").gameObject.GetComponent<ObjectSelection>();
        Count = 0;

        radius = radius * 2;
        var agParent = GameObject.Find("AgentCreator");

        for (int i = 0; i < agentCount; i++)
        {
            location = new Vector3((Random.value - 0.5f) * radius, 0.5f, (Random.value - 0.5f) * radius);
            var newAgent = Instantiate(agent, location, Quaternion.identity);

            //var a = newAgent.GetComponent<Rigidbody>();
            //a.AddForce(100,0,0);

            //newAgent.transform.position = new Vector3((Random.value - 0.5f) * radius, 0.5f, ((Random.value - 0.5f)) * radius);
            newAgent.name = "a" + i;
            newAgent.transform.parent = agParent.transform;
            newAgent.gameObject.GetComponent<Light>().enabled = false;

            CreatedAgents.Add(newAgent);
            Count++;

        }
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }*/
    public void addAgent()
    {

        var agParent = GameObject.Find("AgentCreator");
        agentCount = Count + 5;
        while (Count < agentCount)
        {
            location = new Vector3((Random.value - 0.5f) * radius, 0.5f, (Random.value - 0.5f) * radius);
            var newAgent = Instantiate(agent, location, Quaternion.identity);
            newAgent.name = "a" + Count;
            newAgent.transform.parent = agParent.transform;
            newAgent.gameObject.GetComponent<Light>().enabled = false;
            CreatedAgents.Add(newAgent);
            Count++;
        }
    }
    public void SelectAll()
    {
        foreach (var item in CreatedAgents)
        {
            /*MeshRenderer gameObjectRenderer = item.gameObject.GetComponent<MeshRenderer>();
            gameObjectRenderer.material = SelectedColor;*/
            item.gameObject.GetComponent<Light>().enabled = true;
            Selectionmanager.GroupAgents.Add(item);
        }
    }
    public void DeselectAll()
    {
        foreach (var item in CreatedAgents)
        {
            /*MeshRenderer gameObjectRenderer = item.gameObject.GetComponent<MeshRenderer>();
            gameObjectRenderer.material = DeselectedColor;*/
            if (Selectionmanager.GroupAgents.Contains(item))
            {
                Selectionmanager.GroupAgents.Remove(item);
                item.gameObject.GetComponent<Light>().enabled = false;
                item.GetComponent<SetTarget>().SetDestination(item.transform.position);
            }
            
        }
    }
    /*public void CreateAdversary()
    {
        AdCount++;
        location = new Vector3(0f, .5f, 0f);
        var newAdversary = Instantiate(Adversary, location, Quaternion.identity);
        newAdversary.name = "E" + AdCount;
        //newAdversary.gameObject
        CreatedAd.Add(newAdversary);
    }*/
}