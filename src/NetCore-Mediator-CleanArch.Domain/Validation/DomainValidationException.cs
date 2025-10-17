namespace NetCore_Mediator_CleanArch.Domain.Validation;

public class DomainValidationException(string error) : Exception(error)
{
    public static void When(bool hasError, string error)
    {
        if (hasError)
            throw new DomainValidationException(error);
    }
}
