using DevicesLib.Entities.Component;
using SNMPPollingService.SNMP.MIB;

namespace SNMPPollingService.SNMP.Converter.Component;

public interface IMIBComponentConverter<T> where T : IComponent
{
    public List<T> ConvertMIBsToComponent(List<IMIB> mibs);
}