using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    private MessageManager messageManager;

    public MessageManager()
    {
        if (messageManager == null)
        {
            messageManager = this;
        }
    }

    public MessageManager getRef()
    {
        return messageManager;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
