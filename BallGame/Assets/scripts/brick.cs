using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brick : MonoBehaviour
{
    public int hit = 1;
    public int score = 100;
    public Vector3 rotator;
    public Material brickHit;
    Material orgMaterial;
    Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        //transform.Rotate(rotator * (transform.position.x + transform.position.y) * 0.1f);
        _renderer = GetComponent<Renderer>();
        orgMaterial = _renderer.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotator * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        hit--;
        if(hit<=0)
        {
            GameManager.Instance.Score += score;
            Destroy(gameObject);
          
        }
        _renderer.sharedMaterial = brickHit;
        Invoke("RestoreMaterial", 0.1f);


    }
    void RestoreMaterial()
    {

        _renderer.sharedMaterial = orgMaterial;

    }
}
