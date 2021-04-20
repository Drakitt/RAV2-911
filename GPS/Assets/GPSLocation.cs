using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class GPSLocation : MonoBehaviour
{
    // Start is called before the first frame update

    public float latitud;
    public float longitud;
    public Text gpsText;
    public Text nameText;
   // public GameObject placementIndicator;
    public GameObject arPlaza;
    public GameObject arColegio;
    public GameObject arStadium;
    public GameObject arCasa;
    private GameObject objectI;
    private Pose PlacementPose; 
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        if(Input.location.isEnabledByUser)
        StartCoroutine(GetLocation());
            Destroy(objectI); 
    }

    private IEnumerator GetLocation()
    {
        Input.location.Start();
        while(Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(0.5f);
        }

        latitud = Input.location.lastData.latitude;
        longitud = Input.location.lastData.longitude;
        yield break;

    }
    void ARPlaceObject()
    {
        //gpsText.color=Color.gray;
       
        if ((latitud>=-17.392737 && latitud<=-17.392714) && (longitud>=-66.279078 && longitud<=-66.278844))
        {
            if (objectI==null)
            {
                instanciScale(arColegio,"Colegio cristina Prado");
                gpsText.color=Color.green; 
            }        
        }
        else if ((latitud>=-17.391399 && latitud<=-17.391153)  && (longitud>=-66.277286 && longitud<=-66.277071))
        {
            if (objectI==null)
            {
                instanciScale(arPlaza,"Plaza Elfec");
                gpsText.color=Color.green;  
            }
        }
        else if ((latitud>=-17.393422  && latitud<=-17.391165)  && (longitud>=-66.275604 && longitud<=-66.274506))
        {
            if (objectI==null)
            {
                instanciScale(arStadium,"Estadio de Quillacollo");
                gpsText.color=Color.green;  
            }
        }
        //casa
        else if ((latitud>=-17.392375f && latitud<=-17.39228f) && (longitud>=-66.2774 && longitud<=-66.277071))
        {
            if (objectI==null)
            {
                instanciScale(arCasa, "Casa de Raquelita");
                gpsText.color=Color.green;   
            }  
        }
        
        // else if (latitud==0 && longitud==0)
        // {
        //     if (objectI==null)
        //     {
        //         instanciScale(arCasa, "Casa de Raquelita");
        //         gpsText.color=Color.green;  
        //     }  
        // }
        else
        {
            Destroy(objectI);
            gpsText.color=Color.red;
            nameText.text = "";
        }
               
    }
    void instanciScale(GameObject instanceObject, string name){
        objectI = Instantiate(instanceObject,new Vector3(PlacementPose.position.x+2.4000001f,PlacementPose.position.y+-4.63395119f,PlacementPose.position.z+18.2210217f),PlacementPose.rotation);
        objectI.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        nameText.text = name;
    }
    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f,0.5f));
        var hits = new List <ARRaycastHit>();

        aRRaycastManager.Raycast(screenCenter,hits,TrackableType.Planes);
        placementPoseIsValid = hits.Count >0;
        if(placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;

        }
    }

    // Update is called once per frame
    void Update()
    {
        GetLocation();
        longitud = Input.location.lastData.longitude;
        latitud = Input.location.lastData.latitude;
        gpsText.text = " Latitud: "+latitud + "\nLongitud "+ longitud;
        ARPlaceObject();
        objectI.transform.Rotate(new Vector3(0f,30f,0f)*Time.deltaTime);
    }
}

