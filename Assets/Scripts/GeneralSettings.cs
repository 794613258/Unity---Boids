using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSettings : MonoBehaviour
{
    [SerializeField]
    public float _velociteMin = 0.1f;
    public float _velociteMax = 0.2f;
    public int      _nombres     = 50;
    public float vLim = 1.0f;
    public GameObject _go;
    public int wichOne = 1;
    public List<GameObject> listBoids;


    public int Nombres
    {
        get
        {
            return _nombres;
        }

        set
        {
            _nombres = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        listBoids = new List<GameObject>();

        for(int i = 0; i < _nombres; i++)
        {
            Vector3 position = new Vector3(Random.Range(-20.0f, 20.0f), 0, Random.Range(-20.0f, 20.0f));
            listBoids.Add(Instantiate(_go, position, Quaternion.identity));

        }
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = listBoids[this.wichOne].transform.position - listBoids[this.wichOne].transform.forward * 20;
        //this.transform.LookAt(listBoids[this.wichOne].transform);

        if(Input.GetKey(KeyCode.Z))
        {
            this.transform.position += this.transform.forward * 1.1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= this.transform.forward * 1.1f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Rotate(-Vector3.up, 200.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.up, 200.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Rotate(Vector3.left, 200.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Rotate(-Vector3.left, 200.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(Vector3.forward, 200.0f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(-Vector3.forward, 200.0f * Time.deltaTime);
        }
    }
}
