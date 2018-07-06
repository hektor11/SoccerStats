using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using RestSharp;
using SoccerStats.Models;
using SoccerStats.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SoccerStats.Controllers
{
    
    
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RestrictedAccess()
        {
            return View();
        }

        /// <summary>
        ///         This is used to display total available leagues and output them as a list for the user to
        ///         choose from.
        /// </summary>
        public ActionResult LeagueSelection()
        {
            var client = new RestClient("http://api.football-data.org/v1/competitions");
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            List<Competition> model = JsonConvert.DeserializeObject<List<Competition>>(response.Content);
            return View(model);
        }

        /// <summary>
        ///         This is used to request the league table model which is used to list the 
        ///         placement results for the leage(i.e 1st, 2nd, etc)
        /// </summary>


        public ActionResult LeagueStandings(int? id)
        {
            if (id == null)
            {
                return View("RestrictedAccess");
            }

            else
            {
                var client = new RestClient("http://api.football-data.org/v1/competitions/" + id.ToString() + "/leagueTable");
                var request = new RestRequest(Method.GET);
                request.RequestFormat = DataFormat.Json;
                var response = client.Execute(request);
                LeagueStanding model = JsonConvert.DeserializeObject<LeagueStanding>(response.Content);
                return View(model);
            }

        }

        /// <summary>
        /// This returns the model for a team (team crest, name, etc)
        /// </summary>

        public ActionResult TeamView(string url)
        {
            if (url == null)
            {
                return View("RestrictedAccess");
            }

            else
            {
                List<Object> teamDetails = new List<Object>();
                var teamClient = new RestClient(url);
                var teamRequest = new RestRequest(Method.GET);
                teamRequest.RequestFormat = DataFormat.Json;
                var response = teamClient.Execute(teamRequest);
                Team teamModel = JsonConvert.DeserializeObject<Team>(response.Content);
                return View(teamModel);
            }

        }

        /// <summary>
        /// This is used by the Angular controller to request team specific player details
        /// </summary>
        public JsonResult GetPlayerData(string teamUrl)
        {
            List<Object> teamDetails = new List<Object>();
            var client= new RestClient(teamUrl);
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            var r = client.Execute(request);
            PlayerList model = JsonConvert.DeserializeObject<PlayerList>(r.Content);
            Object response = model.players; 
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method is used to set a logged-in users 'teamUrl' to track their team
        /// </summary>

        public ActionResult GetTeamData(string teamUrl)
        {
            if (!Request.IsAuthenticated)
            {
                return View();
            }

            else
            {
                ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());
                currentUser.teamUrl = teamUrl;
                db.SaveChanges();
                return View();
            } 
          
        }

        /// <summary>
        /// This method is used to create the personalized team view for a logged in user
        /// </summary>

        public ActionResult viewMyTeam()
        {
            ApplicationUser currentUser = db.Users.Find(User.Identity.GetUserId());

            if(currentUser.teamUrl == null)
            {
                return View("RestrictedAccess");
            }

            else
            {
                var teamClient = new RestClient(currentUser.teamUrl);
                var teamRequest = new RestRequest(Method.GET);
                teamRequest.RequestFormat = DataFormat.Json;
                var response = teamClient.Execute(teamRequest);
                Team teamModel = JsonConvert.DeserializeObject<Team>(response.Content);
                return View("TeamView", teamModel);
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}