using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject Main;
    private GameObject Third;
    private GameObject Canvas;
    private GameObject WasdMan;
    public GameObject NavMan;
    private GameObject Creatormanager;
    private GameObject SelectButton;
    public GameObject DeselectButton;


    private void Start()
    {
        Main = GameObject.Find("Camera");
        Third = GameObject.Find("WASD_Adam_General_Animations").transform.GetChild(2).gameObject;
        Canvas = GameObject.Find("Canvas");
        WasdMan = GameObject.Find("WASD_Adam_General_Animations");
        Canvas.transform.GetChild(1).gameObject.SetActive(true);
        Creatormanager = GameObject.Find("AgentCreator");
        SelectButton = GameObject.Find("SelectAll");
        StartCoroutine(wait3Seconds());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("Playground");
        }
    }

    public void ChangeCam()
    {
        Third.gameObject.SetActive(false);
        Main.gameObject.SetActive(true);
        Canvas.transform.GetChild(0).gameObject.SetActive(false);
        WasdMan.GetComponent<Controller>().enabled = false;
    }
    IEnumerator wait3Seconds()
    {
        yield return new WaitForSeconds(3);
        Canvas.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void MakeAgent()
    {
        Creatormanager.gameObject.GetComponent<AgentCreater>().addAgent();
    }
    public void SelectAll()
    {
        //source1.Play();
        Creatormanager.gameObject.GetComponent<AgentCreater>().SelectAll();
        SelectButton.gameObject.SetActive(false);
        DeselectButton.gameObject.SetActive(true);
    }
    public void DeselectAll()
    {
        //source1.Play();
        Creatormanager.gameObject.GetComponent<AgentCreater>().DeselectAll();
        SelectButton.gameObject.SetActive(true);
        DeselectButton.gameObject.SetActive(false);
    }

}
