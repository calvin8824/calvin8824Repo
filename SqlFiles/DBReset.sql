Use BinaryTextAdventure
Go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
		where Routine_Name = 'DbReset')
			Drop Procedure DbReset
Go

Create Procedure DbReset As
Begin
	Delete From PlayerCharacter;
	Delete From Outcome;
	Delete From Player;
	Delete From EventChoice;
	Delete From Ending;
	Delete From Scene;
	Delete From Game;
	--Delete From AppUserRole;
	--Delete From LogInModel where UserName in ('testemail@test.com','othertestemail@test.com');
	--Delete From AppRole;

	DBCC CHECKIDENT ('PlayerCharacter', RESEED, 1);
	DBCC CHECKIDENT ('Game', RESEED, 1);
	DBCC CHECKIDENT ('Scene', RESEED, 1);
	DBCC CHECKIDENT ('Player', RESEED, 1);
	DBCC CHECKIDENT ('Outcome', RESEED, 1);
	DBCC CHECKIDENT ('Ending', RESEED, 1);
	DBCC CHECKIDENT ('EventChoice', RESEED, 1);
	
	--Insert Into LogInModel (UserName, PasswordHash)
	--Values
	--('testemail@test.com', 'password'),
	--('othertestemail@test.com', 'guest')

	--Insert Into AppRole (RoleName)
	--Values
	--('admin'),
	--('player'),
	--('dadmin'),
	--('guest')
	
	--Insert Into AppUserRole(UserName, RoleName)
	--Values
	--('testemail@test.com', 'player'),
	--('othertestemail@test.com', 'admin')

	Set Identity_Insert Player On;

	Insert Into Player (PlayerId, PlayerName)
	Values
	('1', 'The Player'),
	('2', 'The Lady'),
	('3', 'The Guy')
		
	Set Identity_Insert Player Off;
		
	Set Identity_Insert Game On;

	Insert Into Game (GameId, GameTitle, IntroText)
	Values
	('1', 'Fantasy Game', 'Hey there, this is a fantasy game.'),
	('2', 'Bullied Student Sim', 'Hey there, this is a school sim game where you get bullied.'),
	('3', 'Space Adventure Pt 13: The Arctonian Terradrax', 'Hey there, this is a space game where space stuff happens in space.')
		
	Set Identity_Insert Game Off;	
	
	Set Identity_Insert Scene On;

	Insert Into Scene (SceneId, GameId, IsStart, SceneName)
	Values
	('1', '1', '1', 'The Foyer'),
	('2', '2', '1', 'The Classroom'),
	('3', '3', '1', 'The Space Restaurant'),
	('4', '3', '1', 'The Thingverse'),
	('5', '3', '0', 'The Space Store'),
	('6', '3', '0', 'The Space Planet'),
	('7', '1', '1', 'The Magic Castle')
		
	Set Identity_Insert Scene Off;

	Set Identity_Insert Ending On;

	Insert Into Ending (EndingId, EndingText)
	Values
	('1', 'The end is at hand.'),
	('2', 'Bad Ending here. Hi.'),
	('3', 'Yo. I''m the good ending.'),
	('4', 'Just end number 4 here. Sup.'),
	('5', 'It''s over. You can leave now.'),
	('6', 'You have bored me. Leave now.')
		
	Set Identity_Insert Ending Off;

	Set Identity_Insert EventChoice On;

	Insert Into EventChoice (EventChoiceId, SceneId, GenerationNumber, EventName, StartText,
	PositiveText, NegativeText, PositiveRoute, NegativeRoute, PositiveButton, NegativeButton,
	PositiveSceneRoute, NegativeSceneRoute, PositiveEndingId, NegativeEndingId)
	Values
	('1', '1', '0', 'Greeting Event', 'You are greeted in the foyer.', 'You are gracious to your host',
	 'You punch numerous holes in the foyer walls. A magical portal opens from one hole and you are teleported somewhere.', '2', null, 'Be gracious.', 'Punch holes in walls.',
	 null, '7', null, null),
	('2', '1', '1', 'Post-Greeting Event', 'You travel from the foyer to the dining room.', 'You enjoy a hearty meal',
	 'You have a heart attack.', null, null, 'Enjoy.', 'Don''t enjoy',
	 null, null, '3', '2'),
	 ('3', '7', '0', 'Fantasy Castle Event', 'You open your eyes to see an immense castle.', 'You bask in the glory of the castle',
	 'You are struck by a falling cherub and die.', null, null, 'Good Choice.', 'Bad Choice',
	 null, null, '1', '6'),
	 ('4', '2', '0', 'Classroom Greeting Event', 'You are greeted in the classroom.', 'You are gracious to your teacher.',
	 'You are ungracious to your teacher.', null, null, 'Be gracious.', 'Be ungracious.',
	 null, null, '5', '4'),
	 ('5', '3', '0', 'Space Greeting Event', 'You are greeted in space.', 'You are gracious to space.',
	 'You snidely scoff at space.', null, null, 'Be gracious.', 'Scoff',
	 null, null, '1', '2')
		
	Set Identity_Insert EventChoice Off;
	
	Set Identity_Insert Outcome On;

	Insert Into Outcome (OutcomeId, EventChoiceId, Positive, Health, Gold)
	Values
	('1', '1', 1, 0, 10),
	('2', '1', 0, -1, 0),
	('3', '2', 1, 1, 0),
	('4', '2', 0, 0, -10),
	('5', '3', 1, 0, 0),
	('6', '3', 0, 1, 5),
	('7', '4', 1, -1, 1),
	('8', '4', 0, 2, 4)
		
	Set Identity_Insert Outcome Off;

	Set Identity_Insert PlayerCharacter On;

	Insert Into PlayerCharacter (CharacterId, PlayerId, SceneId, EventChoiceId, CharacterName, HealthPoints, Gold)
	Values
	('1', '1', '2', '1', 'Player''s Character', '5', '12'),
	('2', '2', '7', '4', 'Lady Character', '3', '4'),
	('3', '3', '5', '5', 'Guy Character', '1', '0')
		
	Set Identity_Insert PlayerCharacter Off;







End
