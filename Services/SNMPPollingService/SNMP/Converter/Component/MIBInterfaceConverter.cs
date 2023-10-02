using DevicesLib.Entities.Component.Interface;
using SNMPPollingService.SNMP.MIB;
using SNMPPollingService.SNMP.MIB.If;
using SNMPPollingService.SNMP.MIB.If.InterfaceX;

namespace SNMPPollingService.SNMP.Converter.Component;

public class MIBInterfaceConverter : IMIBComponentConverter<IInterface>
{
    public List<IInterface> ConvertMIBsToComponent(List<IMIB> mibs)
    {
        IfMIB? ifMIB = mibs.OfType<IfMIB>().FirstOrDefault();
        
        if (ifMIB == null)
        {
            return new List<IInterface>();
        }
        
        return ifMIB.IfTable.IfEntries
            .Select(e =>
            {
                IfXEntry ifXEntry = ifMIB.IfXTable.IfXEntries.FirstOrDefault(e1 => e1.IfIndex == e.IfIndex.ToInt32()) ?? new IfXEntry();
                return new Interface(
                    e.IfIndex.ToInt32(),
                    e.IfDescr.ToString(),
                    e.IfType,
                    e.IfAdminStatus,
                    e.IfOperationalStatus,
                    e.IfSpeed.ToUInt32(),
                    e.IfPhysAddress.ToString(),
                    e.IfMtu.ToInt32(),
                    ifXEntry.IfHCInOctets.ToUInt64(),
                    ifXEntry.IfHCOutOctets.ToUInt64(),
                    e.IfInErrors.ToUInt32(),
                    e.IfOutErrors.ToUInt32(),
                    e.IfInDiscards.ToUInt32(),
                    e.IfOutDiscards.ToUInt32(),
                    ifXEntry.IfHCInBroadcastPkts.ToUInt64(),
                    ifXEntry.IfHCOutBroadcastPkts.ToUInt64(),
                    ifXEntry.IfHCInMulticastPkts.ToUInt64(),
                    ifXEntry.IfHCOutMulticastPkts.ToUInt64(),
                    ifXEntry.IfHCInUcastPkts.ToUInt64(),
                    ifXEntry.IfHCOutUcastPkts.ToUInt64()
                        
                );
            })
            .Cast<IInterface>()
            .ToList();
    }
}