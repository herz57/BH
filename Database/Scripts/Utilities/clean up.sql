use BH;
go

delete from Tickets;
delete from Machines;
delete from [Domains];
delete from Profiles;
go

DBCC CHECKIDENT ('Tickets', RESEED, 0)
GO
DBCC CHECKIDENT ('Machines', RESEED, 0)
GO
DBCC CHECKIDENT ('Profiles', RESEED, 0)
GO
