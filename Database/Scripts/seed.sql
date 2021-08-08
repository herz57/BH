use BH;

-- Domains seed
insert into [Domains] (DomainType, Description) values 
	(1, 'Star Wars'), 
	(2, 'Naruto'), 
	(3, 'Futurama');

-- Mashines seed

while ((select count(*) from Machines) < 10)
begin
	declare @domainTypeToInsert int = (select top (1) DomainType from Machines group by DomainType order by count(*));
	insert into Machines (DomainType) select d.DomainType from [Domains] d where @domainTypeToInsert is null or d.DomainType = @domainTypeToInsert;
end

-- Tickets seed
declare @cursor cursor,
	@machineId int;

set @cursor = cursor for (select MachineId from Machines);
open @cursor;
fetch next from @cursor into @machineId;

while @@FETCH_STATUS = 0
begin
	exec SeedTickets @machineId = @machineId, @cost = 5, @count = 100;
	exec SeedTickets @machineId = @machineId, @cost = 10, @count = 100;
	exec SeedTickets @machineId = @machineId, @cost = 15, @count = 100;
	fetch next from @cursor into @machineId;
end

close @cursor ;
deallocate @cursor;

