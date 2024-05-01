namespace Battleforged.BuilderService.Domain.Exceptions; 

public class UnauthorisedAccessToResourceException<T>(Guid? entityId = null, string? message = null)
    : Exception(!string.IsNullOrWhiteSpace(message) ? message : entityId.HasValue
        ? $"Unauthorised access to resource of type '{typeof(T).Name}' with ID: '{entityId.Value:N}'."
        : $"Unauthorised access to resource of type '{typeof(T).Name}'."
);