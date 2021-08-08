use BH;
go

delete from Tickets;
delete from Machines;
delete from [Domains];
delete from UsersRoles;
delete from Roles;
delete from Profiles;
delete from Users;
go

DBCC CHECKIDENT ('Tickets', RESEED, 0)
GO
DBCC CHECKIDENT ('Machines', RESEED, 0)
GO
DBCC CHECKIDENT ('Users', RESEED, 0)
GO
DBCC CHECKIDENT ('Roles', RESEED, 0)
GO
DBCC CHECKIDENT ('Profiles', RESEED, 0)
GO
