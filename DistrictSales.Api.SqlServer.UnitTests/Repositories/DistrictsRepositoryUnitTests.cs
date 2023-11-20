using System.Collections.Immutable;
using System.Data;
using Dapper;
using DistrictSales.Api.Domain.Models;
using DistrictSales.Api.Domain.Repositories;
using DistrictSales.Api.SqlServer.Factories;
using DistrictSales.Api.SqlServer.Models;
using DistrictSales.Api.SqlServer.Repositories;
using Moq.Dapper;

namespace DistrictSales.Api.SqlServer.UnitTests.Repositories;

public class DistrictsRepositoryUnitTests
{
    private readonly Mock<IConnectionFactory> _connectionFactoryMock;
    private readonly Mock<ISalespeopleRepository> _salespeopleRepositoryMock;
    private readonly DistrictsRepository _repository;

    public DistrictsRepositoryUnitTests()
    {
        _connectionFactoryMock = new Mock<IConnectionFactory>();
        _salespeopleRepositoryMock = new Mock<ISalespeopleRepository>();
        _repository = new DistrictsRepository(_connectionFactoryMock.Object, _salespeopleRepositoryMock.Object);
    }

    [Fact]
    public void GetAllAsync_ShouldReturnAllDistricts_WhenDistrictsExist()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;
        IEnumerable<DbDistrict> dbDistricts = new List<DbDistrict>
        {
            new(
                Id: Guid.NewGuid(),
                PrimarySalespersonId: Guid.NewGuid(),
                Name: "District 1",
                CreatedAtUtc: DateTime.UtcNow,
                IsActive: true,
                NumberOfStores: 1
            )
        };
        IEnumerable<District> expected = new List<District>
        {
            new(
                Id: dbDistricts.ElementAt(0).Id,
                Name: dbDistricts.ElementAt(0).Name,
                CreatedAtUtc: dbDistricts.ElementAt(0).CreatedAtUtc,
                IsActive: dbDistricts.ElementAt(0).IsActive,
                NumberOfStores: dbDistricts.ElementAt(0).NumberOfStores,
                PrimarySalesperson: new Salesperson(
                    Id: dbDistricts.ElementAt(0).PrimarySalespersonId,
                    FirstName: "John",
                    LastName: "Doe",
                    BirthDate: DateOnly.FromDateTime(DateTime.UtcNow),
                    HireDate: DateOnly.FromDateTime(DateTime.UtcNow),
                    Email: "johndoe@johndoe.com",
                    PhoneNumber: "12345678"
                ),
                SecondarySalespeople: new List<Salesperson>()
            )
        };

        _connectionFactoryMock
            .Setup(connectionFactory => connectionFactory.GetConnection())
            .Returns(Mock.Of<IDbConnection>());
        _salespeopleRepositoryMock
            .Setup(salespeopleRepository => salespeopleRepository.GetByIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ReturnsAsync(new Salesperson(
                Id: dbDistricts.ElementAt(0).PrimarySalespersonId,
                FirstName: "John",
                LastName: "Doe",
                BirthDate: DateOnly.FromDateTime(DateTime.UtcNow),
                HireDate: DateOnly.FromDateTime(DateTime.UtcNow),
                Email: "johndoe@johndoe.com",
                PhoneNumber: "12345678"
            ));
        _salespeopleRepositoryMock
            .Setup(salespeopleRepository => salespeopleRepository.GetAllSecondaryByDistrictIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ReturnsAsync(new ImmutableArray<Salesperson>());

        Mock<IDbConnection> connectionMock = new();
        connectionMock
            .SetupDapperAsync(connection => connection.QueryAsync<DbDistrict>(It.IsAny<CommandDefinition>()))
            .ReturnsAsync(dbDistricts);
        _connectionFactoryMock.Setup(connectionFactory => connectionFactory.GetConnection()).Returns(connectionMock.Object);

        // Act
        IEnumerable<District> actual = _repository.GetAllAsync(cancellationToken).Result;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetByIdAsync_ShouldReturnDistrict_WhenDistrictExists()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;
        DbDistrict dbDistrict = new(
            Id: Guid.NewGuid(),
            PrimarySalespersonId: Guid.NewGuid(),
            Name: "District 1",
            CreatedAtUtc: DateTime.UtcNow,
            IsActive: true,
            NumberOfStores: 1
        );
        District expected = new(
            Id: dbDistrict.Id,
            Name: dbDistrict.Name,
            CreatedAtUtc: dbDistrict.CreatedAtUtc,
            IsActive: dbDistrict.IsActive,
            NumberOfStores: dbDistrict.NumberOfStores,
            PrimarySalesperson: new Salesperson(
                Id: dbDistrict.PrimarySalespersonId,
                FirstName: "John",
                LastName: "Doe",
                BirthDate: DateOnly.FromDateTime(DateTime.UtcNow),
                HireDate: DateOnly.FromDateTime(DateTime.UtcNow),
                Email: "johndoe@johndoe.com",
                PhoneNumber: "12345678"
            ),
            SecondarySalespeople: new List<Salesperson>()
        );

        _connectionFactoryMock
            .Setup(connectionFactory => connectionFactory.GetConnection())
            .Returns(Mock.Of<IDbConnection>());
        _salespeopleRepositoryMock.Setup(salespeopleRepository => salespeopleRepository.GetByIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ReturnsAsync(new Salesperson(
                Id: dbDistrict.PrimarySalespersonId,
                FirstName: "John",
                LastName: "Doe",
                BirthDate: DateOnly.FromDateTime(DateTime.UtcNow),
                HireDate: DateOnly.FromDateTime(DateTime.UtcNow),
                Email: "johndoe@johndoe.com",
                PhoneNumber: "12345678"
            ));
        _salespeopleRepositoryMock
            .Setup(salespeopleRepository => salespeopleRepository.GetAllSecondaryByDistrictIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ReturnsAsync(new ImmutableArray<Salesperson>());

        Mock<IDbConnection> connectionMock = new();
        connectionMock
            .SetupDapperAsync(connection => connection.QuerySingleAsync<DbDistrict>(It.IsAny<CommandDefinition>()))
            .ReturnsAsync(dbDistrict);
        _connectionFactoryMock.Setup(connectionFactory => connectionFactory.GetConnection()).Returns(connectionMock.Object);

        // Act
        District actual = _repository.GetByIdAsync(dbDistrict.Id, cancellationToken).Result;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void CreateAsync_ShouldReturnDistrict_WhenDistrictIsCreated()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;
        CreateDistrict createDistrict = new(
            Name: "District 1",
            PrimarySalespersonId: Guid.NewGuid(),
            IsActive: true,
            NumberOfStores: 1
        );
        DbDistrict dbDistrict = new(
            Id: Guid.NewGuid(),
            PrimarySalespersonId: createDistrict.PrimarySalespersonId,
            Name: createDistrict.Name,
            CreatedAtUtc: DateTime.UtcNow,
            IsActive: true,
            NumberOfStores: createDistrict.NumberOfStores
        );
        District expected = new(
            Id: dbDistrict.Id,
            Name: dbDistrict.Name,
            CreatedAtUtc: dbDistrict.CreatedAtUtc,
            IsActive: dbDistrict.IsActive,
            NumberOfStores: dbDistrict.NumberOfStores,
            PrimarySalesperson: new Salesperson(
                Id: dbDistrict.PrimarySalespersonId,
                FirstName: "John",
                LastName: "Doe",
                BirthDate: DateOnly.FromDateTime(DateTime.UtcNow),
                HireDate: DateOnly.FromDateTime(DateTime.UtcNow),
                Email: "johndoe@johndoe.com",
                PhoneNumber: "12345678"
            ),
            SecondarySalespeople: new List<Salesperson>()
        );

        _connectionFactoryMock
            .Setup(connectionFactory => connectionFactory.GetConnection())
            .Returns(Mock.Of<IDbConnection>());
        _salespeopleRepositoryMock.Setup(salespeopleRepository => salespeopleRepository.GetByIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ReturnsAsync(new Salesperson(
                Id: dbDistrict.PrimarySalespersonId,
                FirstName: "John",
                LastName: "Doe",
                BirthDate: DateOnly.FromDateTime(DateTime.UtcNow),
                HireDate: DateOnly.FromDateTime(DateTime.UtcNow),
                Email: "johndoe@johndoe.com",
                PhoneNumber: "12345678"
            ));
        _salespeopleRepositoryMock
            .Setup(salespeopleRepository => salespeopleRepository.GetAllSecondaryByDistrictIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ReturnsAsync(new ImmutableArray<Salesperson>());

        Mock<IDbConnection> connectionMock = new();
        connectionMock
            .SetupDapperAsync(connection => connection.QuerySingleAsync<Guid>(It.IsAny<CommandDefinition>()))
            .ReturnsAsync(dbDistrict.Id);
        connectionMock
            .SetupDapperAsync(connection => connection.QuerySingleAsync<DbDistrict>(It.IsAny<CommandDefinition>()))
            .ReturnsAsync(dbDistrict);
        _connectionFactoryMock.Setup(connectionFactory => connectionFactory.GetConnection()).Returns(connectionMock.Object);

        // Act
        District actual = _repository.CreateAsync(createDistrict, cancellationToken).Result;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void CreateAsync_ShouldThrowException_WhenDistrictIsNotCreated()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;
        CreateDistrict createDistrict = new(
            Name: "District 1",
            PrimarySalespersonId: Guid.NewGuid(),
            IsActive: true,
            NumberOfStores: 1
        );

        _connectionFactoryMock
            .Setup(connectionFactory => connectionFactory.GetConnection())
            .Returns(Mock.Of<IDbConnection>());
        _salespeopleRepositoryMock.Setup(salespeopleRepository => salespeopleRepository.GetByIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ReturnsAsync(new Salesperson(
                Id: createDistrict.PrimarySalespersonId,
                FirstName: "John",
                LastName: "Doe",
                BirthDate: DateOnly.FromDateTime(DateTime.UtcNow),
                HireDate: DateOnly.FromDateTime(DateTime.UtcNow),
                Email: "johndoe@johndoe.com",
                PhoneNumber: "12345678"
            ));
        _salespeopleRepositoryMock
            .Setup(salespeopleRepository => salespeopleRepository.GetAllSecondaryByDistrictIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ReturnsAsync(new ImmutableArray<Salesperson>());

        Mock<IDbConnection> connectionMock = new();
        connectionMock
            .SetupDapperAsync(connection => connection.QuerySingleAsync<Guid>(It.IsAny<CommandDefinition>()))
            .ReturnsAsync(Guid.Empty);

        _connectionFactoryMock.Setup(connectionFactory => connectionFactory.GetConnection()).Returns(connectionMock.Object);

        // Act
        Func<Task> actual = async () => await _repository.CreateAsync(createDistrict, cancellationToken);

        // Assert
        actual.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public void UpdateAsync_ShouldReturn_WhenDistrictIsUpdated()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;
        UpdateDistrict updateDistrict = new(
            Id: Guid.NewGuid(),
            Name: "District 1",
            PrimarySalespersonId: Guid.NewGuid(),
            IsActive: true,
            NumberOfStores: 1
        );
        DbDistrict dbDistrict = new(
            Id: updateDistrict.Id,
            PrimarySalespersonId: updateDistrict.PrimarySalespersonId!.Value,
            Name: updateDistrict.Name!,
            CreatedAtUtc: DateTime.UtcNow,
            IsActive: true,
            NumberOfStores: updateDistrict.NumberOfStores
        );

        _connectionFactoryMock
            .Setup(connectionFactory => connectionFactory.GetConnection())
            .Returns(Mock.Of<IDbConnection>());
        _salespeopleRepositoryMock
            .Setup(salespeopleRepository => salespeopleRepository.GetByIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ReturnsAsync(new Salesperson(
                Id: dbDistrict.PrimarySalespersonId,
                FirstName: "John",
                LastName: "Doe",
                BirthDate: DateOnly.FromDateTime(DateTime.UtcNow),
                HireDate: DateOnly.FromDateTime(DateTime.UtcNow),
                Email: "johndoe@johndoe.com",
                PhoneNumber: "12345678"
            ));

        Mock<IDbConnection> connectionMock = new();
        connectionMock
            .SetupDapperAsync(connection => connection.ExecuteAsync(It.IsAny<CommandDefinition>()))
            .ReturnsAsync(1);

        _connectionFactoryMock.Setup(connectionFactory => connectionFactory.GetConnection()).Returns(connectionMock.Object);

        // Act
        _repository.UpdateAsync(updateDistrict.Id, updateDistrict, cancellationToken).GetAwaiter().GetResult();

        // Assert. If no exception is thrown, the test passes.
    }

    [Fact]
    public void UpdateAsync_ShouldThrowException_WhenDistrictIsNotUpdated()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;
        UpdateDistrict updateDistrict = new(
            Id: Guid.NewGuid(),
            Name: "District 1",
            PrimarySalespersonId: Guid.NewGuid(),
            IsActive: true,
            NumberOfStores: 1
        );
        DbDistrict dbDistrict = new(
            Id: updateDistrict.Id,
            PrimarySalespersonId: updateDistrict.PrimarySalespersonId!.Value,
            Name: updateDistrict.Name!,
            CreatedAtUtc: DateTime.UtcNow,
            IsActive: true,
            NumberOfStores: updateDistrict.NumberOfStores
        );

        _connectionFactoryMock
            .Setup(connectionFactory => connectionFactory.GetConnection())
            .Returns(Mock.Of<IDbConnection>());
        _salespeopleRepositoryMock
            .Setup(salespeopleRepository => salespeopleRepository.GetByIdAsync(It.IsAny<Guid>(), cancellationToken))
            .ReturnsAsync(new Salesperson(
                Id: dbDistrict.PrimarySalespersonId,
                FirstName: "John",
                LastName: "Doe",
                BirthDate: DateOnly.FromDateTime(DateTime.UtcNow),
                HireDate: DateOnly.FromDateTime(DateTime.UtcNow),
                Email: "johndoe@johndoe.com",
                PhoneNumber: "12345678"
            ));

        Mock<IDbConnection> connectionMock = new();
        connectionMock
            .SetupDapperAsync(connection => connection.ExecuteAsync(It.IsAny<CommandDefinition>()))
            .ReturnsAsync(0);

        _connectionFactoryMock.Setup(connectionFactory => connectionFactory.GetConnection()).Returns(connectionMock.Object);

        // Act
        Func<Task> actual = async () => await _repository.UpdateAsync(updateDistrict.Id, updateDistrict, cancellationToken);

        // Assert
        actual.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public void DeleteAsync_ShouldReturn_WhenDistrictIsDeleted()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;
        Guid districtId = Guid.NewGuid();

        _connectionFactoryMock
            .Setup(connectionFactory => connectionFactory.GetConnection())
            .Returns(Mock.Of<IDbConnection>());

        Mock<IDbConnection> connectionMock = new();
        connectionMock
            .SetupDapperAsync(connection => connection.ExecuteAsync(It.IsAny<CommandDefinition>()))
            .ReturnsAsync(1);

        _connectionFactoryMock.Setup(connectionFactory => connectionFactory.GetConnection()).Returns(connectionMock.Object);

        // Act
        _repository.DeleteAsync(districtId, cancellationToken).GetAwaiter().GetResult();

        // Assert. If no exception is thrown, the test passes.
    }

    [Fact]
    public void DeleteAsync_ShouldThrowException_WhenDistrictIsNotDeleted()
    {
        // Arrange
        CancellationToken cancellationToken = CancellationToken.None;
        Guid districtId = Guid.NewGuid();

        _connectionFactoryMock
            .Setup(connectionFactory => connectionFactory.GetConnection())
            .Returns(Mock.Of<IDbConnection>());

        Mock<IDbConnection> connectionMock = new();
        connectionMock
            .SetupDapperAsync(connection => connection.ExecuteAsync(It.IsAny<CommandDefinition>()))
            .ReturnsAsync(0);

        _connectionFactoryMock.Setup(connectionFactory => connectionFactory.GetConnection()).Returns(connectionMock.Object);

        // Act
        Func<Task> actual = async () => await _repository.DeleteAsync(districtId, cancellationToken);

        // Assert
        actual.Should().ThrowAsync<Exception>();
    }
}
