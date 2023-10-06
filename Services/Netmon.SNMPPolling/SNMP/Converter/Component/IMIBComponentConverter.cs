using Netmon.Models.Component;
using Netmon.SNMPPolling.SNMP.MIB;

namespace Netmon.SNMPPolling.SNMP.Converter.Component;

public interface IMIBComponentConverter<T> where T : IComponent
{
    public List<T> ConvertMIBsToComponent(List<IMIB> mibs);
}