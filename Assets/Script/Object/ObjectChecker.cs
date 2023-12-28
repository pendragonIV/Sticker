using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ObjectChecker : MonoBehaviour
{

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject touchEffect;

    public static ObjectChecker instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #region Cast n Check

    private Ray ray;

    #endregion

    private void Start()
    {
        touchEffect.GetComponent<ParticleSystem>().Stop();
        touchEffect.SetActive(false);
    }

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

    public void PlayEffect(Vector3 pos)
    {
        touchEffect.transform.position = pos;
        touchEffect.SetActive(true);
        touchEffect.GetComponent<ParticleSystem>().Play();
    }
}
