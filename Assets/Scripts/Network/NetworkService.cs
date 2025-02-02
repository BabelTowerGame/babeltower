﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;
using Grpc.Core;
using Tob;
using UniRx;

public class NetworkService : MonoBehaviour {

    public static NetworkService Instance = null;

    public static bool isServer = false;

    private bool isActive = false;

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
        DontDestroyOnLoad(this);
    }

    void OnDestroy() {
        Stop();
    }
    

    private void ServiceStart() {
        NetworkID.Local_ID = NetworkID.generateNewID(128);
        this.channel = new Channel("51.15.190.233:16882", ChannelCredentials.Insecure);
        this.client = new Tob.ToB.ToBClient(this.channel);
        this.metadata = new Metadata {
            { "id", NetworkID.Local_ID}
        };

    }

    private void ServiceRun() {
        Debug.Log("NetworkService: Run");
        this.StartListen();
        this.sendhandle = this.client.Publish(metadata);

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
        //Debug.Log("NetworkService: StartSendWindow");
        //Debug.Log("[Sent] EventType:" + e.Topic);

        while (!isActive) { }
        
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
                isActive = true;
                if (e.Id.Equals(NetworkID.Local_ID)) {
                    NetworkService.isServer = true;
                }
                break;
            case ServerEventType.ServerYield:
                break;
        }
    }

    private void OnPlayerEvent(PlayerEvent e) {
        NetworkPlayerManager mgr;
        try {
            mgr = GameObject.FindGameObjectWithTag("NetworkPlayer")
                .GetComponent<NetworkPlayerManager>();
        } catch (Exception ee) {
            return;
        }

        Debug.Log("[!!PlayerEvent!!] EventType:" + e.Type);
        switch (e.Type) {
            case PlayerEventType.PlayerEnter:
                mgr.OnPlayerEnter(e);
                break;
            case PlayerEventType.PlayerExit:
                mgr.OnPlayerExit(e);
                break;
            case PlayerEventType.PlayerCast:
                mgr.OnPlayerCast(e);
                break;
            case PlayerEventType.PlayerCrouch:
                mgr.OnPlayerCrouch(e);
                break;
            case PlayerEventType.PlayerDamaged:
                mgr.OnPlayerDamaged(e);
                break;
            case PlayerEventType.PlayerDie:
                mgr.OnPlayerDie(e);
                break;
            case PlayerEventType.PlayerEquipped:
                mgr.OnPlayerEquipped(e);
                break;
            case PlayerEventType.PlayerJump:
                mgr.OnPlayerJump(e);
                break;
            case PlayerEventType.PlayerMove:
                mgr.OnPlayerMove(e);
                break;
            case PlayerEventType.PlayerPosition:
                mgr.OnPlayerPosition(e);
                break;
            case PlayerEventType.PlayerAnimation:
                mgr.OnPlayerAnimation(e);
                break;
            default:
                break;
        }

    }


    // TODO: LINK FUNCTIONS
    private void OnMonsterEvent(MonsterEvent e) {
		NetworkMonsterManager mgr = GameObject.FindGameObjectWithTag("NetworkMonster")
			.GetComponent<NetworkMonsterManager>();
        Debug.Log("[MonsterEvent] EventType:" + e.Type);
        switch (e.Type) {
		case MonsterEventType.MonsterAttack:
			mgr.OnMonsterAttack (e);
                break;
		case MonsterEventType.MonsterDestroy:
			mgr.OnMonsterDestory (e);
                break;
		case MonsterEventType.MonsterDie:
			mgr.OnMonsterDie (e);
                break;
		case MonsterEventType.MonsterLoot:
			mgr.OnMonsterLoot (e);
                break;
		case MonsterEventType.MonsterLootResult:
			mgr.OnMonsterLootResult (e);
                break;
		case MonsterEventType.MonsterMove:
			mgr.OnMonsterMove (e);
                break;
		case MonsterEventType.MonsterSpawn:
			mgr.OnMonsterSpawn (e);
                break;
		case MonsterEventType.MonsterBack:
			mgr.OnMonsterBack (e);
				break;
        }
    }

}
