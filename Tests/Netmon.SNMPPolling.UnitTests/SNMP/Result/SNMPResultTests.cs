using Lextm.SharpSnmpLib;
using Netmon.SNMPPolling.SNMP.Result;
using NUnit.Framework;

namespace Netmon.SNMPPolling.Tests.SNMP.Result;

public class SNMPResultTests
    {
        [TestCaseSource(nameof(VariablesTestCases))]
        public void Variables_Set_GetsCorrectVariables(List<Variable> variables)
        {
            SNMPResult result = new(variables);
            Assert.That(result.Variables, Is.EqualTo(variables));
        }

        [TestCaseSource(nameof(GetEntriesTestCases))]
        public void GetEntries_GivenOID_FiltersAndGroupsCorrectly(
            List<Variable> variables, 
            string oid, 
            int expectedCount)
        {
            SNMPResult result = new(variables);
            List<List<Variable>> entries = result.GetEntries(oid);

            Assert.That(entries, Has.Count.EqualTo(expectedCount));
            Assert.That(entries.SelectMany(e => e).All(v => v.Id.ToString().StartsWith(oid)), Is.True);
        }

        [TestCaseSource(nameof(GetTableTestCases))]
        public void GetTable_GivenOID_FiltersCorrectly(
            List<Variable> variables, 
            string oid,
            int expectedCount)
        {
            SNMPResult result = new(variables);
            List<Variable> table = result.GetTable(oid);

            Assert.That(table, Has.Count.EqualTo(expectedCount));
            Assert.That(table.All(v => v.Id.ToString().StartsWith(oid)), Is.True);
        }

        private static readonly object[] VariablesTestCases =
        {
            new object[] 
            { 
                new List<Variable>
                {
                    new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2.1")),
                    new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2.2")),
                    new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2.3")),
                    new(new ObjectIdentifier("1.2.3.4.1.2.1")),
                } 
            }
        };

        private static readonly object[] GetEntriesTestCases =
        {
            new object[]
            {
                new List<Variable>
                { 
                    new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2.1")),
                    new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2.2")),
                    new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2.3")),
                    new(new ObjectIdentifier("1.2.3.4.1.2.1")),
                },
                "1.3.6.1.2",
                3
            },
        };

        private static readonly object[] GetTableTestCases =
        {
            new object[]
            {
                new List<Variable>
                { 
                    new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2.1")),
                    new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2.2")),
                    new(new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2.3")),
                    new(new ObjectIdentifier("1.2.3.4.1.2.1")),
                },
                "1.3.6.1.2",
                3
            },
        };

    }