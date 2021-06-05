using UnityEngine;

/// <summary>
/// CameraFollower Klasse sorgt dafür, dass die Kamera immer dem Spieler folgt.
/// </summary>
public class CameraFollower : MonoBehaviour
{
    #region Unity Variables
    public GameObject target;
    public Vector3 offset = new Vector3(0, 0, -1);
    #endregion

    #region Unity Methods
    private void FixedUpdate()
    {
        if(target)
        {
            transform.position = new Vector3(
                target.transform.position.x + offset.x,
                target.transform.position.y + offset.y,
                target.transform.position.z + offset.z
                );
        }
    }
    #endregion
}
