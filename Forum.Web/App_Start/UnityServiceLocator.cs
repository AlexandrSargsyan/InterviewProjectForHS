using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace Forum.Web.App_Start
{
    public class UnityServiceLocator
    {
        public static T Resolve<T>() => UnityConfig.Container.Resolve<T>();
    }
}