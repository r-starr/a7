using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

public class TreasureHunter : MonoBehaviour
{
    public LayerMask mask;

    GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("CenterEyeAnchor");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            Collider[] collectibles = Physics.OverlapSphere(player.transform.position, 0.75f, mask.value);
            for(int i = 0; i < collectibles.Length; i++) {
                print(collectibles[i]);
                Destroy(collectibles[i].gameObject);
            }
        }
    }

}