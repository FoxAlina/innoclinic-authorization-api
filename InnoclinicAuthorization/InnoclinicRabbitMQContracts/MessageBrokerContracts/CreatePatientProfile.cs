namespace InnoclinicRabbitMQContracts.MessageBrokerContracts;

public record CreatePatientProfile
(
    string FirstName,
    string MiddleName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTimeOffset DateOfBirth
);
