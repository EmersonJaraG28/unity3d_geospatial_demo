using Google.XR.ARCoreExtensions.GeospatialCreator.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotPrefab : MonoBehaviour
{
    public ARGeospatialCreatorAnchor aRGeospatialCreatorAnchor;
    public Text nameText;
    public Robot robotData;

    public void SetData(Robot data)
    {
        robotData = data;
        nameText.text = data.name;
        aRGeospatialCreatorAnchor.Latitude = robotData.latitude;
        aRGeospatialCreatorAnchor.Longitude = robotData.longitude;
        aRGeospatialCreatorAnchor.Altitude = robotData.altitude;
        // aRGeospatialCreatorAnchor.SetUnityPosition();
    }

}