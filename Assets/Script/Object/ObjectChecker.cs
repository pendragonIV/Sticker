using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{

    [SerializeField]
    private Camera cam;

    #region Cast n Check

    private Ray ray;

    #endregion

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    private void CastRay()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.GetComponent<Sticker>() != null)
            {
                GameManager.instance.SetChoosenStickerID(hit.collider.gameObject.GetComponent<Sticker>().GetID());
            }
        }
    }
}
