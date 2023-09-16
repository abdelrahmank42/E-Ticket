using E_Ticket.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Ticket.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //add primary key for many to many relationship
            modelBuilder.Entity<Actor_Movie>().HasKey(actorMovie => new { actorMovie.ActorId, actorMovie.MovieId });

            //make one to many relationship between movie and actor_Movie
            modelBuilder.Entity<Actor_Movie>().HasOne(movie => movie.Movie).WithMany(actorMovie => actorMovie.Actor_Movies).HasForeignKey(movie => movie.MovieId);

            //make one to many relationship between actor and actor_Movie
            modelBuilder.Entity<Actor_Movie>().HasOne(actor => actor.Actor).WithMany(actorMovie => actorMovie.Actor_Movies).HasForeignKey(actor => actor.ActorId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor_Movie> Actors_Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }

        //Orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
