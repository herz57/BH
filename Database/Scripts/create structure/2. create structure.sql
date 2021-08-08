use BH;

create table [Domains]
(
	[DomainType] int primary key,
	[Description] varchar(max) not null
);

create table Machines 
(
	MachineId int primary key identity,
	[DomainType] int references [Domains]([DomainType]) not null
);

create table Tickets
(
    TicketId int primary key identity,
    MachineId int references Machines(MachineId) not null,
    Cost int not null,
    Win int not null,
    PlayedOut bit not null,
    Symbols nvarchar(max) not null
);

create table Users
(
	UserId int primary key identity,
	UserName varchar unique not null,
	Password varchar(max) not null
);

create table Roles
(
	RoleId int primary key identity,
	Description nvarchar(max) not null
);

create table Profiles
(
	ProfileId int primary key identity,
	UserId int references Users(UserId) unique not null,
	Balance bigint not null
);

create table UsersRoles
(
	UserId int references Users(UserId) not null,
	RoleId int references Roles(RoleId) not null,
	primary key(UserId, RoleId)
);
