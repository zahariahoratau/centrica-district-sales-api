SELECT [Id], [PrimarySalespersonId], [Name], [CreatedAtUtc], [IsActive], [NumberOfStores]
FROM Management.District
WHERE Id = @Id
