using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarburstGaming;

public class BoxManager : MonoBehaviour {

    public GameObject boxPrefab;

    Dictionary<int, Transform> boxes;
    Topic.CometReceiver newBoxTopic;
    Comet boxComet;

	// Use this for initialization
	void Start () {

        boxes = new Dictionary<int, Transform>();

        if (SatelliteLibrary.Running)
        {

            newBoxTopic = new Topic.CometReceiver("NewPlatform", Topic.CometReceiver.StorageType.QUEUE);

            boxComet = new Comet("Box");

            newBoxTopic.OnCometReceived(NewBox);

        }

	}

    void NewBox(Comet c)
    {

        Vector2 pos = c["Position"].AsVector2();

        boxes.Add(boxes.Count, Instantiate(boxPrefab, new Vector3(pos.x, 3, pos.y), Quaternion.identity).transform);
        SendBoxes();

    }

    void SendBoxes()
    {

        foreach (var box in boxes)
        {

            Vector3 pos = box.Value.position;

            boxComet["ID"].Set(box.Key);
            boxComet["Position"].Set(new Vector2(pos.x, pos.z));

            Topic.SendComet("Boxes", boxComet);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
