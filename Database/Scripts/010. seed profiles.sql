;with profiles (Id, Balance)
as
(
select Id, 0 as Balance from dbo.AspNetUsers
)
insert into dbo.Profiles select * from profiles;