using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Voting_System
{
    public static class SQL
    {
        public static string getNIDAndPassword = "select nid, password from voters where nid = @nid";

        public static string getElectionDateAndStatus = "select electionDate, status from election where year(electionDate) = @year";

        public static string getCandidatesDetails = @"select partySymbol, symbol, voterName, partyName, gender, totalVote, candidateId
                                                      from (candidates inner join voters
                                                      on candidates.voterId = voters.voterId)
                                                      inner join parties
                                                      on candidates.partyId = parties.partyId
                                                      where candidates.seatId = @seatId and candidates.electionId = @electionId;";

        public static string getVoterSeatId = "select seatId from voters where nid = @nid";

        public static string getElectionId = "select electionId from election where year(electionDate) = @year";

        public static string getSeatName = "select seatName from seats where seatId = @seatId";

        public static string getVoterId = "select voterId from voters where nid = @nid";

        public static string insertVote = "insert into votes values(@voterId, @electionId, 1)";

        public static string updateTotalVote = "update candidates set totalVote = totalVote + 1 where candidateId = @candidateId";

        public static string getVoteInstance = "select * from votes where voterId = @voterId and electionId = @electionId";
    }
}