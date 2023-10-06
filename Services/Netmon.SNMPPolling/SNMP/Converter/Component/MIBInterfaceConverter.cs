using System.Net.NetworkInformation;
using Netmon.Models.Component.Interface;
using Netmon.Models.Component.Interface.Metric;
using Netmon.SNMPPolling.SNMP.MIB;
using Netmon.SNMPPolling.SNMP.MIB.If;
using Netmon.SNMPPolling.SNMP.MIB.If.InterfaceX;

namespace Netmon.SNMPPolling.SNMP.Converter.Component;

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
                return new Interface{
                    Index = e.IfIndex.ToInt32(),
                    Name = e.IfDescr.ToString(),
                    Type = e.IfType,
                    PhysAddress = e.IfPhysAddress.GetRaw().Length >= 6 ? e.IfPhysAddress.ToPhysicalAddress() : PhysicalAddress.None,
                    Metrics = new List<IInterfaceMetric>
                    {
                        new InterfaceMetric
                        {
                            AdminStatus = e.IfAdminStatus,
                            OperationStatus = e.IfOperationalStatus,
                            Speed = e.IfSpeed.ToUInt32(),
                            Mtu = e.IfMtu.ToInt32(),
                            InOctets = ifXEntry.IfHCInOctets.ToUInt64(),
                            OutOctets = ifXEntry.IfHCOutOctets.ToUInt64(),
                            InErrors = e.IfInErrors.ToUInt32(),
                            OutErrors = e.IfOutErrors.ToUInt32(),
                            InDiscards = e.IfInDiscards.ToUInt32(),
                            OutDiscards = e.IfOutDiscards.ToUInt32(),
                            InBroadcastPackets = ifXEntry.IfHCInBroadcastPkts.ToUInt64(),
                            OutBroadcastPackets = ifXEntry.IfHCOutBroadcastPkts.ToUInt64(),
                            InMulticastPackets = ifXEntry.IfHCInMulticastPkts.ToUInt64(),
                            OutMulticastPackets = ifXEntry.IfHCOutMulticastPkts.ToUInt64(),
                            InUnicastPackets = ifXEntry.IfHCInUcastPkts.ToUInt64(),
                            OutUnicastPackets = ifXEntry.IfHCOutUcastPkts.ToUInt64(),
                        }
                    }
                };
            })
            .Cast<IInterface>()
            .ToList();
    }
}