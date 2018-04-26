using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using Tob;

public class NetworkService : MonoBehaviour {

    static NetworkService Instance = null;

    private Channel channel;
    private ToB.ToBClient client;
    private NodeInfo nodeInfo;

    // Use this for initialization
    void Awake()
    {
        if (NetworkService.Instance == null)
        {
            NetworkService.Instance = this;
            this.ServiceStart();
            Debug.Log("NetworkService: Awake");
        }
    }

    void OnDestroy()
    {
        Stop();
    }

    // Update is called once per frame
    private void ServiceStart() {
        NetworkID.Local_ID = NetworkID.generateNewID(128);
        this.nodeInfo = new NodeInfo();
        nodeInfo.Id = NetworkID.Local_ID;
        this.channel = new Channel("127.0.0.1:50051", ChannelCredentials.Insecure);
        this.client = new Tob.ToB.ToBClient(this.channel);
    }

    private async void ServiceRun() {
        using (var handle = this.client.Subscribe(this.nodeInfo)) {
            while(await handle.ResponseStream.MoveNext()) {

            }
        }


    }

    private void Stop()
    {
        this.channel.ShutdownAsync();
    }
}
