using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientInfoManager : MonoBehaviour
{
    private float radius = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {

            //Get List of Objects nearby in radius
            PatientInfo nearestPatient = GetNearbyObjects();
            if (nearestPatient) Debug.Log("<color=green> Getting Patient ID."+nearestPatient.patientIndex+" is the patient index.</color>");
        }
    }

    private PatientInfo GetNearbyObjects()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        List<PatientInfo> patientInfos = new List<PatientInfo>();
        foreach (var hitCollider in hitColliders) {
             if(hitCollider.GetComponent<PatientInfo>())
                patientInfos.Add(hitCollider.GetComponent<PatientInfo>());
        }
        if (patientInfos.Count > 0) return patientInfos[0];
        else return null;
    }
}
