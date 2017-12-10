using UnityEngine;

public class WorldCursor : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
    }

    private void Update()
    {
        //Raycast into the world based on user's head position and orientation
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if(Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            //If the  raycast hit a hologram
            //Display the mesh
            meshRenderer.enabled = true;

            //Move the cursor to the point where the raycast hit
            this.transform.position = hitInfo.point;

            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);

        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}
