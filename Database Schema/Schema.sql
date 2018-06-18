-- Election Table
create table election
(
	electionId int identity(1,1) primary key,
	electionDate date,
	status varchar(20) default 'pre',
	constraint chk_elc_status check(status in('pre', 'in', 'post'))
)

-- Seats table
create table seats
(
	seatId int identity(1,1) primary key,
	seatName nvarchar(200) unique
)

-- party table 
create table parties
(
	partyId int identity(1,1) primary key,
	partyName nvarchar(200) unique,
	partySymbol ntext
)

-- voters table
create table voters
(
	voterId int identity(1,1) primary key,
	voterName ntext,
	dob date,
	gender nvarchar(15),
	address ntext,
	nid bigint unique,
	password ntext,
	seatId int,
	constraint fk_voter_seat foreign key(seatId) references seats(seatId)
)


-- candidates table
create table candidates
(
	candidateId int identity(1,1) primary key,
	totalVote int,
	voterId int,
	electionId int,
	seatId int,
	partyId int null,
	symbol ntext null,
	constraint fk_can_voter foreign key(voterId) references voters(voterId),
	constraint fk_can_election foreign key(electionId) references election(electionId),
	constraint fk_can_seat foreign key(seatId) references seats(seatId)
)

-- vote table
create table votes
(
	voterId int not null,
	electionId int not null,
	status int default 0,
	constraint fk_vote_voter foreign key(voterId) references voters(voterId),
	constraint fk_vote_election foreign key(electionId) references election(electionId),
	constraint chk_vote_status check(status in(0,1)),
	constraint pk_compositeKey primary key(voterId, electionId)
)
	