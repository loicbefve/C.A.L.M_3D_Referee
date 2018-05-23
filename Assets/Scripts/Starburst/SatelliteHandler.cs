using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarburstGaming;

public class SatelliteHandler : MonoBehaviour {

    PolledComponent refereeComp;

    void Awake()
    {
    #if UNITY_IOS || UNITY_ANDROID
        EngineMobile.Start();
    #endif

        SatelliteLibrary.Init();

        // Register Satellite and Component
        Satellite.Register("FiL_RefereeSat");
        refereeComp = new PolledComponent("FiL_Referee");
    }

    // Use this for initialization
    void Start () {

        if (SatelliteLibrary.Running)
        {
            // Execute OnSessionFound when a session has been found
            EngineExtensions.OnSessionJoined(SessionJoined);
        }

    }

    void SessionJoined()
    {
        // This simple satellite does not have any particular action to execute on START,
        // just signal that we are started.
        refereeComp.OnComponentStart(() => refereeComp.ReportState(ComponentState.STARTED));

        // Just Quit the application when the Component is stopped
        refereeComp.OnComponentStop(Application.Quit);
    }

    void OnApplicationQuit()
    {
        // If we are currently in a session, we can report that we are now stopped
        if (Engine.InSession) refereeComp.ReportState(ComponentState.STOPPED);

        SatelliteLibrary.Close();

    #if UNITY_IOS || UNITY_ANDROID
        EngineMobile.Stop();
    #endif
    }

    // Update is called once per frame
    void Update () {
		
	}
}
