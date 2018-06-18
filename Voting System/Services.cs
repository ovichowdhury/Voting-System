using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Voting_System
{
    public class Services
    {
        Repository repo;

        public Services()
        {
            repo = new Repository();
        }

        public List<Dictionary<string, object>> GetVoterNIDAndPassword(long nid)
        {
            var param = new Dictionary<string, object>(){
                {"nid", nid}
            };
            var result = repo.Db.Select(SQL.getNIDAndPassword, param);
            return result;
        }

        public List<Dictionary<string, object>> GetElectionDateAndStatus()
        {
            var year = new Dictionary<string, object>(){
                {"year",DateTime.Now.Year}
            };
            var electionResultSet = repo.Db.Select(SQL.getElectionDateAndStatus, year);
            return electionResultSet;
        }

        public List<Dictionary<string, object>> GetVoterSeatId(long nid)
        {
            var param = new Dictionary<string, object>(){
                    {"nid", nid}
            };
            var seatIdResultSet = repo.Db.Select(SQL.getVoterSeatId, param);
            return seatIdResultSet;
        }

        public List<Dictionary<string, object>> GetElectionId()
        {
            var param2 = new Dictionary<string, object>(){
                    {"year", DateTime.Now.Year}
            };
            var electionIdResultSet = repo.Db.Select(SQL.getElectionId, param2);
            return electionIdResultSet;
        }

        public List<Dictionary<string, object>> GetCandidatesDetails(List<Dictionary<string, object>> seatIdResultSet, List<Dictionary<string, object>> electionIdResultSet)
        {
            var param3 = new Dictionary<string, object>(){
                    {"seatId", seatIdResultSet[0]["seatId"]},
                    {"electionId", electionIdResultSet[0]["electionId"]}
            };
            var candidateDetailsResultSet = repo.Db.Select(SQL.getCandidatesDetails, param3);
            return candidateDetailsResultSet;
        }

        public List<Dictionary<string, object>> GetSeatName(List<Dictionary<string, object>> seatIdResultSet)
        {
            var param4 = new Dictionary<string, object>(){
                    {"seatId", seatIdResultSet[0]["seatId"]}
            };
            var seatNameResultSet = repo.Db.Select(SQL.getSeatName, param4);
            return seatNameResultSet;
        }


        public List<Dictionary<string, object>> GetVoterId(long nid)
        {
            var param = new Dictionary<string, object>(){
                {"nid", nid}
            };

            var result = repo.Db.Select(SQL.getVoterId, param);
            return result;
        }

        public void GiveVoteFor(long candidateId, long voterId, long electionId)
        {
            var param = new Dictionary<string, object>(){
                {"voterId", voterId},
                {"electionId", electionId}
            };
            repo.Db.Execute(SQL.insertVote, param);

            var param2 = new Dictionary<string, object>(){
                {"candidateId", candidateId}
            };

            repo.Db.Execute(SQL.updateTotalVote, param2);
        }

        public List<Dictionary<string, object>> GetVoteInstance(long voterId, long electionId)
        {
            var param = new Dictionary<string, object>(){
                {"voterId", voterId},
                {"electionId", electionId}
            };
            var result = repo.Db.Select(SQL.getVoteInstance, param);
            return result;
        }

    }
}