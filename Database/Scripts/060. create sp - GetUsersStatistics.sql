create proc GetUsersStatistics
(
	@forDays int
)
as  
select 
	u.UserName,
	count(th.TicketId) as TotalTickets, 
	sum(t.Win) as TotalWin, 
	sum(t.Cost) as TotalCost
from dbo.TicketHistories th
inner join dbo.Tickets t on t.TicketId = th.TicketId
inner join dbo.AspNetUsers u on u.Id = th.PlayedOutByUserId
where th.PlayedOutDate > dateadd(day, -@forDays, getutcdate())
group by u.UserName