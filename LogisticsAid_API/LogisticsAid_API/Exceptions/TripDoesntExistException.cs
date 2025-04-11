namespace LogisticsAid_API.Exceptions;

public class TripDoesntExistException(string id) : Exception($"Trip with ID '{id}' already exists.");