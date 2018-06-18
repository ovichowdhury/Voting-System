using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Voting_System.Attributes;

namespace Voting_System.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        Services service = new Services();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var result = service.GetVoterNIDAndPassword(Convert.ToInt64(collection["nid"]));
            if (result != null && result[0]["nid"].ToString().Equals(collection["nid"].ToString()) && result[0]["password"].ToString().Equals(collection["pass"].ToString()))
            {
                Session["nid"] = collection["nid"].ToString();
                return RedirectToAction("Dashboard");
            }
            else
            {
                TempData["error"] = "Incorrect NID or Password";
                return RedirectToAction("Login");
            }
        }

        [Authentication]
        public ActionResult Dashboard()
        {
            try
            {
                var electionResultSet = service.GetElectionDateAndStatus();

                if (electionResultSet != null && Convert.ToDateTime(electionResultSet[0]["electionDate"]).Year == DateTime.Now.Year && Convert.ToDateTime(electionResultSet[0]["electionDate"]).Month == DateTime.Now.Month && Convert.ToDateTime(electionResultSet[0]["electionDate"]).Day == DateTime.Now.Day && electionResultSet[0]["status"].Equals("in"))
                {
                    if (IsVoteGiven())
                    {
                        return View("AfterVoteView");
                    }
                    else
                    {
                        var seatIdResultSet = service.GetVoterSeatId(Convert.ToInt64(Session["nid"]));

                        var electionIdResultSet = service.GetElectionId();

                        var candidateDetailsResultSet = service.GetCandidatesDetails(seatIdResultSet, electionIdResultSet);

                        var seatNameResultSet = service.GetSeatName(seatIdResultSet);

                        ViewBag.SeatName = seatNameResultSet;
                        ViewBag.Candidates = candidateDetailsResultSet;
                        return View();
                    }
                    
                }
                else
                {
                    ViewBag.Message = "Please wait for next election";
                    return View();
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Login");
            }
            
        }

        [NonAction]
        private bool IsVoteGiven()
        {
            var voterIdResultSet = service.GetVoterId(Convert.ToInt64(Session["nid"]));
            var electionIdResultSet = service.GetElectionId();
            var voteInstanceResultSet = service.GetVoteInstance(Convert.ToInt64(voterIdResultSet[0]["voterId"]), Convert.ToInt64(electionIdResultSet[0]["electionId"]));
            if (voteInstanceResultSet == null)
                return false;
            else
                return true;
        }

        [Authentication, HttpPost]
        public ActionResult Dashboard(FormCollection collection)
        {
            var voterIdResultSet = service.GetVoterId(Convert.ToInt64(Session["nid"]));
            var electionIdResultSet = service.GetElectionId();

            var voteInstanceResultSet = service.GetVoteInstance(Convert.ToInt64(voterIdResultSet[0]["voterId"]), Convert.ToInt64(electionIdResultSet[0]["electionId"]));
            if (voteInstanceResultSet == null)
            {
                service.GiveVoteFor(Convert.ToInt64(collection["vote"]), Convert.ToInt64(voterIdResultSet[0]["voterId"]), Convert.ToInt64(electionIdResultSet[0]["electionId"]));
                return View("AfterVoteView");
            }
            else
                return View("AfterVoteView");

            
        }


        [Authentication]
        public ActionResult Logout()
        {
            if (Session["nid"] != null)
            {
                Session.Remove("nid");
                return RedirectToAction("Login");
            }
            else
                return RedirectToAction("Login");
        }



	}
}