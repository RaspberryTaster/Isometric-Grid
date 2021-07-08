using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    bool isHit
    {
        get
        {
            return hits.Length > 0;
        }
    }
    private Vector3 direction = Vector3.down;
    private float maxDistance = 20;
    public LayerMask EnviromentMask;
    public TerrainType[] WalkableRegions = new TerrainType[0];
    public Vector3 positionOffset = Vector3.zero;
    private NodeObject nodeObject;
    RaycastHit hit;
    public RaycastHit[] hits = new RaycastHit[0];

    Dictionary<int, int> walkableRegionDictionary = new Dictionary<int, int>();

	public void SetRegions()
	{
		foreach (TerrainType region in WalkableRegions)
		{
			EnviromentMask.value += region.terrainMask.value;
			walkableRegionDictionary.Add((int)Mathf.Log(region.terrainMask.value, 2), region.penalty);
		}
	}

	public void Spawn(int x, int y, Vector3 worldPoint, NodeObject nodePrefab, GameObject GridNodes, NodeGrid NodeGrid)
	{
        string Name = "N/A";
        int movementPenalty = 99;
        nodeObject = Instantiate(nodePrefab, worldPoint, Quaternion.identity);
        hits = Physics.BoxCastAll(worldPoint, nodeObject.transform.lossyScale / 2, direction, nodeObject.transform.rotation, maxDistance, EnviromentMask);
        bool walkable = false;
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (i == 0)
                {
                    hit = hits[i];
                }
                else
                {
                    if (hit.point.y < hits[i].point.y)
                    {
                        hit = hits[i];
                    }
                }
            }


            Vector3 vector3 = hit.point + positionOffset + new Vector3(0, this.nodeObject.rnd.bounds.size.y / 2, 0);
            nodeObject.transform.position = new Vector3(nodeObject.transform.position.x, vector3.y, this.nodeObject.transform.position.z);
            Name = hit.transform.gameObject.name;
            walkable = !(hit.transform.gameObject.layer == LayerMask.NameToLayer("Unwalkable"));
        }

        worldPoint = nodeObject.transform.position;

        TIleMode tileMode;
        if(walkable)
		{
            walkableRegionDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
            tileMode = TIleMode.DEFAULT;
        }
        else
		{
            tileMode = TIleMode.UNREACHABLE;
		}
        
        nodeObject.transform.parent = GridNodes.transform;
        NodeGrid.NodeArray[x, y] = new Node(walkable, worldPoint, x, y, (int)tileMode, Name, movementPenalty, nodeObject);
        nodeObject = null;
    }

    void OnDrawGizmos()
    {

        if (nodeObject == null) return;


        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(nodeObject.transform.position, direction * hit.distance);
            Gizmos.DrawWireCube(nodeObject.transform.position + direction * hit.distance, nodeObject.transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(nodeObject.transform.position, direction * maxDistance);
        }
    }
}
[System.Serializable]
public class TerrainType
{
    public LayerMask terrainMask;
    public int penalty;
}
