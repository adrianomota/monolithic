using FluentValidation.Results;

namespace Monolithic.Core.SharedKernel.DomainObjects;
public abstract class AbstractEntity
{
    protected AbstractEntity()
    {
        Id = System.Guid.NewGuid();
        CreatedAt = System.DateTime.UtcNow;
    }
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdateAt { get; set; }
    public ValidationResult? ValidationResult { get; set; }
    public override bool Equals(object? obj)
    {
        var compjareTo = obj as AbstractEntity;
        if (ReferenceEquals(this, compjareTo)) return true;
        if (ReferenceEquals(null, compjareTo)) return false;
        return Id.Equals(compjareTo.Id);
    }
    public static bool operator ==(AbstractEntity a, AbstractEntity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return false;
        return a.Equals(b);
    }
    public static bool operator !=(AbstractEntity a, AbstractEntity b) => !(a == b);

    public override int GetHashCode()
        => (GetType().GetHashCode() * 907) + Id.GetHashCode();
 
    public override string ToString()
        => $"{GetType().Name} [Id={Id}]";
    public virtual bool IsValid()
        => throw new NotImplementedException();
    public void Validate() => IsValid();
}