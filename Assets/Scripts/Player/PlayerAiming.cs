using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] GameObject playerBody;
    [SerializeField] GameObject aimingPoint;
    [SerializeField] float gizmoRadius = 1.0f;

    Vector3 collidedTargetPoint;
    Vector3 screenPos;
    Vector3 worldPos;


   

    public void ProcessAiming(Vector2 aimInput)
    {
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        Ray cameraRay = Camera.main.ScreenPointToRay(aimInput);

        

        Debug.DrawLine(cameraRay.origin, worldPos, Color.blue);

        float hitDist = 0.0f;
        
        if (groundPlane.Raycast(cameraRay, out hitDist))
        { 
            Vector3 targetPoint = cameraRay.GetPoint(hitDist);
            collidedTargetPoint = targetPoint;

            Debug.DrawLine(cameraRay.origin, targetPoint, Color.red);

            screenPos = new Vector3(aimInput.x, aimInput.y, Camera.main.farClipPlane);

            worldPos = Camera.main.ScreenToWorldPoint(screenPos);


            Vector3 finalAimingPoint = new Vector3(targetPoint.x, targetPoint.y + 1f, targetPoint.z);
            Vector3 finalLookPoint = new Vector3(targetPoint.x, transform.position.y, targetPoint.z);

            aimingPoint.transform.position = finalAimingPoint;
            playerBody.transform.LookAt(finalLookPoint);

           
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(collidedTargetPoint, gizmoRadius);
        Gizmos.DrawSphere(worldPos, gizmoRadius);
    }
}
