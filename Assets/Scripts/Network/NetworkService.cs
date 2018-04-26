using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;

public class NetworkService : MonoBehaviour
{

    static NetworkService Instance = null;

    private Channel channel;

    // Use this for initialization
    void Awake()
    {
        if (NetworkService.Instance == null)
        {
            NetworkService.Instance = this;
            this.Start();
            Debug.Log("NetworkService: Awake");
        }
    }

    void OnDestroy()
    {
        Stop();
    }

    // Update is called once per frame
    private void Start()
    {
        this.channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
    }

    private void Stop()
    {
        this.channel.ShutdownAsync();
    }
}
