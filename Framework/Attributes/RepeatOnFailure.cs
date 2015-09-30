
using System;
using Gallio.Common;
using Gallio.Framework;
using Gallio.Framework.Pattern;
using Gallio.Model;
using Gallio.Common.Reflection;
using Framework;
using System.Collections;


namespace MbUnit.Framework
{
    /// <summary>
    /// Decorates a test method and causes it to be invoked repeatedly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each repetition of the test method will occur within its own individually labeled
    /// test step so that it can be identified in the test report.
    /// </para>
    /// <para>
    /// The initialize, setup, teardown and dispose methods will are invoked around each
    /// repetition of the test.
    /// </para>
    /// </remarks>
  
    [AttributeUsage(PatternAttributeTargets.Test, AllowMultiple = true, Inherited = true)]
    public class RepeatOnFailAttribute : TestDecoratorPatternAttribute
    {
        //private readonly int numRepetitions;
       // private int _maxNumberOfAttempts;

        /// <summary>
        /// Executes the test method repeatedly.
        /// </summary>
        /// <example>
        /// <code><![CDATA[
        /// [Test]
        /// [Repeat(10)]
        /// public void Test()
        /// {
        ///     // This test will be executed 10 times.
        /// }
        /// ]]></code>
        /// </example>
        /// <param name="numRepetitions">The number of times to repeat the test.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="numRepetitions"/>
        /// is less than 1.</exception>
        

      
        protected override void DecorateTest(IPatternScope scope, ICodeElementInfo codeElement)
        {
            
            scope.TestBuilder.TestInstanceActions.RunTestInstanceBodyChain.Around(delegate(PatternTestInstanceState state, Gallio.Common.Func<PatternTestInstanceState, TestOutcome> inner)
            {
                TestOutcome outcome = TestOutcome.Passed;
                int failureCount = 0;
                // we will try up to 'max' times to get a pass, if we do, then break out and don't run the test anymore
                for (int i = 0; i < FrameGlobals.repeatCount; i++)
                {
                    string name = String.Format("Repetition #{0}", i + 1);
                    TestContext context = TestStep.RunStep(name, delegate
                    {
                        TestOutcome innerOutcome = inner(state);
                        // if we get a fail, and we have used up the number of attempts allowed to get a pass, throw an error
                        if (innerOutcome.Status != TestStatus.Passed)
                        {
                            throw new SilentTestException(innerOutcome);
                        }
                    }, null, false, codeElement);

                    outcome = context.Outcome;
                    // escape the loop if the test has pa   -0ssed, otherwise increment the failure count
                    if (context.Outcome.Status == TestStatus.Passed)
                    {
                        
                        break;
                    }
                        failureCount++;
                }



                TestLog.WriteLine(String.Format(
                        failureCount == FrameGlobals.repeatCount
                            ? "Tried {0} times to get a pass test result but didn't get it"
                            : "The test passed on attempt {1} out of {0}", FrameGlobals.repeatCount, failureCount + 1));


                Queue tcStatus2 = new Queue();
                BaseTest.TestDetails tc2 = new BaseTest.TestDetails();

                string parent=Gallio.Framework.TestContext.CurrentContext.Test.Parent.ToString();
                tc2.Name = Gallio.Framework.TestContext.CurrentContext.Test.Name;
                if (outcome.Status == TestStatus.Passed)
                tc2.Status = "Pass";
                else
                    tc2.Status = "Fail";

                if(BaseTest.thirdStatus.ContainsKey(parent+tc2.Name))
                    tc2.Status = BaseTest.thirdStatus[parent+tc2.Name];

                if (BaseTest.testStatus.ContainsKey(parent))
                {
                    tcStatus2 = BaseTest.testStatus[parent];
                    tcStatus2.Enqueue(tc2);
                    BaseTest.testStatus.Remove(parent);
                    BaseTest.testStatus.Add(parent, tcStatus2);
                }
                else
                {
                    tcStatus2.Enqueue(tc2);
                    BaseTest.testStatus.Add(Gallio.Framework.TestContext.CurrentContext.Test.Parent.ToString(), tcStatus2);
                }


                return outcome;
            });
        }
    }
}