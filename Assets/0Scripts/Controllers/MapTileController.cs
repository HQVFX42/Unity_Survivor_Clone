using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTileController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        Camera camera = collision.gameObject.GetComponent<Camera>();
        if (!camera)
        {
            return;
        }

        Vector3 dir = camera.transform.position - transform.position;
        float dirX = dir.x < 0 ? -1 : 1;
        float dirY = dir.y < 0 ? -1 : 1;

        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            transform.Translate(Vector3.right * dirX * gameObject.GetComponent<BoxCollider2D>().size * 2);
        }
        else
        {
            transform.Translate(Vector3.up * dirY * gameObject.GetComponent<BoxCollider2D>().size * 2);
        }
    }
}
