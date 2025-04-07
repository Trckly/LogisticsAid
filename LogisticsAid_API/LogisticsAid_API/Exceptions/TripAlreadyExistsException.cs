namespace LogisticsAid_API.Exceptions;

public class TripAlreadyExistsException(string readableId) : Exception($"Trip with ID '{readableId}' already exists.");
