using BarberBossManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBossManagement.Infrastructure.DataAccess;
public class BarberBossManagementDbContext : DbContext
// Essa classe herda de DbContext, que é a classe base do Entity Framework Core para manipulação de entidades,
// configuração de conexões, e execução de consultas e comandos SQL
{
    public BarberBossManagementDbContext(DbContextOptions options) : base(options){}
    //Esse construtor recebe um objeto DbContextOptions, que contém informações de configuração, como a string
    // de conexão com o banco de dados, provedores(SQL Server, MySQL) e outras opções de configuração.

    // O construtor chama o construtor da classe base(base(options)) para inicializar o contexto com essas opções.
    // Isso permite que o contexto seja configurado externamente
    public DbSet<Revenue> Revenues { get; set; } 
    //DbSet<Revenue> é a representação de uma coleção de entidades no banco de dados
    //Essa propriedade define um conjunto de entidades Revenue, que corresponde à tabela Revenues no banco de dados
    //A partir dessa coleção, é possível executar operações como inserir, atualizar, deletar etc
    public DbSet<User> Users { get; set; }

    public DbSet<BarberShop> BarberShops { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    // Abaixo nós fazemos as configurações entre relacionamentos no banco de dados
    // Por isso o Entity Framework sabe que ao buscar por uma entidade ele tras outra entidade associada.
    // O EF vai usar essa configuração para identificar que a tabela Revenue possui uma chave estrangeira
    // BarberShopId, que faz referencia a tabela BarberShop
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.BarberShops) // Um usuário tem muitas barbearias
            .WithOne(b => b.User) // Cada barbearia pertence a um usuário
            .HasForeignKey(b => b.UserId); // A chave estrangeira na tabela de barbearias é UserId


        modelBuilder.Entity<BarberShop>()
            .HasMany(r => r.Revenues) // Uma barbearia tem muitas receitas
            .WithOne(b => b.BarberShop) // Cada receita pertence a uma barbearia
            .HasForeignKey(r => r.BarberShopId) // A chave estrangeira na tabela de receita é BarberShopId
            .OnDelete(DeleteBehavior.Cascade); // Quando uma barbearia for excluída todas as receitas vão juntas
    }

}

// O DbContext rastreia mudanças nas entidades(Revenue, User) e sabe quando salvá-las ou modifica-las no
//banco de dados

// Quando chamamos métodos como SaveChanges(), o DbContext aplica as alterações feitas às entidades