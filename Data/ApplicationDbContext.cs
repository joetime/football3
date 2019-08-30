using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FOOTBALL3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.HomeGames)
                .WithOne(g => g.HomeTeam);

            modelBuilder.Entity<Team>()
                .HasMany(t => t.AwayGames)
                .WithOne(g => g.AwayTeam);

            modelBuilder.Entity<Game>()
                .Property(g => g.Spread)
                .HasDefaultValue(0);

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

       
        

        public void CreateTeams()
        {
            ApplicationDbContext DB = this;
            Team test = DB.Teams.Find("ARI");
            if (test != null) return;

            DB.Teams.Add(Team.Create("ARI", "Arizona", "Cardinals"));
            DB.Teams.Add(Team.Create("ATL", "Atlanta", "Falcons"));
            DB.Teams.Add(Team.Create("BAL", "Baltimore", "Ravens"));
            DB.Teams.Add(Team.Create("BUF", "Buffalo", "Bills"));
            DB.Teams.Add(Team.Create("CAR", "Carolina", "Panthers"));
            DB.Teams.Add(Team.Create("CHI", "Chicago", "Bears"));
            DB.Teams.Add(Team.Create("CIN", "Cincinnati", "Bengals"));
            DB.Teams.Add(Team.Create("CLE", "Cleveland", "Browns"));
            DB.Teams.Add(Team.Create("DAL", "Dallas", "Cowboys"));
            DB.Teams.Add(Team.Create("DEN", "Denver", "Broncos"));
            DB.Teams.Add(Team.Create("DET", "Detroit", "Lions"));
            DB.Teams.Add(Team.Create("GB", "Green Bay", "Packers"));
            DB.Teams.Add(Team.Create("HOU", "Houston", "Texans"));
            DB.Teams.Add(Team.Create("IND", "Indianapolis", "Colts"));
            DB.Teams.Add(Team.Create("JAX", "Jacksonville", "Jaguars"));
            DB.Teams.Add(Team.Create("KC", "Kansas City", "Chiefs"));
            DB.Teams.Add(Team.Create("MIA", "Miami", "Dolphins"));
            DB.Teams.Add(Team.Create("MIN", "Minnesota", "Vikings"));
            DB.Teams.Add(Team.Create("NE", "New England", "Patriots"));
            DB.Teams.Add(Team.Create("NO", "New Orleans", "Saints"));
            DB.Teams.Add(Team.Create("NYG", "New York", "Giants"));
            DB.Teams.Add(Team.Create("NYJ", "New York", "Jets"));
            DB.Teams.Add(Team.Create("OAK", "Oakland", "Raiders"));
            DB.Teams.Add(Team.Create("PHI", "Philadelphia", "Eagles"));
            DB.Teams.Add(Team.Create("PIT", "Pittsburgh", "Steelers"));
            DB.Teams.Add(Team.Create("LAC", "Los Angeles", "Chargers"));
            DB.Teams.Add(Team.Create("SEA", "Seattle", "Seahawks"));
            DB.Teams.Add(Team.Create("SF", "San Francisco", "49ers"));
            DB.Teams.Add(Team.Create("LAR", "Los Angeles", "Rams"));
            DB.Teams.Add(Team.Create("TB", "Tampa Bay", "Buccaneers"));
            DB.Teams.Add(Team.Create("TEN", "Tennessee", "Titans"));
            DB.Teams.Add(Team.Create("WAS", "Washington", "Redskins"));

            DB.SaveChanges();
        }
    }

    public class Game
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GameId { get; set; }
        public int Week { get; set; }
        public DateTime GameTimeZ { get; set; }
        public int? HomeScore { get; set; }
        public int? AwayScore { get; set; }

        public int Spread { get; set; }

        [ForeignKey("HomeTeamId")]
        public Team HomeTeam { get; set; }

        [ForeignKey("AwayTeamId")]
        public Team AwayTeam { get; set; }

        [ForeignKey("FavoriteTeamId")]
        public Team Favorite { get; set; }
    }

    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TeamId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        [NotMapped]
        public string LogoURL { get {
                return $"https://imagecomposer.nfl.com/image/fetch/q_80,h_90,w_90,c_fill/https://static.nfl.com/static/site/img/logos/500x500/{TeamId}.png";
            } }

        public static Team Create (string Abbr, string City, string Name)
        {
            Team t = new Team();
            t.TeamId = Abbr;
            t.City = City;
            t.Name = Name;

            return t;
        }

        public ICollection<Game> HomeGames { get; set; }
        public ICollection<Game> AwayGames { get; set; }
    }
}
