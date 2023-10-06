using Lextm.SharpSnmpLib;

namespace Netmon.SNMPPolling.SNMP.Result;

public interface ISNMPResult
{
    public List<Variable> Variables { get; set; }
    
    List<List<Variable>> GetEntries(string oid);
    List<Variable> GetTable(string oid);
}