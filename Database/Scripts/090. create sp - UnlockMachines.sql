create proc UnlockMachines
as  
	with machinesToUnlock
	as
	(
		select 
			max(l.[Date]) as [LastUserActivityDate],
			m.MachineId
		from dbo.Machines m
		inner join dbo.Tickets t on t.MachineId = m.MachineId
		inner join dbo.Logs l on l.EntityId = t.TicketId 
		where m.LockedByUserId = l.UserId
			and l.EntityDiscriminator = 'Ticket' 
		group by l.UserId, m.MachineId
	)
	update m set m.LockedByUserId = null
		from dbo.Machines m
		inner join machinesToUnlock mtl on mtl.MachineId = m.MachineId
		where mtl.[LastUserActivityDate] < dateadd(minute, -1, getutcdate())