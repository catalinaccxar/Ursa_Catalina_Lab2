using Microsoft.EntityFrameworkCore;

public class Ursa_Catalina_Lab2Context(DbContextOptions<Ursa_Catalina_Lab2Context> options) : DbContext(options)
{
    public DbSet<Ursa_Catalina_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Ursa_Catalina_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    public DbSet<Ursa_Catalina_Lab2.Models.Authors> Authors { get; set; } = default!;
    public DbSet<Ursa_Catalina_Lab2.Models.Category> Category { get; set; } = default!;


}
