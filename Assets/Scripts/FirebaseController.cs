using Firebase.Database;
using Firebase.Extensions;
using Google.XR.ARCoreExtensions.GeospatialCreator.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseController : MonoBehaviour
{
    DatabaseReference reference;
    public GameObject robotPrefab;
    public Data robotsData;
    public List<GameObject> robotsInSceneList = new List<GameObject>();

    private void Awake()
    {
        var db = FirebaseDatabase.GetInstance("https://geospatial-demo-7bca9-default-rtdb.firebaseio.com");
        reference = db.RootReference;
    }

    void Start()
    {

        reference.ValueChanged += ValueChanged;
        //reference.Child("robots").GetValueAsync().ContinueWithOnMainThread(task => {

        //    if (task.IsFaulted)
        //    {
        //        // Handle the error...
        //    }
        //    else if (task.IsCompleted)
        //    {
        //        DataSnapshot snapshot = task.Result;
        //        var a = snapshot.GetRawJsonValue();
        //        Debug.Log(a);
        //        // Do something with snapshot...
        //    }
        //});


    }

    private void ValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        DataSnapshot snapshot = args.Snapshot;
        var a = snapshot.GetRawJsonValue();
        robotsData = JsonUtility.FromJson<Data>(a);
        Debug.Log(a);

        foreach (var item in robotsInSceneList)
        {
            Destroy(item);
        }
        robotsInSceneList.Clear();

        for (int i = 0; i < robotsData.robots.Count; i++)
        {
            var index = i;
            var obj = Instantiate(robotPrefab);
            obj.GetComponent<RobotPrefab>().SetData(robotsData.robots[index]);
            obj.SetActive(true);
            obj.GetComponent<RobotPrefab>().aRGeospatialCreatorAnchor.SetUnityPosition();
            robotsInSceneList.Add(obj);
        }

    }

    private void OnDestroy()
    {
        reference.ValueChanged -= ValueChanged;
    }

}

[System.Serializable]
public class Robot
{
    public string name;
    public double latitude;
    public double longitude;
    public double altitude;
}
[System.Serializable]
public class Data
{
    public List<Robot> robots;
}
