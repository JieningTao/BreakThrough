using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineconeSubCollider : MonoBehaviour
{

    private Pinecone MyPinecone;

    // Start is called before the first frame update
    void Start()
    {
        MyPinecone = GetComponentInParent<Pinecone>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        MyPinecone.CheckToAddTarget(other);
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
