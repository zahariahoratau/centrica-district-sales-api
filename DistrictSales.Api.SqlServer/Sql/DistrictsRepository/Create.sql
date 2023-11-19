INSERT INTO Management.District (PrimarySalespersonId, Name, IsActive, NumberOfStores)
OUTPUT INSERTED.[Id]
VALUES (@PrimarySalespersonId, @Name, @IsActive, @NumberOfStores)
