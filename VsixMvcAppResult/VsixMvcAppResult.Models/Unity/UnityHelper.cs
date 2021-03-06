﻿using System;
using Microsoft.Practices.Unity;
using VsixMvcAppResult.Models.Configuration;

namespace VsixMvcAppResult.Models.Unity
{
    public static class DependencyFactory
    {
        //public static UnityContainerAvailable UnityContainerDefault = UnityContainerAvailable.Real;
        private static IUnityContainer _unity;

        public static void SetUnityContainerProviderFactory(IUnityContainer container)
        {
            if (DependencyFactory._unity == null)
            {
                DependencyFactory._unity = container;
            }
            else
            {
                //throw new Exception("DependencyFactory should not be changed once intialized");
            }
        }

        public static T Resolve<T>()
        {
            return DependencyFactory._unity.Resolve<T>();
        }
    }

    public enum BackEndUnityContainerAvailable
    {
        Real,
        //MockDALDevelopment
    }

    public enum FrontEndUnityContainerAvailable
    {
        ProxiesToCustomHost,
        ProxiesToAzure
        //MockDALDevelopment
    }
}