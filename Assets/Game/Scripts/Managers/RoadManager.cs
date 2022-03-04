using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class RoadManager : SceneBasedMonoSingleton<RoadManager>
{
    [BoxGroup("Created Objects List")]
    [SerializeField] private List<GameObject> roads;
    [SerializeField, BoxGroup("Road Settings"), OnValueChanged(nameof(UpdateRoadFormat))]
    private int roadLength = 1;
    [SerializeField, BoxGroup("Road Settings"), OnValueChanged(nameof(UpdateRoadFormat))]
    private int roadWidth = 1;
    public int RoadCount => roads.Count;
    public int RoadWidth => roadWidth;
    [BoxGroup("Parent")]
    [SerializeField] private Transform roadParent;
    [SerializeField] private Final finalRoad;
    private Vector3 offSet => roadParent.position;
    private void UpdateRoadFormat()
    {
        roads[0].transform.localScale = new Vector3(roadWidth, 1f, roadLength);
        for (int i = 1; i < roads.Count; i++)
        {
            roads[i].transform.localScale = new Vector3(roadWidth, 1f, roadLength);
            roads[i].transform.position = offSet + roads[i - 1].transform.position + (roadParent.forward * roadLength);
        }
    }
#if UNITY_EDITOR
    [BoxGroup("Objects")]
    [SerializeField] private GameObject myRoad;

    [BoxGroup("Road Settings"), OnValueChanged(nameof(UpdateRoadCount)), Range(0, 999)]
    public int roadCount;
    private int prevRoadCount;

    private void CreateRoad()
    {
        var index = roads.Count == 0 ? 0 : roads.Count;
        var roadPosition = index * (roadParent.forward * roadLength) + offSet;
        var roadToCreate = myRoad;
        roadToCreate.transform.localScale = new Vector3(roadWidth, 1f, roadLength);
        var newRoad = PrefabUtility.InstantiatePrefab(roadToCreate, roadParent) as GameObject;
        newRoad.transform.position = roadPosition;
        roads.Add(newRoad);
        MoveFinal();
    }

    private void DeleteRoad()
    {
        if (roads.Count == 0) return;
        DestroyImmediate(roads[roads.Count - 1]);
        roads.RemoveAt(roads.Count - 1);
        MoveFinal();
    }
    private void DeleteAll()
    {
        for (int i = 0; i < roads.Count; i++)
        {
            DestroyImmediate(roads[i]);
        }
        roads.Clear();
    }
    private void UpdateRoadCount()
    {
        var difference = (roadCount - 1) - roads.Count;
        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                CreateRoad();
            }
        }
        else if (difference < 0)
        {
            for (int i = 0; i < -difference; i++)
            {
                DeleteRoad();
            }
        }
    }

    private void MoveFinal()
    {
        finalRoad.transform.position = (roadParent.forward * roadLength) + roads[roads.Count - 1].transform.position;
        finalRoad.transform.position += roadParent.forward * 2f;
    }
#endif


}