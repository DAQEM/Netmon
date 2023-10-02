using DevicesLib.DBO.Component.Interface;

namespace DevicesLib.Repositories.Component.Interface;

public interface IInterfaceRepository
{
    Task AddOrUpdateInterface(InterfaceDBO @interface);
    
    Task SaveChanges();
}