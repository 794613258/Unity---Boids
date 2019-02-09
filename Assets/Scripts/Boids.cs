using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boids : MonoBehaviour {
    [SerializeField]
    //public Vector2 position;
    public float maxDistance = 1;
    public float vlim;
    public List<GameObject> listGameObjct;

    Rigidbody myRigidBody;
    

	// Use this for initialization
	void Start () {
        GeneralSettings camera = GameObject.Find("Main Camera").GetComponent<GeneralSettings>();
        float velociteMin = camera._velociteMin;
        float velociteMax = camera._velociteMax;
        listGameObjct = new List<GameObject>();
        myRigidBody = this.GetComponent<Rigidbody>();
        vlim = camera.vLim;
        this.GetComponent<Collider>().enabled = false;
        this.GetComponent<Collider>().enabled = true;
        myRigidBody.velocity = new Vector3(Random.Range(velociteMin, velociteMax), 0, Random.Range(velociteMin, velociteMax));


    }

    //// Caluculates the separation vector with a target.
    //Vector3 GetSeparationVector(Transform target)
    //{
    //    var diff = transform.position - target.transform.position;
    //    var diffLen = diff.magnitude;
    //    var scaler = Mathf.Clamp01(1.0f - diffLen / controller.neighborDist);
    //    return diff * (scaler / diffLen);
    //}
    // Update is called once per frame
    void Update () {
        if(listGameObjct.Count > 0 )
        {
            Quaternion currentRot = this.transform.rotation;

            Vector3 separation = Vector3.zero;
            Vector3 alignment = this.transform.forward;
            Vector3 cohesion = this.transform.position;

            foreach (GameObject go in listGameObjct)
            {
                UnityEngine.Transform transF = go.transform;
                separation += this.separation(go.transform);
                alignment += go.transform.forward;
                cohesion += go.transform.position;

            }

            float limiter = 0.9f / listGameObjct.Count;
            //limiter = (limiter > )
            alignment *= limiter;
            cohesion = (((cohesion * limiter) - this.transform.position)).normalized;
            this.transform.position = (this.transform.position + this.transform.forward * (2.0f * Time.deltaTime) )+this.transform.forward * 2.5f;
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, (separation + alignment + cohesion).normalized);
            
            if (rot != this.transform.rotation)
            {
                float exp = Mathf.Exp(-2.0f * Time.deltaTime);
                this.transform.rotation = Quaternion.Slerp(rot, this.transform.rotation, exp);
            }
            //Vector3 _cohesion = this.cohesion();
            //Vector3 _separation = this.separation();
            //Vector3 _alignement = this.alignement();
            ////Debug.Log(_cohesion);
            ////Debug.Log(_separation);
            ////Debug.Log(_alignement);
            //myRigidBody.velocity += _cohesion + _separation + _alignement;
            //float velocitynormal = Mathf.Sqrt(Mathf.Pow(myRigidBody.velocity.x, 2) + Mathf.Pow(myRigidBody.velocity.y, 2) + Mathf.Pow(myRigidBody.velocity.z, 2));
            //if ( velocitynormal > vlim)
            //{
            //    myRigidBody.velocity = (myRigidBody.velocity / velocitynormal) * vlim;
            //}
            ////IF | b.velocity | > vlim THEN
            ////            b.velocity = (b.velocity / | b.velocity |) * vlim
            ////myRigidBody.velocity = (new Vector3(10,0,10) - myRigidBody.velocity) / 100;
            //this.transform.position += (new Vector3(10, 0, 10) -myRigidBody.velocity)/100;


        }

    }

    private void OnTriggerEnter(Collider otherBoid)
    {
        //Debug.Log(otherBoid.gameObject);
        listGameObjct.Add((GameObject)otherBoid.gameObject);
    }

    void OnTriggerExit(Collider otherBoid)
    {
        listGameObjct.Remove((GameObject)otherBoid.gameObject);
    }

    Vector3 separation(Transform t)
    {
        Vector3 sep = this.transform.position - t.transform.position;
        float magnitude = Mathf.Sqrt((Mathf.Pow(sep.x, 2) + Mathf.Pow(sep.y, 2) + Mathf.Pow(sep.z, 2)));
        float speed = 1.0f - magnitude / 2.0f;
        speed = (speed < 0 || speed > 1) ? ((speed < 0)?0 : -1): speed;
        return sep * (speed / magnitude);
    }



    #region
    ////3rules
    //private Vector3 cohesion()
    //{
    //    Vector3 newCohesion = new Vector3(0,0,0);
    //    foreach(GameObject go in listGameObjct)
    //    {
    //        newCohesion += go.transform.position;
    //    }
    //    newCohesion = newCohesion / listGameObjct.Count;
    //    //Debug.Log(newCohesion);
    //    return (newCohesion - this.transform.position) / 100f;
    //    //return new Vector3();
    //}

    //private Vector3 separation()
    //{
    //    Vector3 newSeparation = new Vector3(0, 0, 0);
    //    Vector3 myPosition = this.transform.position;
    //    Vector3 bufferVector;
    //    foreach(GameObject go in listGameObjct)
    //    {
    //        bufferVector = go.transform.position - myPosition;
    //        if (bufferVector.normalized < maxDistance)
    //        {
    //            newSeparation -= bufferVector;
    //        }
    //    }
    //    return newSeparation;
    //    //return new Vector3();
    //}

    //private Vector3 alignement()
    //{
    //    Vector3 newAlignement = new Vector3(0, 0, 0);
    //    foreach(GameObject go in listGameObjct)
    //    {
    //        newAlignement += go.GetComponent<Rigidbody>().velocity;
    //    }
    //    return newAlignement;
    //}
    #endregion

}
