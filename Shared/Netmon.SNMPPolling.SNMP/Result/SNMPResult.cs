using Lextm.SharpSnmpLib;

namespace Netmon.SNMPPolling.SNMP.Result;

public class SNMPResult(List<Variable> variables) : ISNMPResult
{
    public List<Variable> Variables { get; set; } = variables;

    public List<List<Variable>> GetEntries(string oid)
    {
        return Variables
            .Where(v => v.Id.ToString().StartsWith(oid))
            .GroupBy(variable => variable.Id.ToString().Split(".").Last())
            .Select(group => group.ToList()).ToList();
    }

    public List<Variable> GetTable(string oid)
    {
        return Variables
            .Where(v => v.Id.ToString().StartsWith(oid))
            .ToList();
    }
}