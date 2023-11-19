SELECT [Id], [FirstName], [LastName], [BirthDate], [HireDate], [Email], [PhoneNumber]
FROM Management.Salesperson
WHERE [Id] IN (
    SELECT [SalespersonId]
    FROM Management.DistrictSecondarySalesperson
    WHERE [DistrictId] = @DistrictId
)
