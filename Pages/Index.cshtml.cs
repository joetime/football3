using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FOOTBALL3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FOOTBALL3.Pages
{
    public class IndexModel : PageModel
    {
        public List<Team> Teams;

        private ApplicationDbContext DB;
        

        public IndexModel (ApplicationDbContext _db)
        {
            DB = _db;

            DB.CreateTeams();
            Teams = DB.Teams
                .Include("HomeGames")
                .Include("AwayGames")
                .OrderBy(t => t.TeamId).ToList();
        }

        public void OnGet()
        {
            int i = DB.Teams.Count();
            ;
        }
    }
}
