using FluentValidation.Results;
namespace Monolithic.Core.SharedKernel.DomainObjects;
public interface IValueObject
{
    ValidationResult? ValidationResult { get; set; }
    bool IsValid();
    void Validate();
}