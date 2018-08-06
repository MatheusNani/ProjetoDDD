using Ninject;
using Ninject.Web.Common.WebHost;
using ProjetoModeloDDD.Application;
using ProjetoModeloDDD.Application.Interface;
using ProjetoModeloDDD.Domain.Interfaces.Repositories;
using ProjetoModeloDDD.Domain.Interfaces.Services;
using ProjetoModeloDDD.Domain.Services;
using ProjetoModeloDDD.Infra.Data.Repositories;
using ProjetoModeloDDD.MVC.AutoMapper;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ProjetoModeloDDD.MVC
{
	public class MvcApplication : NinjectHttpApplication
	{

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}

		protected override IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			RegisterServices(kernel);
			return kernel;
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private void RegisterServices(IKernel kernel)
		{
			kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(IAppServiceBase<>));
			kernel.Bind<IClienteAppService>().To<ClienteAppService>();
			kernel.Bind<IProdutoAppService>().To<ProdutoAppService>();

			kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
			kernel.Bind<IClienteService>().To<ClienteService>();
			kernel.Bind<IProdutoService>().To<ProdutoService>();

			kernel.Bind(typeof(IRepositoyBase<>)).To(typeof(RepositoryBase<>));
			kernel.Bind<IClienteRepository>().To<ClienteRepository>();
			kernel.Bind<IProdutoRepository>().To<ProdutoRepository>();

		}


		protected override void OnApplicationStarted()
		{
			base.OnApplicationStarted();
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AutoMapperConfig.RegisterMappings();
		}

		/*protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AutoMapperConfig.RegisterMappings();
		}*/
	}
}
