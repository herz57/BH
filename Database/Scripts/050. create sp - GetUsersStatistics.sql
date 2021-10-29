create proc GetUsersStatistics
(
	@forDays int
)
as  
select 
	u.UserName,
	count(l.EntityId) as TotalTickets, 
	sum(t.Win) as TotalWin, 
	sum(t.Cost) as TotalCost
from dbo.Logs l
inner join dbo.Tickets t on t.TicketId = l.EntityId
inner join dbo.AspNetUsers u on u.Id = l.UserId
where l.Date > dateadd(day, -@forDays, getutcdate()) and l.EntityDiscriminator = 'Ticket' and l.Level = 2
group by u.UserName 