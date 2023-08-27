namespace Fanitty.Server.Core.Common;
public class BaseEntityGeneric<T> : BaseEntity where T : struct
{
    public T Id { get; set; }
}
