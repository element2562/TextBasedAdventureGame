delete from Zone;
delete from Player;
delete from Item;
delete from Monster;
delete from Event;
delete from Npc;
delete from Quest;

alter table Player drop constraint [FK_PlayerZone];
alter table Item drop constraint [FK_PlayerItem];
alter table Monster drop constraint [FK_MonsterZone];
alter table Event drop constraint [FK_EventMonster];
alter table Event drop constraint [FK_EventItem];
alter table Event drop constraint [FK_EventZone];
alter table Quest drop constraint [FK_QuestNpc];

drop table if exists Zone;
drop table if exists Player;
drop table if exists Item;
drop table if exists Monster;
drop table if exists Event;
drop table if exists Npc;
drop table if exists Quest;

create table Zone (
	ZoneId integer not null primary key identity,
	ZoneName nvarchar(25)
);

create table Player (
	PlayerId integer not null primary key identity,
	PlayerName nvarchar(50) not null,
	Level integer not null,
	Health integer not null,
	MaxHealth integer not null,
	Strength integer not null,
	Defense integer not null,
	Gold integer not null,
	Experience integer not null,
	CurrentZoneId integer not null,
	constraint FK_PlayerZone foreign key(CurrentZoneId) references Zone(ZoneId)
);

create table Item (
	ItemId integer not null primary key identity,
	ItemName nvarchar(50) not null,
	LevelRequirement integer not null,
	ItemType nvarchar(15) not null,
	Equipped bit not null,
	StrengthBonus integer,
	DefenseBonus integer,
	HealthBonus integer,
	BuyPrice integer not null,
	PlayerId integer,
	constraint FK_PlayerItem foreign key(PlayerId) references Player(PlayerId)
);

create table Monster (
	MonsterId integer not null primary key identity,
	MonsterName nvarchar(25) not null,
	Level integer not null,
	Health integer not null,
	MaxHealth integer not null,
	ZoneId integer not null,
	constraint FK_MonsterZone foreign key(ZoneId) references Zone(ZoneId)
);

create table Event (
	EventId integer not null primary key identity,
	EventSummary nvarchar(500),
	EventAlreadyEncountered bit not null,
	EventPassed bit not null,
	MonsterId integer,
	ItemId integer,
	ZoneId integer not null,
	constraint FK_EventMonster foreign key(MonsterId) references Monster(MonsterId),
	constraint FK_EventItem foreign key(ItemId) references Item(ItemId),
	constraint FK_EventZone foreign key(ZoneId) references Zone(ZoneId)
);

create table Npc (
	NpcId integer not null primary key identity,
	NpcName nvarchar(25) not null,
	IsMerchant bit not null,
	GivesQuests bit not null,
);

create table Quest (
	QuestId integer not null primary key identity,
	XpReward integer not null,
	GoldReward integer not null,
	IsComplete bit not null,
	NpcId integer not null,
	constraint FK_QuestNpc foreign key(NpcId) references Npc(NpcId)
);

drop procedure if exists [dbo].[SP_Create_Player];

GO

CREATE PROCEDURE [dbo].[SP_Create_Player]    
	@PlayerName nvarchar(50),
	@Level int,
	@Health int,
	@MaxHealth int,
	@Strength int,
	@Defense int,
	@Gold int,
	@Experience int,
	@CurrentZoneId int

AS    
	BEGIN    
 DECLARE @PlayerId as BIGINT  
		INSERT  INTO [Player]    
				(PlayerName, [Level], Health, MaxHealth, Strength, Defense, Gold, Experience, CurrentZoneId)    
		VALUES  ( @PlayerName, @Level, @Health, @MaxHealth, @Strength, @Defense, @Gold, @Experience, @CurrentZoneId);   
	SET @PlayerId = SCOPE_IDENTITY();   
		SELECT  @PlayerId AS PlayerID;    
	END;    

GO

drop procedure if exists [dbo].[SP_Update_Player];

GO

CREATE PROCEDURE [dbo].[SP_Update_Player] 
	@PlayerId INT,   
	@PlayerName nvarchar(50),
	@Level int,
	@Health int,
	@MaxHealth int,
	@Strength int,
	@Defense int,
	@Gold int,
	@Experience int,
	@CurrentZoneId int  
AS    
	BEGIN    

  UPDATE [Player] 
  SET PlayerName = @PlayerName,
  [Level] = @Level,
  Health = @Health,
  MaxHealth = @MaxHealth,
  Strength = @Strength,
  Defense = @Defense,
  Gold = @Gold,
  Experience = @Experience,
  CurrentZoneId = @CurrentZoneId
  WHERE PlayerId = @PlayerId 
			 
	END;    