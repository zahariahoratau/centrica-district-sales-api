UPDATE Management.Salesperson
SET [FirstName] = ISNULL(@FirstName, [FirstName]),
    [LastName] = ISNULL(@LastName, [LastName]),
    [BirthDate] = ISNULL(@BirthDate, [BirthDate]),
    [HireDate] = ISNULL(@HireDate, [HireDate]),
    [Email] = ISNULL(@Email, [Email]),
    [PhoneNumber] = ISNULL(@PhoneNumber, [PhoneNumber])
WHERE [Id] = @Id
