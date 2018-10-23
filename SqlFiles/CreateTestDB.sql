SET NOCOUNT ON
Go

USE master
Go

if exists (select * from sysdatabases where name='BinaryTextAdventureTest')
		drop database BinaryTextAdventureTest
Go

Create Database BinaryTextAdventureTest
Go

Use BinaryTextAdventureTest
Go

if exists (select * from sys.tables where name='PlayerCharacter')
		drop table PlayerCharacter
Go

if exists (select * from sys.tables where name='Ending')
		drop table Ending
Go

if exists (select * from sys.tables where name='Player')
		drop table Player
Go

if exists (select * from sys.tables where name='Outcome')
		drop table Outcome
Go

if exists (select * from sys.tables where name='EventChoice')
		drop table EventChoice
Go

if exists (select * from sys.tables where name='Scene')
		drop table Scene
Go

if exists (select * from sys.tables where name='Game')
		drop table Game
Go

if exists (select * from sys.tables where name='AppUserRole')
		drop table AppUserRole
Go

if exists (select * from sys.tables where name='LogInModel')
		drop table LogInModel
Go

if exists (select * from sys.tables where name='AppRole')
		drop table AppRole
Go

create table AppRole (
	RoleName nvarchar(50) primary key
)
GO

Create Table LogInModel (
	UserName nvarchar(50) primary key,
	PasswordHash nvarchar(512) not null	
)
Go

create table AppUserRole (
	UserName nvarchar(50) not null,
	RoleName nvarchar(50) not null,
	primary key (UserName, RoleName),
	foreign key (UserName) references LogInModel(UserName),
	foreign key (RoleName) references AppRole(RoleName)
)
GO

Create Table Player (
	PlayerId int identity(1,1) primary key not null,
	PlayerName nVarChar(30) not null
)
Go

Create Table Game (
	GameId int identity(1,1) primary key not null,
	GameTitle nVarChar(75) not null,
	IntroText nVarChar(max) not null
)
Go

Create Table Scene (
	SceneId int identity(1,1) primary key not null,
	GameId int not null foreign key references Game(GameId),
	IsStart bit not null,
	SceneName nVarChar(50) not null
)
Go

Create Table Ending(
	EndingId int identity(1, 1) primary key not null,
	EndingText nVarChar(max) not null
)
Go

Create Table EventChoice (
	EventChoiceId int identity(1,1) primary key not null,
	SceneId int not null foreign key references Scene(SceneId),
	GenerationNumber int not null,
	EventName nVarChar(50) not null,
	StartText nVarChar(max) not null,
	PositiveText nVarChar(max) not null,
	NegativeText nVarChar(max) not null,
	PositiveRoute int null foreign key references EventChoice(EventChoiceId),
	NegativeRoute int null foreign key references EventChoice(EventChoiceId),
	PositiveButton nVarChar(128) not null,
	NegativeButton nVarChar(128) not null,
	PositiveSceneRoute int null foreign key references Scene(SceneId),
	NegativeSceneRoute int null foreign key references Scene(SceneId),
	PositiveEndingId int null foreign key references Ending(EndingId),
	NegativeEndingId int null foreign key references Ending(EndingId) 

)
Go

Create Table Outcome (
	OutcomeId int identity(1,1) primary key not null,
	EventChoiceId int not null foreign key references EventChoice(EventChoiceId),
	Positive bit not null,
	Health int not null default 0,
	Gold int not null default 0
)
Go

Create Table PlayerCharacter (
	CharacterId int identity(1,1) primary key not null,
	PlayerId int not null foreign key references Player(PlayerId),
	SceneId int not null foreign key references Scene(SceneId),
	EventChoiceId int not null foreign key references EventChoice(EventChoiceId),
	CharacterName nVarChar(30) not null,
	HealthPoints int not null default 3,
	Gold int not null default 0
)
Go












