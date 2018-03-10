﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using CatchTestAdapter;
using System.Collections.Generic;

using TestAdapterTest.Mocks;
using System.IO;

namespace TestAdapterTest
{
    [TestClass]
    public class TestTestDiscoverer
    {

        // Tests that all the tests in the reference project are found.
        [TestMethod]
        [DeploymentItem( Common.ReferenceExePath )]
		public void DiscoversAllTests()
		{
            // Initialize a mock sink to keep track of the discovered tests.
            MockTestCaseDiscoverySink testSink = new MockTestCaseDiscoverySink();

            // Discover tests from the reference project.
            TestDiscoverer discoverer = new TestDiscoverer();
            discoverer.DiscoverTests( Common.ReferenceExeList,
                new MockDiscoveryContext(),
                new MockMessageLogger(),
                testSink );

            // There is a known number of test cases in the reference project.
            Assert.AreEqual( testSink.Tests.Count, Common.ReferenceTestCount );
        }

        // Tests that the test case lines are correct.
        [TestMethod]
        [DeploymentItem( Common.ReferenceExePath )]
        public void TestCaseLinesCorrect()
        {
            // Initialize a mock sink to keep track of the discovered tests.
            MockTestCaseDiscoverySink testSink = new MockTestCaseDiscoverySink();

            // Discover tests from the reference project.
            TestDiscoverer discoverer = new TestDiscoverer();
            discoverer.DiscoverTests( Common.ReferenceExeList,
                new MockDiscoveryContext(),
                new MockMessageLogger(),
                testSink );

            // The reference project test cases are on these lines.
            var linesOfCases = new Dictionary<string, int>();
            linesOfCases.Add( "No tags", 6 );
            linesOfCases.Add( "With tags", 16 );
            linesOfCases.Add( "Has failure", 24 );

            foreach( var test in testSink.Tests )
            {
                // Process only tests we have hard coded here.
                if( linesOfCases.ContainsKey( test.FullyQualifiedName ) )
                {
                    // Check that the line number is correct.
                    Assert.AreEqual( linesOfCases[ test.FullyQualifiedName ], test.LineNumber, test.FullyQualifiedName );

                    // Remove the case so we can check all were handled.
                    linesOfCases.Remove( test.FullyQualifiedName );
                }
            }

            // Make sure all the cases we wanted got checked.
            Assert.AreEqual( linesOfCases.Count, 0, String.Format( "Unhandled cases: {0}", linesOfCases.ToString() ) );
        }
    }
}