using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Grpc.Core;
using Tob;
using UniRx;

public class NetworkService : MonoBehaviour {

    static NetworkService Instance = null;

    static bool isServer = false;

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
            while (await handle.ResponseStream.MoveNext()) {
                Tob.Event e = handle.ResponseStream.Current;
                Debug.Log("[Received] EventType:" + e.Topic);
                switch(e.Topic) {
                    case EventTopic.ServerEvent:
                        this.OnServerEvent(e.S);
                        break;
                    case EventTopic.PlayerEvent:
                        this.OnPlayerEvent(e.P);
                        break;
                    case EventTopic.MonsterEvent:
                        this.OnMonsterEvent(e.M);
                        break;
                    default:
                        break;
                }

            }

        }
    }

    private void Stop() {
        this.channel.ShutdownAsync();
    }

    private void OnServerEvent(ServerEvent e) {
        Debug.Log("[ServerEvent] EventType:" + e.Type);
        switch (e.Type) {
            case ServerEventType.ServerChange:
                if(e.Id.Equals(this.nodeInfo.Id)) {
                    NetworkService.isServer = true;
                }
                break;
            case ServerEventType.ServerYield:
                break;
        }
    }

    private void OnPlayerEvent(PlayerEvent e) {
        Debug.Log("[PlayerEvent] EventType:" + e.Type);
        switch (e.Type) {
            case PlayerEventType.PlayerCast:
                break;
            case PlayerEventType.PlayerCrouch:
                break;
            case PlayerEventType.PlayerDanmaged:
                break;
            case PlayerEventType.PlayerDie:
                break;
            case PlayerEventType.PlayerEnter:
                break;
            case PlayerEventType.PlayerEquipped:
                break;
            case PlayerEventType.PlayerExit:
                break;
            case PlayerEventType.PlayerJump:
                break;
            case PlayerEventType.PlayerMove:
                break;
            case PlayerEventType.PlayerPosition:
                break;
            default:
                break;
        }

    }

    private void OnMonsterEvent(MonsterEvent e) {
        Debug.Log("[MonsterEvent] EventType:" + e.Type);
        switch (e.Type) {
            case MonsterEventType.MonsterAttack:
                break;
            case MonsterEventType.MonsterDestroy:
                break;
            case MonsterEventType.MonsterDie:
                break;
            case MonsterEventType.MonsterLoot:
                break;
            case MonsterEventType.MonsterLootResult:
                break;
            case MonsterEventType.MonsterMove:
                break;
            case MonsterEventType.MonsterSpawn:
                break;
        }
    }

}
