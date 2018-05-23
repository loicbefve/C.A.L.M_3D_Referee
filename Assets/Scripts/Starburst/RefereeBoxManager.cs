using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarburstGaming;

public class RefereeBoxManager : MonoBehaviour {

    public GameObject boxPrefab;

    private Dictionary<int, Transform> boxes;
    Topic.CometReceiver boxTopic;

	// Use this for initialization
	void Start () {

        boxes = new Dictionary<int, Transform>();

        if (SatelliteLibrary.Running)
        {

            boxTopic = new Topic.CometReceiver("Boxes", Topic.CometReceiver.StorageType.QUEUE);

            boxTopic.OnCometReceived(UpdateBox);

        }

	}

    void UpdateBox(Comet c)
    {
        int id = c["ID"].AsInt();
        Vector2 pos = c["Position"].AsVector2();

        if (boxes.ContainsKey(id))
        {

            boxes[id].transform.localPosition = new Vector3(pos.x, 0.1f, pos.y);

        }
        else
        {

            boxes.Add(id, Instantiate(boxPrefab, new Vector3(pos.x, 0.1f, pos.y), Quaternion.identity).transform);

        }

    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.touchSupported)
        {

            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {

                createBox(Input.GetTouch(0).position);

            }

        } else
        {

            if (Input.GetMouseButtonUp(0))
            {

                createBox(Input.mousePosition);

            }

        }
		
	}

    void createBox(Vector3 screenPos)
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(screenPos);    // We use the impact point of the ray with the lava plane
        if (Physics.Raycast(ray, out hit) && SatelliteLibrary.Running)
        {
            Comet c = new Comet("Box");    // We build a comet from the Platform.comet file

            c["Position"].Set(new Vector2(hit.point.x, hit.point.z));

            Topic.SendComet("NewBox", c);
        }

    }
}
