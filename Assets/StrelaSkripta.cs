using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StrelaSkripta : MonoBehaviour
{

    Tilemap burad;
    public Rigidbody2D strela;

    // Start is called before the first frame update
    void Start()
    {

        burad = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -20 || transform.position.x >20 || transform.position.y >10 || transform.position.y<-10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.name.StartsWith("Strela") || !(gameObject.name.EndsWith(collision.name))))
        {

            if (collision.name.Equals("Burad"))
            { 
                Tilemap mapa = collision.gameObject.GetComponent<Tilemap>();
                var pravac = strela.velocity.normalized / 2f;
                Vector2 pravac1 = Quaternion.AngleAxis(45, Vector3.forward) * pravac;
                Vector2 pravac2 = Quaternion.AngleAxis(-45, Vector3.forward) * pravac;

                Vector2 pozicija1 = collision.ClosestPoint(transform.position) + pravac1;
                Vector2 pozicija2 = collision.ClosestPoint(transform.position) + pravac2;
                mapa.SetTile(mapa.WorldToCell(pozicija1), null);
                mapa.SetTile(mapa.WorldToCell(pozicija2), null);

            }
            if (collision.name.Equals("Voda"))
            {

            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
