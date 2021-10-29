create proc GetTicketWonLog
(
	@ticketId int,
	@userId nvarchar(450)
)
as  
select top(1)
	l.LogId,
	u.UserName,
	t.Win,
	t.Cost,
	p.Balance as ProfileBalance,
	l.[Message],
	l.Exception,
	l.[Date]
from dbo.Logs l
inner join dbo.Tickets t on t.TicketId = l.EntityId
inner join dbo.AspNetUsers u on u.Id = l.UserId
inner join dbo.Profiles p on p.UserId = l.UserId
where l.EntityDiscriminator = 'Ticket' 
	and l.Level = 2
	and t.TicketId = @ticketId
	and l.UserId = @userId
order by l.[Date] desc