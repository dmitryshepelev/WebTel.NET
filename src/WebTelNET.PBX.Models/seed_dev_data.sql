INSERT INTO "ZadarmaAccounts"
	("Id", "SecretKey", "UserId", "UserKey")
VALUES
	('c56ad931-c854-47ca-9ae0-587561eef34c', '7ad629bd16a93905ded2', '60a41f98-23a7-4aed-adc5-0cc2990b2207', '097e0fc063d054d8d5cb');


INSERT INTO "NotificationTypes"
	("Id", "Name")
VALUES
	(1, 'NotifyStart'),
	(2, 'NotifyInternal'),
	(3, 'NotifyEnd'),
	(4, 'NotifyOutStart'),
	(5, 'NotifyOutEnd');


INSERT INTO "DispositionTypes"
	("Id", "Name")
VALUES
	(1, 'answered'),
	(2, 'busy'),
	(3, 'cancel'),
	(4, 'no answer'),
	(5, 'failed'),
	(6, 'no money'),
	(7, 'unallocated number'),
	(8, 'no limit'),
	(9, 'no day limit'),
	(10, 'line limit'),
	(11, 'no money, no limit');


SELECT * from "ZadarmaAccounts";