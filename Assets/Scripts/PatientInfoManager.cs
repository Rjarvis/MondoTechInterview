using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientInfoManager : MonoBehaviour
{
    public string filePathToJSON;
    private float radius = 5f;
    private JSONData rawData;
    private JSONPatientCollection collection;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        GetInfoFromJSON();
        collection = rawData.MoveToCollection(rawData.data);
        //Debug.Log(rawData.data[0].name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {

            //Get List of Objects nearby in radius
            PatientInfo nearestPatient = GetNearbyObjects();
            if (nearestPatient) {
                canvas.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = collection.patients[nearestPatient.patientIndex].name;
            }
        }
    }

    private void GetInfoFromJSON()
    {
        using (StreamReader stream = new StreamReader(filePathToJSON))
        {
            string json = stream.ReadToEnd();
            rawData = JsonUtility.FromJson<JSONData>(json);
            Debug.Log("JSON data loaded with count:<color=blue>" + rawData.data.Count + "</color>");

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

[System.Serializable]
public class JSONData {

    public List<JSONPatient> data;
    public JSONPatientCollection collection = new JSONPatientCollection();

    public JSONPatientCollection MoveToCollection(List<JSONPatient> dataObj) {
        collection.patients = dataObj.ToArray();
        return collection;
    }
}

[System.Serializable]
public class JSONPatient
{
    public int index;
    public int age;
    public string name;
    public string gender;
}
