﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using Pbalut.RealTimeHomeController.Shared.AppData;
using Pbalut.RealTimeHomeController.Shared.Enums.Extensions;
using Pbalut.RealTimeHomeController.Shared.Enums.Groups;
using Pbalut.RealTimeHomeController.Shared.Enums.Hubs;
using Pbalut.RealTimeHomeController.Shared.Models;
using Pbalut.RealTimeHomeController.Shared.Models.Lights;
using Pbalut.Uwp.Commons.Exceptions.SignalR;
using Pbalut.Uwp.Commons.SignalR;

namespace Pbalut.RealTimeHomeController.HardwareController.HubProxy
{
    public class LightHubProxy : HubProxyBase
    {
        internal event EventHandler<LightServerRequest> RequestToServerEvent;

        public LightHubProxy()
            : base(EHub.Light.GetHubName())
        {
        }

        public async Task Start()
        {
            //SetListeaners();
            await Init();
            HubProxy.On<LightServerRequest>(EHubMethod.LightChangeStateServerRequest.GetClientName(),
                clientRequest => { RequestToServerEvent?.Invoke(this, clientRequest); });
            await JoinToGroup();
        }

        private async Task JoinToGroup()
        {
            try
            {
                if (HubConnection.State != ConnectionState.Connected)
                    throw new InvalidHubConnectionStateException(EndPoint.HubUrl, HubConnection.State);
                await HubProxy.Invoke(EHubMethod.LightJoinGroup.GetServerName(), EGroup.Server);
            }
            catch (Exception ex)
            {
                //TODO            
            }
        }

        private void SetListeaners()
        {
            RequestToServerEvent += async (p, q) =>
            {
                await AA(q);
            };
        }

        private async Task AA(LightServerRequest request)
        {

        }
    }
}