using CommonServiceLocator;
using Forum.BLL.Posts;
using Forum.BLL.Threads;
using Forum.BLL.Topics;
using Forum.BLL.Users;
using Forum.DAL.Base;
using Forum.DAL.Posts;
using Forum.DAL.Threads;
using Forum.DAL.Topics;
using Forum.DAL.Users;
using System;
using System.Configuration;
using Unity;
using Unity.Injection;

namespace Forum.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
                       
            container.RegisterType<IRepositoryConfigs, RepositoryConfigs>();

            container.RegisterType<RepositoryConfigs>(new 
                         InjectionConstructor(ConfigurationManager.ConnectionStrings["ForumDBFile"].ConnectionString));

            container.RegisterType<IUsersRepository, UsersRepository>();
            container.RegisterType<IUsersService, UsersService>();

            container.RegisterType<ITopicsRepository, TopicsRepository>();
            container.RegisterType<ITopicsService, TopicsService>();

            container.RegisterType<IThreadsRepository, ThreadsRepository>();
            container.RegisterType<IThreadsService, ThreadsService>();

            container.RegisterType<IPostsRepository, PostsRepository>();
            container.RegisterType<IPostsService, PostsService>();
        }
    }
}