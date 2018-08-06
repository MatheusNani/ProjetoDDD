using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Infra.Data.EntityConfig;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace ProjetoModeloDDD.Infra.Data.Contexto
{
	public class ProjetoModeloContext : DbContext
	{
		public ProjetoModeloContext()
			: base("ProjetoModeloDDD")
		{

		}

		public DbSet<Cliente> Clientes { get; set; }
		public DbSet<Produto> Produtos { get; set; }


		/*Mudar a forma de como o entity cria campos*/
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
			modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

			modelBuilder.Properties()
				.Where(p => p.Name == p.ReflectedType.Name + "Id")
				.Configure(p => p.IsKey());

			/*Todos os campos String vão ser Varchar ao inves de Nvarchar
			 e todos com tamanho maximo 100*/
			modelBuilder.Properties<string>()
				.Configure(p => p.HasColumnType("varchar"));

			modelBuilder.Properties<string>()
				.Configure(p => p.HasMaxLength(100));

			modelBuilder.Configurations.Add(new ClienteConfiguration());
			modelBuilder.Configurations.Add(new ProdutoConfiguration());
		}

		public override int SaveChanges()
		{
			const string DataCadastro = "DataCadastro";
			foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(DataCadastro) != null))
			{
				if (entry.State == EntityState.Added)
				{
					entry.Property(DataCadastro).CurrentValue = DateTime.Now;
				}
				if (entry.State == EntityState.Modified)
				{
					entry.Property(DataCadastro).IsModified = false;
				}
			}

			return base.SaveChanges();
		}
	}

}
