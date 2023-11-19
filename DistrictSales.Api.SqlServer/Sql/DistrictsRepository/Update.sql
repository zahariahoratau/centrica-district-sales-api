UPDATE Management.District
SET PrimarySalespersonId = ISNULL(@PrimarySalespersonId, [PrimarySalespersonId]),
    Name = ISNULL(@Name, [Name]),
    IsActive = ISNULL(@IsActive, [IsActive]),
    NumberOfStores = ISNULL(@NumberOfStores, [NumberOfStores])
WHERE Id = @Id
