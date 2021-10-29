create procedure SeedTickets
(
	@machineId int,
	@cost int,
	@count int
)
as
begin
	declare @totalCost int = @cost * @count;
	declare @wonTotalValue int = @totalCost * 20 / 100;
	declare @counter int = @count;
	declare @winMultiplier int;

	-- seed symbols
	declare @winSymbolsMap table 
	(
		WinMultiplier varchar(10),
		Symbols varchar(100)
	);

	insert into @winSymbolsMap (WinMultiplier, Symbols) values
		-- x0 win
		(0, '{"Symbols":[["b","a","c"],["a","b","c"],["b","c","a"]]}'),
		(0, '{"Symbols":[["b","a","c"],["a","c","b"],["c","b","a"]]}'),
		(0, '{"Symbols":[["a","c","a"],["b","a","c"],["c","b","a"]]}'),
		(0, '{"Symbols":[["a","b","c"],["b","c","a"],["b","a","c"]]}'),
		(0, '{"Symbols":[["a","b","c"],["c","a","b"],["c","b","a"]]}'),
		(0, '{"Symbols":[["a","c","b"],["c","b","a"],["b","a","c"]]}'),
		
		-- x1 win
		(1, '{"Symbols":[["b","a","c"],["a","a","b"],["c","b","a"]]}'),
		(1, '{"Symbols":[["a","c","b"],["b","a","c"],["c","a","b"]]}'),
		(1, '{"Symbols":[["a","b","c"],["c","b","a"],["b","a","c"]]}'),
		(1, '{"Symbols":[["b","c","a"],["a","b","c"],["c","b","a"]]}'),
		(1, '{"Symbols":[["a","c","c"],["b","c","a"],["c","b","a"]]}'),
		(1, '{"Symbols":[["c","a","b"],["b","c","a"],["b","c","a"]]}'),

		-- x2 win
		(2, '{"Symbols":[["c","a","b"],["b","a","c"],["a","c","b"]]}'),
		(2, '{"Symbols":[["c","b","a"],["a","b","c"],["b","c","a"]]}'),
		(2, '{"Symbols":[["b","c","a"],["a","c","b"],["a","c","b"]]}'),

		-- x3 win
		(3, '{"Symbols":[["c","a","a"],["b","a","c"],["a","a","b"]]}'),
		(3, '{"Symbols":[["a","b","b"],["c","b","a"],["b","b","c"]]}'),
		(3, '{"Symbols":[["a","c","c"],["b","c","a"],["c","c","b"]]}'),
		(3, '{"Symbols":[["b","a","a"],["c","a","b"],["a","a","c"]]}'),
		(3, '{"Symbols":[["a","b","b"],["c","b","a"],["b","b","a"]]}'),
		(3, '{"Symbols":[["b","c","c"],["a","c","b"],["c","c","a"]]}');

	while (@counter > 0)
	begin
		declare @win int = 0;

		if (@counter > @count * 10 / 100)
			begin
				set @win = 0;
				set @winMultiplier = 0;
			end
		else
			if (@counter > @count * 7 / 100 and @wonTotalValue >= @cost * 3)
				begin
					set @win = @cost * 3;
					set @winMultiplier = 3;
				end
			else if (@counter >= @count * 4 / 100 and @wonTotalValue >= @cost * 2)
				begin
					set @win = @cost * 2;
					set @winMultiplier = 2;
				end
			else
				begin
					set @win = @cost;
					set @winMultiplier = 1;
				end
			set @wonTotalValue =  @wonTotalValue - @win;

		set @counter = @counter - 1;

		declare @symbols varchar(100) = (select top(1) Symbols from @winSymbolsMap where WinMultiplier = @winMultiplier order by newid());
		insert into Tickets (MachineId, Cost, Win, Symbols, PlayedOut) values (@machineId, @cost, @win, @symbols, 0);
	end
end