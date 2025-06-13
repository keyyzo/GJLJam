using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] GameObject playerBody;
    [SerializeField] GameObject aimingPoint;

    public void ProcessAiming(Vector2 aimInput)
    {
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        Ray cameraRay = Camera.main.ScreenPointToRay(aimInput);

        float hitDist = 0.0f;

        if (groundPlane.Raycast(cameraRay, out hitDist))
        { 
            Vector3 targetPoint = cameraRay.GetPoint(hitDist);

            Debug.DrawLine(cameraRay.origin, targetPoint, Color.red);

            Vector3 finalLookPoint = new Vector3(targetPoint.x, transform.position.y, targetPoint.z);

            aimingPoint.transform.position = finalLookPoint;
            playerBody.transform.LookAt(finalLookPoint);

           
        }

    }
}
