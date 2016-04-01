﻿using VsixMvcAppResult.Models.Configuration;
using VsixMvcAppResult.Models.Unity;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using VsixMvcAppResult.Models.Enumerations;
using System.Net;

namespace VsixMvcAppResult.Models.ProxyProviders
{
    public class ClientChannelAzureInternalRoleInitializer<TChannel> : ClientChannelInitializer<TChannel>
    {
        public ClientChannelAzureInternalRoleInitializer() : base() { }

        protected override ChannelFactory<TChannel> ChannelFactoryInit(ChannelFactory<TChannel> channelFactory)
        {
            IPEndPoint backendIPEndPoint = ApplicationConfigurationAzure.AzureRolesConfigurationSection.BackEndGetIpEndpoint();
            Uri originalUri = channelFactory.Endpoint.Address.Uri;
            Uri azureHostUri = new Uri(originalUri.ToString().Replace(originalUri.Authority, backendIPEndPoint.ToString()));
            channelFactory.Endpoint.Address = new EndpointAddress(azureHostUri);

            return channelFactory;
        }
    }
}