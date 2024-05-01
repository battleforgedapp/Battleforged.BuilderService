namespace Battleforged.BuilderService.Domain.Exceptions; 

public sealed class EntityNotFoundException<T>(Guid? entityId = null)
    : Exception(entityId.HasValue 
        ? $"Could not find entity of type '{typeof(T).Name}' with ID: '{entityId.Value:N}'." 
        : $"Could not find entity of type '{typeof(T).Name}'."
);