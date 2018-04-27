using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Grpc.Core;
using Tob;
using UniRx;

public class NetworkService : MonoBehaviour {

    public static NetworkService Instance = null;

    public static bool isServer = false;

    private Channel channel;
    private ToB.ToBClient client;
    private Metadata metadata;

    private Grpc.Core.AsyncClientStreamingCall<Tob.Event, Empty> sendhandle;


    // Use this for initialization
    void Awake()
    {
        if (NetworkService.Instance == null)
        {
            NetworkService.Instance = this;
            this.ServiceStart();
            Debug.Log("NetworkService: Awake");
            this.ServiceRun();
        }
    }

    void OnDestroy() {
        Stop();
    }
    

    private void ServiceStart() {
        NetworkID.Local_ID = NetworkID.generateNewID(128);
        this.channel = new Channel("127.0.0.1:16882", ChannelCredentials.Insecure);
        this.client = new Tob.ToB.ToBClient(this.channel);
        this.metadata = new Metadata {
            { "id", NetworkID.Local_ID}
        };
        



    }

    private void ServiceRun() {
        Debug.Log("NetworkService: Run");
        this.sendhandle = this.client.Publish(metadata);
        this.StartListen();

        Tob.Event e = new Tob.Event();
        e.Topic = EventTopic.PlayerEvent;
        this.SendEvent(e);
    }

    private async void StartListen() {
        Debug.Log("NetworkService: StartListen");
        using (var handle = this.client.Subscribe(new Empty(), metadata)) {
            while (await handle.ResponseStream.MoveNext()) {
                Tob.Event e = handle.ResponseStream.Current;
                Debug.Log("[Received] EventType:" + e.Topic);
                switch (e.Topic) {
                    case EventTopic.ServerEvent:
                        Debug.Log(e.S);
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

    public async void SendEvent(Tob.Event e) {
        Debug.Log("NetworkService: StartSendWindow");
        Debug.Log("[Sent] EventType:" + e.Topic);
        await sendhandle.RequestStream.WriteAsync(e);
    }

    private async void Stop() {
        Debug.Log("NetworkService: UnSub");
        await this.sendhandle.RequestStream.CompleteAsync();
        await this.channel.ShutdownAsync();
    }

    private void OnServerEvent(ServerEvent e) {
        Debug.Log("[ServerEvent] EventType:" + e.Type);
        switch (e.Type) {
            case ServerEventType.ServerChange:
                if(e.Id.Equals(NetworkID.Local_ID)) {
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
            case PlayerEventType.PlayerEnter:
                break;
            case PlayerEventType.PlayerExit:
                break;
            case PlayerEventType.PlayerCast:
                break;
            case PlayerEventType.PlayerCrouch:
                break;
            case PlayerEventType.PlayerDamaged:
                break;
            case PlayerEventType.PlayerDie:
                break;
            case PlayerEventType.PlayerEquipped:
                break;
            case PlayerEventType.PlayerJump:
                break;
            case PlayerEventType.PlayerMove:
                break;
            case PlayerEventType.PlayerPosition:
                break;
            case PlayerEventType.PlayerAnimation:
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
